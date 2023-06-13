namespace Sandbox.Common.Maths.Shapes
{
  public class Sphere
  {
    public double Radius;
    public Sphere(double radius = 1)
    {
      Radius = radius;
    }

    public double Volume()
    {
      return (4 / 3) * Math.PI * Math.Pow(Radius, 3);
    }

    public double SurfaceArea()
    {
      return 4 * Math.PI * Math.Pow(Radius, 2);
    }

    public double Circumference()
    {
      return 2 * Math.PI * Radius;
    }

    public double Diameter()
    {
      return 2 * Radius;
    }

    /* ---------- Static methods ---------- */
    public static double Diameter(double radius)
    {
      return 2 * radius;
    }

    public static double Circumference(double radius)
    {
      return 2 * Math.PI * radius;
    }

    public static double SurfaceArea(double radius)
    {
      return 4 * Math.PI * Math.Pow(radius, 2);
    }

    public static double Volume(double radius)
    {
      return (4 / 3) * Math.PI * Math.Pow(radius, 3);
    }

    public static double RadiusFromSurfaceArea(double surfaceArea)
    {
      return Math.Sqrt(surfaceArea / (4 * Math.PI));
    }

    public static double RadiusFromCircumference(double circumference)
    {
      return circumference / (2 * Math.PI);
    }

    public static double RadiusFromVolume(double volume)
    {
      return Math.Pow((3 * volume) / (4 * Math.PI), 1 / 3);
    }
  }
}