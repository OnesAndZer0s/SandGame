namespace Sandbox.Common.Maths.Shapes
{
  public class Cylinder
  {
    public double Height;
    public double Radius;

    public Cylinder(double height = 1, double radius = 1)
    {
      Height = height;
      Radius = radius;
    }

    public double Volume()
    {
      return Math.PI * Math.Pow(Radius, 2) * Height;
    }

    public double SurfaceArea()
    {
      return (2 * Math.PI * Radius * Height) + (2 * Math.PI * Math.Pow(Radius, 2));
    }

    /* ---------- Static methods ---------- */
    public static double SurfaceArea(double height, double radius)
    {
      return (2 * Math.PI * radius * height) + (2 * Math.PI * Math.Pow(radius, 2));
    }

    public static double Volume(double height, double radius)
    {
      return Math.PI * Math.Pow(radius, 2) * height;
    }

    public static double RadiusFromSurfaceArea(double height, double surfaceArea)
    {
      return Math.Sqrt((surfaceArea - (2 * Math.PI * height)) / (2 * Math.PI));
    }

    public static double RadiusFromVolume(double height, double volume)
    {
      return Math.Sqrt(volume / (Math.PI * height));
    }

    public static double HeightFromSurfaceArea(double radius, double surfaceArea)
    {
      return (surfaceArea - (2 * Math.PI * Math.Pow(radius, 2))) / (2 * Math.PI * radius);
    }

    public static double HeightFromVolume(double radius, double volume)
    {
      return volume / (Math.PI * Math.Pow(radius, 2));
    }
  }
}