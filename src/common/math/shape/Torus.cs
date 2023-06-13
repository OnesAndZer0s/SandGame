namespace Sandbox.Common.Maths.Shapes
{
  public class Torus
  {
    public double MajorRadius;
    public double MinorRadius;
    public Torus(double majorRadius = 1, double minorRadius = 1)
    {
      MajorRadius = majorRadius;
      MinorRadius = minorRadius;
    }

    public double Volume()
    {
      return 2 * Math.PI * Math.Pow(MajorRadius, 2) * MinorRadius;
    }

    public double SurfaceArea()
    {
      return 4 * Math.PI * Math.Pow(MajorRadius, 2);
    }

    /* ---------- Static methods ---------- */
    public static double SurfaceArea(double majorRadius)
    {
      return 4 * Math.PI * Math.Pow(majorRadius, 2);
    }

    public static double Volume(double majorRadius, double minorRadius)
    {
      return 2 * Math.PI * Math.Pow(majorRadius, 2) * minorRadius;
    }

    public static double MajorRadiusFromSurfaceArea(double surfaceArea)
    {
      return Math.Sqrt(surfaceArea / (4 * Math.PI));
    }

    public static double MinorRadiusFromVolume(double majorRadius, double volume)
    {
      return volume / (2 * Math.PI * Math.Pow(majorRadius, 2));
    }
  }
}