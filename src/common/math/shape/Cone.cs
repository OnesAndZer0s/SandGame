namespace Sandbox.Common.Maths.Shapes
{
  public class Cone
  {
    public double Height;
    public double Radius;

    public Cone(double height = 1, double radius = 1)
    {
      Height = height;
      Radius = radius;
    }

    public double Volume()
    {
      return (1 / 3) * Math.PI * Math.Pow(Radius, 2) * Height;
    }

    public double SurfaceArea()
    {
      return Math.PI * Radius * (Radius + Math.Sqrt(Math.Pow(Height, 2) + Math.Pow(Radius, 2)));
    }

    /* ---------- Static methods ---------- */
    public static double SurfaceArea(double height, double radius)
    {
      return Math.PI * radius * (radius + Math.Sqrt(Math.Pow(height, 2) + Math.Pow(radius, 2)));
    }

    public static double Volume(double height, double radius)
    {
      return (1 / 3) * Math.PI * Math.Pow(radius, 2) * height;
    }

    public static double RadiusFromSurfaceArea(double height, double surfaceArea)
    {
      return Math.Sqrt((Math.Pow(surfaceArea, 2) - (4 * Math.PI * Math.Pow(height, 2))) / (4 * Math.PI));
    }

  }
}