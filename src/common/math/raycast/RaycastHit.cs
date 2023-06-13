using OpenTK.Mathematics;

namespace Sandbox.Common.Maths.Raycasts
{
  public class RaycastHit
  {
    public Vector3 barycentricCoords { get; private set; }
    // collider
    public float distance { get; private set; }
    public Vector3 normal { get; private set; }
    public Vector3 point { get; private set; }
    // rigidbody
    //texture coord
    // transform
    // triangle idea
  }
}