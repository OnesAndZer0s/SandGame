
using OpenTK.Mathematics;

namespace Sandbox.Common.Maths
{
  public class AABB
  {
    public Vector3d Min;
    public Vector3d Max;

    public Vector3d Center => (Min + Max) / 2;
    public Vector3d Size => Max - Min;

    public AABB(Vector3d min, Vector3d max)
    {
      Min = min;
      Max = max;
      Fix();
    }

    public AABB(double minX, double minY, double minZ, double maxX, double maxY, double maxZ)
    {
      Min = new(minX, minY, minZ);
      Max = new(maxX, maxY, maxZ);
    }

    public AABB(double minX, double minY, double minZ, double size)
    {
      Min = new(minX, minY, minZ);
      Max = new(minX + size, minY + size, minZ + size);
    }

    public AABB(Vector3 min, double size)
    {
      Min = min;
      Max = new(min.X + size, min.Y + size, min.Z + size);
    }

    public void Fix()
    {
      if (Min.X > Max.X)
      {
        (Max.X, Min.X) = (Min.X, Max.X);
      }
      if (Min.Y > Max.Y)
      {
        (Max.Y, Min.Y) = (Min.Y, Max.Y);
      }
      if (Min.Z > Max.Z)
      {
        (Max.Z, Min.Z) = (Min.Z, Max.Z);
      }
    }

    public bool Contains(Vector3 point)
    {
      return point.X >= Min.X && point.X <= Max.X &&
             point.Y >= Min.Y && point.Y <= Max.Y &&
             point.Z >= Min.Z && point.Z <= Max.Z;
    }

    public bool Contains(AABB other)
    {
      return other.Min.X >= Min.X && other.Max.X <= Max.X &&
             other.Min.Y >= Min.Y && other.Max.Y <= Max.Y &&
             other.Min.Z >= Min.Z && other.Max.Z <= Max.Z;
    }

    public bool Intersects(AABB other)
    {
      return other.Min.X <= Max.X && other.Max.X >= Min.X &&
             other.Min.Y <= Max.Y && other.Max.Y >= Min.Y &&
             other.Min.Z <= Max.Z && other.Max.Z >= Min.Z;
    }

    public override string ToString()
    {
      return $"AABB({Min}, {Max})";
    }
  }
}