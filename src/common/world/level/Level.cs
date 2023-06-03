namespace Sandbox.Common.Worlds.Levels
{
  public class Level
  {
    public Level()
    {
    }
  }
}



/*
﻿using System.Collections;
using System.Collections.Generic;
using Sandbox.Worlds.Chunks;
using Sandbox.Worlds.Tiles;
using Sandbox.Worlds;
using UnityEngine;
using Sandbox.Core;
using Sandbox.Worlds.Levels.Tiles.States;
using Sandbox.Core.Registries;

namespace Sandbox
{
  public class Level : MonoBehaviour
  {
    public static Level activeWorld;
    public WorldInfo info;
    public Camera mainCamera;
    public ChunkManager chunkManager;
    private bool initialized = false;
    public TMPro.TextMeshProUGUI debugText;
    public void Initialize(WorldInfo info)
    {
      this.info = info;
      if (info.seed == 0) info.seed = GenerateSeed();
      activeWorld = this;
      chunkManager.Initialize();
      SimplexNoise.Noise.Seed = info.seed;
      System.GC.Collect();
      initialized = true;
    }
    void LateUpdate()
    {
      if (!initialized) return;
      debugText.text = "Seed: " + info.seed;
      //update chunks if no modifications have happened this frame
      //only rebuild 1 chunk per frame to avoid framedrops
      chunkManager.UpdateChunks(mainCamera);
    }

    public bool Modify(Vector3Int pos, Tile tile)
    {
      return Modify(pos, Registries.Get<Tile>(BuiltInRegistries.TILE).GetKey(tile));
    }

    public bool Modify(Vector3Int pos, ResourceLocation tile)
    {
      if (!initialized) return false;

      Vector3Int chunkPos = Vector3Int.FloorToInt(pos / 20);

      // TODO
      // Tile tile = ModManager.Get("tile", GetTile(x, y, z));
      // if (tile == null) return false;
      // tile.OnRemove(this, new Vector3Int(x, y, z), tile.defaultTileState);

      return chunkManager.Modify(chunkPos, pos - chunkPos, tile);

    }



    public bool SetTile(Vector3 pos, TileState state, int p_46607_)
    {
      return SetTile(pos, state, p_46607_, 512);
    }


    public bool SetTile(Vector3 pos, TileState state, int p_46607_, int p_46608_)
    {
      // if (this.isOutsideBuildHeight(pos))
      // {
      //   return false;
      // }
      // if (!this.isClientSide && this.isDebug())
      // {
      //   return false;
      // }
      LevelChunk levelchunk = chunkManager.levelChunkManager.GetChunkWithPosition(pos);
      Tile block = state.GetTile();
      // pos = pos.immutable();
      // BlockSnapshot blockSnapshot = null;
      // if (this.captureBlockSnapshots && !this.isClientSide)
      // {
      //   blockSnapshot = BlockSnapshot.create(this.dimension, this, pos, p_46607_);
      //   this.capturedBlockSnapshots.add(blockSnapshot);
      // }
      TileState old = this.GetTileState(pos);
      // final int oldLight = old.getLightEmission(this, pos);
      // final int oldOpacity = old.getLightBlock(this, pos);
      // TileState blockstate = levelchunk.SetTileState(pos, state, (p_46607_ & 0x40) != 0x0);
      // if (blockstate == null)
      // {
      //   if (blockSnapshot != null)
      //   {
      //     this.capturedBlockSnapshots.remove(blockSnapshot);
      //   }
      //   return false;
      // }
      // final BlockState blockstate2 = this.GetTileState(pos);
      // if ((p_46607_ & 0x80) == 0x0 && blockstate2 != blockstate && (blockstate2.getLightBlock(this, pos) != oldOpacity || blockstate2.getLightEmission(this, pos) != oldLight || blockstate2.useShapeForLightOcclusion() || blockstate.useShapeForLightOcclusion()))
      // {
      //   this.getProfiler().push("queueCheckLight");
      //   this.getChunkSource().getLightEngine().checkBlock(pos);
      //   this.getProfiler().pop();
      // }
      // if (blockSnapshot == null)
      // {
      //   this.markAndNotifyBlock(pos, levelchunk, blockstate, p_46606_, p_46607_, p_46608_);
      // }
      return true;
    }

    public bool IsClientSide()
    {
      return true;
    }

    public TileState GetTileState(Vector3 p_46732_)
    {
      Chunk levelchunk = chunkManager.GetChunkWithPosition(p_46732_);
      return levelchunk.GetTileState(p_46732_);
    }
    public bool Remove(int x, int y, int z)
    {
      return Remove(new Vector3Int(x, y, z));
    }

    public bool Remove(Vector3Int pos)
    {
      return Modify(pos, "core:air");
    }

    public string GetTile(int x, int y, int z)
    {
      return GetTile(new Vector3Int(x, y, z));

    }

    public string GetTile(Vector3Int pos)
    {
      if (!initialized) return "core:air";
      // TODO better
      Vector3Int chunkPos = Vector3Int.FloorToInt(pos / 20);


      return chunkManager.GetTile(chunkPos, pos - chunkPos);
    }

    private int GenerateSeed()
    {
      int tickCount = System.Environment.TickCount;
      int processId = System.Diagnostics.Process.GetCurrentProcess().Id;
      return new System.Random(tickCount + processId).Next();
    }

  }
}

*/