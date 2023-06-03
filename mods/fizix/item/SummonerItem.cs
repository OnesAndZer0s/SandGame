

public class SummonerItem : Item
{


  public override InteractionResult UseOn(UseOnContext useOn)
  {
    if (useOn.GetHit())
    {
      // do the routine of turning that block into a physics entity
      Vector3Int pos = Vector3Int.FloorToInt(useOn.GetHitResult().point - (useOn.GetHitResult().normal * 0.5f));
      Debug.Log("SummonerItem.UseOn");
      ResourceLocation tile = useOn.GetLevel().GetTile(pos);
      if (tile == "core:air")
      {
        return InteractionResult.PASS;
      }
      Debug.Log(tile);
      useOn.GetLevel().Remove(pos);

      // DEBUG - spawn a cube
      GameObject cube = new GameObject(tile);
      cube.layer = LayerMask.NameToLayer("Ground");
      cube.transform.position = pos + (Vector3.one / 2);
      cube.transform.localScale = new Vector3(1 / 20f, 1 / 20f, 1 / 20f);


      // Rigidbody rb = cube.AddComponent<Rigidbody>();

      // rb.mass = 1;
      // rb.drag = 1;
      // rb.angularDrag = 0.05f;
      // rb.useGravity = true;
      // rb.isKinematic = false;
      // rb.interpolation = RigidbodyInterpolation.None;
      // rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
      // rb.constraints = RigidbodyConstraints.None;

      Entity e = cube.AddComponent<Entity>();

      cube.AddComponent<MeshRenderer>().material = GameObject.Find("Chunk").GetComponent<MeshRenderer>().material;
      cube.AddComponent<MeshFilter>().mesh = TileModelManager.GetMesh(tile.WithPrefix("tile"), GameManager.Instance.PLACEHOLDERTINTINDEX);

      MeshCollider mc = cube.AddComponent<MeshCollider>();
      mc.convex = true;
      return InteractionResult.SUCCESS;
    }
    return InteractionResult.PASS;
  }
}