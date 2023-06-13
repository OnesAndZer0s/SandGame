using OpenTK.Mathematics;

namespace Sandbox.Common.Maths.Physics
{
  public class PhysicsObject
  {

    public Vector3 Position;
    public Vector3 Velocity;
    public Vector3 Acceleration;
    public Vector3 NetForce;

    public float Mass;

    public PhysicsObject(float mass = 1, Vector3 pos = new())
    {
      Mass = mass;
      Position = pos;
      Velocity = new();
      Acceleration = new();
    }

    public void Tick(float delta)
    {
      Velocity += Acceleration * delta;
      Position += Velocity * delta;
    }
  }
}