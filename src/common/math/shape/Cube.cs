namespace Sandbox.Common.Maths.Shapes
{
  public class Cube
  {
    public double SideLength;
    public Cube(double sideLength = 1)
    {
      SideLength = sideLength;
    }

    public double Volume()
    {
      return Math.Pow(SideLength, 3);
    }

    public double SurfaceArea()
    {
      return 6 * Math.Pow(SideLength, 2);
    }

    public double Perimeter()
    {
      return 12 * SideLength;
    }

    public double EdgeLength()
    {
      return SideLength;
    }

    /* ---------- Static methods ---------- */
    public static double EdgeLength(double sideLength)
    {
      return sideLength;
    }

    public static double Circumference(double sideLength)
    {
      return 12 * sideLength;
    }

    public static double SurfaceArea(double sideLength)
    {
      return 6 * Math.Pow(sideLength, 2);
    }

    public static double Volume(double sideLength)
    {
      return Math.Pow(sideLength, 3);
    }

    public static double SideLengthFromSurfaceArea(double surfaceArea)
    {
      return Math.Sqrt(surfaceArea / 6);
    }

    public static double SideLengthFromCircumference(double circumference)
    {
      return circumference / 12;
    }

    public static double SideLengthFromVolume(double volume)
    {
      return Math.Pow(volume, 1 / 3);
    }
  }
}