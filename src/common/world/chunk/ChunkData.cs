using System.Diagnostics;
using OpenTK.Mathematics;
using Sandbox.Common;
using Sandbox.Common.NBT;

namespace Sandbox.Worlds.Chunks
{

  public class ChunkData
  {

    private List<int> heapCounts = new List<int>();
    private List<ResourceLocation?> heapIDs = new List<ResourceLocation?>();
    private int[,,] tiles = new int[20, 20, 20];

    public Vector3i position { get; private set; }


    public ChunkData(Vector3i position)
    {
      this.position = position;
    }

    public CompoundTag Save()
    {
      CompoundTag tag = new CompoundTag("Chunk");
      tag.Add(new IntTag("x", position.X));
      tag.Add(new IntTag("y", position.Y));
      tag.Add(new IntTag("z", position.Z));

      // heap counts
      // tag.Add(new ListTag("heapCounts", heapCounts.Select((int count) => new IntTag(count)).ToList()));
      // nevermind. i figuer that if we just save the heap ids, we can just recalculate the heap counts later
      // uncomment this if the CPU is angry at us :)

      // heap ids
      tag.Add(new ListTag("heapIDs", heapIDs.Select((ResourceLocation? id) => new StringTag(id!.ToString())).ToList()));


      // tiles
      List<int> comp_tiles = new List<int>();
      foreach (int id in tiles)
      {
        comp_tiles.Add(id);
      }
      tag.Add(new IntArrayTag("tiles", comp_tiles.ToArray()));

      return tag;
    }

    public bool Load(CompoundTag tag)
    {

      if (tag == null) return false;
      position = new Vector3i(tag.Get<IntTag>("x")!.Value, tag.Get<IntTag>("y")!.Value, tag.Get<IntTag>("z")!.Value);
      tiles = new int[20, 20, 20];

      // heap counts
      // heapCounts = tag.Get<ListTag>("heapCounts")!.ToArray<IntTag>().ToList().ConvertAll((IntTag count) => count.Value);
      // nevermind. i figuer that if we just save the heap ids, we can just recalculate the heap counts later
      // uncomment this if the CPU is angry at us :)


      // heap ids
      heapIDs = tag.Get<ListTag>("heapIDs")!.ToArray<StringTag>().ToList().ConvertAll((StringTag id) => id.HasValue ? new ResourceLocation(id.Value) : null);

      // tiles
      int[] comp_tiles = tag.Get<IntArrayTag>("tiles")!.Value;
      for (int i = 0; i < comp_tiles.Length; ++i)
      {
        tiles[i % 20, (i / 20) % 20, i / 400] = comp_tiles[i];
      }

      // recalculate heapCounts
      heapCounts = new List<int>();
      foreach (int id in tiles)
      {
        if (id == -1) continue;
        if (heapCounts.Count <= id)
        {
          heapCounts.Add(1);
        }
        else
        {
          heapCounts[id] += 1;
        }
      }
      return true;
    }


    public void SetTile(Vector3i pos, ResourceLocation tile)
    {
      SetTile(pos.X, pos.Y, pos.Z, tile);
    }

    public void SetTile(int x, int y, int z, ResourceLocation tile)
    {
      // decrement the tile at the value
      DecrTile(GetTile(x, y, z));
      // increment the tile
      IncrTile(tile);
      tiles[x, y, z] = heapIDs.IndexOf(tile);
    }

    public void RemoveTile(Vector3i pos)
    {
      RemoveTile(pos.X, pos.Y, pos.Z);
    }

    public void RemoveTile(int x, int y, int z)
    {
      DecrTile(GetTile(x, y, z));
      tiles[x, y, z] = -1;
    }

    public ResourceLocation? GetTile(Vector3i pos)
    {
      return GetTile(pos.X, pos.Y, pos.Z);
    }

    public ResourceLocation? GetTile(int x, int y, int z)
    {
      if (x < 0 || x >= 20 || y < 0 || y >= 20 || z < 0 || z >= 20)
      {
        return null;
      }
      return heapIDs[tiles[x, y, z]];
    }

    private void IncrTile(ResourceLocation? tile)
    {
      int index = heapIDs.IndexOf(tile);
      if (index == -1)
      {
        index = heapIDs.IndexOf(null);
        if (index == -1)
        {
          heapIDs.Add(tile);
          heapCounts.Add(1);
        }
        else
        {
          heapIDs[index] = tile;
          heapCounts[index] = 1;
        }
      }
      else
      {
        heapCounts[index] += 1;
      }
    }

    private void DecrTile(ResourceLocation? tile)
    {
      int index = heapIDs.IndexOf(tile);
      if (index == -1)
      {
        Debug.Fail("Tried to decrement tile count that does not exist");
        // heapIDs[index] == null;
        // heapCounts[index] = -1;
      }
      else
      {
        heapCounts[index] -= 1;
        if (heapCounts[index] == 0)
        {
          heapIDs[index] = null;
        }
      }
    }


    /*!SECTION

    ﻿using UnityEngine;
    using System.Threading;
    using System.Collections.Generic;
    using Sandbox.Core;
    using Sandbox.Worlds.Levels.Tiles.States;
    using Sandbox.NBT;
    using System.Linq;

    namespace Sandbox.Worlds.Chunks
    {
      public class LevelChunk
      {
        public Vector3Int position;
        private GameObject[] children;
        public bool terrainReady { get; private set; }
        public bool startedLoadingDetails { get; private set; }
        public bool chunkReady { get; private set; }


        public List<TileState> states;


        private int[,,] tiles;

        public List<ResourceLocation> tileIndexes;
        public Dictionary<int, int> tileCounts;


        public CompoundTag saveData;


        public bool isDirty;

        //devide by chance ( 1 in X )
        const int STRUCTURE_CHANCE_TREE = (int.MaxValue / 100);
        const int STRUCTURE_CHANCE_WELL = (int.MaxValue / 512);
        const int STRUCTURE_CHANCE_CAVE_ENTRANCE = (int.MaxValue / 50);


        private Thread loadTerrainThread;
        private Thread loadDetailsThread;

        public HashSet<Vector3Int> references;

        public List<StructureInfo> structures;


        private LevelChunk front, left, back, right, up, down; //neighbours (only exist while loading structures)

        public struct StructureInfo
        {
          public StructureInfo(Vector3Int position, Structure.Type type, int seed)
          {
            this.position = position;
            this.type = type;
            this.seed = seed;
          }
          public Vector3Int position;
          public Structure.Type type;
          public int seed;
        }


        public LevelChunk(Vector3Int position)
        {
          this.position = position;
          terrainReady = false;
          startedLoadingDetails = false;
          chunkReady = false;
          isDirty = false;
          references = new HashSet<Vector3Int>();

          tiles = new int[20, 20, 20];
          tileIndexes = new List<ResourceLocation>();
          tileCounts = new Dictionary<int, int>();
        }

        public void StartTerrainLoading()
        {
          //Debug.Log($"Chunk {position} start terrain loading");
          loadTerrainThread = new Thread(LoadTerrain);
          loadTerrainThread.IsBackground = true;
          loadTerrainThread.Start();
        }

        public void StartDetailsLoading(LevelChunk front, LevelChunk left, LevelChunk back, LevelChunk right, LevelChunk up, LevelChunk down)
        {
          //Debug.Log($"Chunk {position} start structure loading");
          //need to temporarily cache LevelChunk of neighbors since generation is on another thread
          this.front = front;
          this.left = left;
          this.right = right;
          this.back = back;
          this.up = up;
          this.down = down;

          loadDetailsThread = new Thread(LoadDetails);
          loadDetailsThread.IsBackground = true;
          loadDetailsThread.Start();
          startedLoadingDetails = true;
        }


        public ResourceLocation GetTile(Vector3Int position)
        {
          return GetTile(position.x, position.y, position.z);
        }

        public ResourceLocation GetTile(int x, int y, int z)
        {

          if (x < 0 || x >= 20 || y < 0 || y >= 20 || z < 0 || z >= 20)
          {
            return new ResourceLocation("core", "air");
          }
          // Debug.Log(tiles[x, y, z]);
          // Debug.Log($"GetTile {x} {y} {z}");
          return tileIndexes[tiles[x, y, z]];
        }
        public void LoadTerrain() //also loads structures INFO
        {
          // TODO 3d TERRAIN and a bunch of other shit
          Vector3Int worldPos = position * 20;

          saveData = SaveDataManager.instance.LoadChunk(position);
          // if the thing does not exist, load from 
          if (Load(saveData))
          {
            Debug.Log("CHUNK EXISTS YEAHH");
          }
          else
          {
            Debug.Log("Chunk Does Not Exist, whoops");
            for (int z = 0; z < 20; ++z)
            {
              for (int y = 0; y < 20; ++y)
              {
                for (int x = 0; x < 20; ++x)
                {
                  int localX = worldPos.x + x;
                  int localY = worldPos.y + y;
                  int localZ = worldPos.z + z;
                  float height = SimplexNoise.Noise.CalcPixel3D(localX * 1000, localY, localZ * 1000, .00000000001f);

                  // height = height * 20 + 20;
                  height = 20;
                  int heightInt = (int)height;

                  float bedrock = SimplexNoise.Noise.CalcPixel3D(localX, localY, localZ, 1f);
                  bedrock = bedrock * 3 + 1;
                  int bedrockInt = (int)bedrock;
                  //bedrock
                  if (localY < bedrockInt)
                  {
                    Modify(x, y, z, new ResourceLocation("expanse", "bedrock"));
                    continue;
                  }

                  //air
                  if (localY > heightInt)
                  {
                    Modify(x, y, z, new ResourceLocation("core", "air"));
                    continue;
                  }

                  //ores
                  float o1 = SimplexNoise.Noise.CalcPixel3D(localX + 50000, localY, localZ, 0.1f);
                  float o2 = SimplexNoise.Noise.CalcPixel3D(localX + 20000, localY, localZ, 0.1f);
                  float o3 = SimplexNoise.Noise.CalcPixel3D(localX + 30000, localY, localZ, 0.04f);
                  float o4 = SimplexNoise.Noise.CalcPixel3D(localX + 60000, localY, localZ, 0.1f);
                  float o5 = SimplexNoise.Noise.CalcPixel3D(localX + 70000, localY, localZ, 0.1f);
                  float o6 = SimplexNoise.Noise.CalcPixel3D(localX + 80000, localY, localZ, 0.03f);

                  float heightGradient = Mathf.Pow(Mathf.Clamp01(y / 128f), 2f);

                  // caves
                  float c1 = SimplexNoise.Noise.CalcPixel3D(localX, localY, localZ, 0.1f);
                  float c2 = SimplexNoise.Noise.CalcPixel3D(localX, localY, localZ, 0.04f);
                  float c3 = SimplexNoise.Noise.CalcPixel3D(localX, localY, localZ, 0.02f);
                  float c4 = SimplexNoise.Noise.CalcPixel3D(localX, localY, localZ, 0.01f);

                  c1 += (heightGradient);
                  // if (c1 < .5 && c2 < .5 && c3 < .5 && c4 < .5)
                  // {
                  //   blocks[x, y, z] = BlockTypes.AIR;
                  //   continue;
                  // }

                  //grass level
                  if (localY == heightInt)
                  {
                    Modify(x, y, z, new ResourceLocation("expanse", "tile_grass"));
                    continue;
                  }

                  //dirt
                  if (localY >= heightInt - 4)
                  {
                    Modify(x, y, z, new ResourceLocation("expanse", "dirt"));
                    continue;
                  }


                  // o5 += (heightGradient);
                  // if (y < 20 && o5 < .04)
                  // {
                  //   blocks[x, y, z] = BlockTypes.GOLD;
                  //   continue;
                  // }

                  // if (y < 20 && Mathf.Pow(o2, 4f) > .7 && o3 < .1)
                  // {
                  //   blocks[x, y, z] = BlockTypes.DIAMOND;
                  //   continue;
                  // }

                  // if (o4 < .1 && o6 > .8)
                  // {
                  //   blocks[x, y, z] = BlockTypes.IRON;
                  //   continue;
                  // }

                  // if (o1 < .08)
                  // {
                  //   blocks[x, y, z] = BlockTypes.COAL;
                  //   continue;
                  // }

                  //remaining is stone
                  Modify(x, y, z, new ResourceLocation("expanse", "stone"));
                  continue;
                }
              }
            }

          }
          string hash = Level.activeWorld.info.seed.ToString() + position.x.ToString() + position.y.ToString() + position.z.ToString();
          int structuresSeed = hash.GetHashCode();
          //Debug.Log("Chunk structures seed is " + structuresSeed);
          System.Random rnd = new System.Random(structuresSeed);
          structures = new List<StructureInfo>();
          bool[,,] spotsTaken = new bool[20, 20, 20];

          //trees
          // for (int z = 2; z < 14; ++z)
          // {
          //   for (int y = 2; y < 14; ++y)
          //   {
          //     for (int x = 2; x < 14; ++x)
          //     {
          //       if (rnd.Next() < STRUCTURE_CHANCE_TREE)
          //       {
          //         if (IsSpotFree(spotsTaken, new Vector3Int(x, y, z), 2))
          //         {
          //           spotsTaken[x, y, z] = true;
          //           int height = 255;
          //           while (height > 0)
          //           {
          //             if (blocks[x, y, z] == BlockTypes.GRASS)
          //             {
          //               structures.Add(new StructureInfo(new Vector3Int(x, height + 1, y), Structure.Type.OAK_TREE, rnd.Next()));
          //               break;
          //             }
          //             height--;
          //           }
          //         }
          //       }
          //     }
          //   }
          // }

          // if (rnd.Next() < STRUCTURE_CHANCE_WELL)
          // {
          //   if (IsSpotFree(spotsTaken, new Vector3Int(7, 7, 7), 3))
          //   {
          //     //Debug.Log("Spot is free");

          //     int minH = 255;
          //     int maxH = 0;
          //     bool canPlace = true;
          //     for (int y = 5; y < 11; ++y)
          //     {
          //       for (int x = 5; x < 11; ++x)
          //       {
          //         for (int h = 255; h > -1; h--)
          //         {
          //           byte b = blocks[x, h, y];
          //           if (b != BlockTypes.AIR)
          //           {
          //             //Debug.Log(b);
          //             canPlace &= (b == BlockTypes.GRASS);
          //             minH = Mathf.Min(minH, h);
          //             maxH = Mathf.Max(maxH, h);
          //             break;
          //           }
          //         }
          //       }
          //     }
          //     canPlace &= Mathf.Abs(minH - maxH) < 2;
          //     if (canPlace)
          //     {
          //       Debug.Log("spawning well structure");
          //       for (int z = 5; z < 11; ++z)
          //       {
          //         for (int y = 5; y < 11; ++y)
          //         {
          //           for (int x = 5; x < 11; ++x)
          //           {
          //             spotsTaken[x, y, z] = true;
          //           }
          //         }
          //       }

          //       int h = 255;
          //       while (h > 0)
          //       {
          //         if (blocks[7, 7, 7] != BlockTypes.AIR)
          //         {
          //           structures.Add(new StructureInfo(new Vector3Int(7, 7, 7), Structure.Type.WELL, rnd.Next()));
          //           break;
          //         }
          //         h--;
          //       }
          //     }
          //   }
          // }



          //already load changes from disk here (apply later)
          // saveData =
          CalculateTileCounts();


          terrainReady = true;
          //Debug.Log($"Chunk {position} terrain ready");

        }

        private bool IsSpotFree(bool[,,] spotsTaken, Vector3Int position, int size) //x area is for example size + 1 + size
        {
          bool spotTaken = false;
          for (int z = Mathf.Max(0, position.z - size); z < Mathf.Min(19, position.z + size + 1); ++z)
          {
            for (int y = Mathf.Max(0, position.y - size); y < Mathf.Min(19, position.y + size + 1); ++y)
            {
              for (int x = Mathf.Max(0, position.x - size); x < Mathf.Min(19, position.x + size + 1); ++x)
              {
                spotTaken |= spotsTaken[x, y, z];
              }
            }
          }
          return !spotTaken;
        }

        private void LoadDetails()
        {


          //load structures

          Load(saveData);

          // for (int i = 0; i < structures.Count; ++i)
          // {
          //   StructureInfo structure = structures[i];
          //   bool overwritesEverything = Structure.OverwritesEverything(structure.type);
          //   Vector3Int p = structure.position;
          //   int x = p.x;
          //   int y = p.y;
          //   int z = p.z;
          //   List<Structure.Change> changeList = Structure.Generate(structure.type, structure.seed);
          //   //Debug.Log($"placing {structure.type} wich has {changeList.Count} blocks");
          //   for (int j = 0; j < changeList.Count; ++j)
          //   {
          //     Structure.Change c = changeList[j];
          //     int placeX = x + c.x;
          //     int placeY = y + c.y;
          //     int placeZ = z + c.z;

          //     if (!overwritesEverything)
          //     {
          //       //only place new blocks if density is higher or the same (leaves can't replace dirt for example)
          //       // if (blocks[placeX, placeY, placeZ] < BlockTypes.density[c.b]) continue;
          //     }

          //     // tiles[placeX, placeY, placeZ] = c.b;
          //   }
          // }

          //remove all references to neighbors to avoid them staying in memory when unloading chunks
          front = null;
          left = null;
          right = null;
          back = null;
          up = null;
          down = null;

          //Debug.Log($"Chunk {position} structures ready");

          //load changes
          // List<ChunkSaveData.C> changes = saveData.changes;
          // for (int i = 0; i < changes.Count; ++i)
          // {
          //   ChunkSaveData.C c = changes[i];
          //   tiles[c.x, c.y, c.z] = c.b;

          // ModManager.Get("tile", c.b).OnPlace(this, new Vector3Int(c.x, c.y, c.z));
          // byte lightLevel = BlockTypes.lightLevel[c.b];
          // if (lightLevel > 0)
          // {
          //   if (lightSources.ContainsKey(new Vector3Int(c.x, c.y, c.z)))
          //     lightSources[new Vector3Int(c.x, c.y, c.z)] = lightLevel;
          //   else
          //     lightSources.Add(new Vector3Int(c.x, c.y, c.z), lightLevel);
          //   // lightSources.Add(new Vector3Int(c.x, c.y, c.z), lightLevel);
          // }
          // }

          // List<GameObject> children = saveData.children;
          // for (int i = 0; i < children.Count; ++i)
          // {
          //   GameObject child = children[i];
          //   // child.transform.SetParent(transform);
          //   child.transform.localPosition = Vector3.zero;
          //   child.transform.localRotation = Quaternion.identity;
          //   child.transform.localScale = Vector3.one;
          //   children[i] = child;
          // }
          chunkReady = true;
        }

        public void Modify(Vector3Int pos, ResourceLocation blockType)
        {
          Modify(pos.x, pos.y, pos.z, blockType);
        }

        public void Modify(int x, int y, int z, ResourceLocation blockType)
        {


          // if blockType is not found in tileIndexes
          if (!tileIndexes.Contains(blockType))
          {
            tileIndexes.Add(blockType);
            // Debug.Log("BEFORE ADDING TO TILECOUNTS " + tileCounts.Keys.Count);
            tileCounts.Add(tileCounts.Keys.Count, 0);
            // Debug.Log("AFTER ADDING TO TILECOUNTS " + tileCounts.Keys.Count);
          }


          // if (!chunkReady) throw new System.Exception("Chunk has not finished loading");
          // Debug.Log("Count of tileIndexes: " + tileIndexes.Count);
          // Debug.Log("Count of " + blockType + " in tileIndexes: " + tileIndexes.Where((ResourceLocation rl) => rl == blockType).Count());
          // if (tileCounts[tiles[x, y, z]] != null)


          // tileCounts[tiles[x, y, z]] -= 1;
          int i = tileIndexes.IndexOf(blockType);
          tiles[x, y, z] = i;
          tileCounts[i] += 1;
          CheckTileCounts();
        }

        private void CheckTileCounts()
        {
          tileCounts = tileCounts.Where((KeyValuePair<int, int> pair) => pair.Value > 0).ToDictionary(pair => pair.Key, pair => pair.Value);
          // foreach (KeyValuePair<int, int> pair in tileCounts)
          // {
          //   if (pair.Value <= 0)
          //   {
          //     tileCounts.Remove(pair.Key);
          //   }
          // }
        }

        private void CalculateTileCounts()
        {
          // don't need coordinate, so don't bother
          foreach (int tile in tiles)
          {
            // Debug.Log(tile);
            if (tileCounts.ContainsKey(tile))
            {
              tileCounts[tile] += 1;
            }
            else
            {
              tileCounts.Add(tile, 1);
            }
          }

        }



        public void Unload()
        {
          if (isDirty)
          {
            SaveDataManager.instance.Save(Save(), position);
          }
        }


        public CompoundTag Save()
        {
          CompoundTag tag = new CompoundTag("Chunk");
          tag.Add(new IntTag("x", position.x));
          tag.Add(new IntTag("y", position.y));
          tag.Add(new IntTag("z", position.z));

          // create tile index
          CalculateTileCounts(); // just in case
          List<int> tileKeys = tileCounts.Keys.ToList();
          List<IntTag> tileIndexTags = tileKeys.Select((int tile) => new IntTag(tile)).ToList();

          // rows
          List<ListTag> whys = new List<ListTag>();
          for (int y = 0; y < 20; ++y)
          {
            // columns
            List<ListTag> exes = new List<ListTag>();
            for (int x = 0; x < 20; ++x)
            {
              // layers
              ListTag zeds = new ListTag();
              for (int z = 0; z < 20; ++z)
              {
                zeds.Add(new IntTag(tileKeys.IndexOf(tiles[x, y, z])));
              }
              exes.Add(zeds);
            }
            whys.Add(new ListTag(exes));
          }

          tag.Add(new ListTag("tileIndex", tileIndexTags));
          tag.Add(new ListTag("tiles", whys));

          return tag;
        }

        public bool Load(CompoundTag tag)
        {

          if (tag == null) return false;
          position = new Vector3Int(tag.Get<IntTag>("x").Value, tag.Get<IntTag>("y").Value, tag.Get<IntTag>("z").Value);

          List<int> tileKeys = tag.Get<ListTag>("tileIndex").ToArray<IntTag>().ToList().ConvertAll((IntTag tileIndex) => tileIndex.Value);
          // .SelectMany((StringTag tileIndex) => new ResourceLocation(tileIndex.Value));

          tiles = new int[20, 20, 20];
          tileCounts = new Dictionary<int, int>();

          tag.Get<ListTag>("tiles").ToList().ForEach((NBTTag nbtExes) =>
          {
            List<ListTag> exes = ((ListTag)nbtExes).ToArray<ListTag>().ToList();
            exes.ForEach((ListTag zeds) =>
            {

              zeds.ToList().ForEach((NBTTag tileIndex) =>
              {
                tiles[exes.IndexOf(zeds), zeds.IndexOf(tileIndex), exes.IndexOf(zeds)] = tileKeys[((IntTag)tileIndex).Value];
              });
            });
          });

          CalculateTileCounts();
          return true;
          // saveData.changes = tag.Get<ListTag<CompoundTag>>("changes");
          // saveData.children = tag.Get<ListTag<CompoundTag>>("children");
        }
      }
    }
    */
  }
}