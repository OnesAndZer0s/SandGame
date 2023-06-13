namespace Sandbox.Common.Maths.Shapes
{
  public class Pyramid
  {
    public double BaseWidth;
    public double BaseHeight;
    public double Height;

    public Pyramid(double width = 1, double height = 1, double depth = 1)
    {
      BaseWidth = width;
      BaseHeight = height;
      Height = depth;
    }

    public double Volume()
    {
      return (1 / 3) * BaseWidth * BaseHeight * Height;
    }

    public double SurfaceArea()
    {
      return BaseWidth * BaseHeight + BaseWidth * Math.Sqrt(Math.Pow(Height, 2) + Math.Pow(BaseHeight / 2, 2)) + Height * Math.Sqrt(Math.Pow(BaseWidth, 2) + Math.Pow(BaseHeight / 2, 2));
    }

    /* ---------- Static methods ---------- */
    public static double SurfaceArea(double width, double height, double depth)
    {
      return width * height + width * Math.Sqrt(Math.Pow(depth, 2) + Math.Pow(height / 2, 2)) + depth * Math.Sqrt(Math.Pow(width, 2) + Math.Pow(height / 2, 2));
    }

    public static double Volume(double width, double height, double depth)
    {
      return (1 / 3) * width * height * depth;
    }

    public static double WidthFromSurfaceArea(double height, double depth, double surfaceArea)
    {
      return (surfaceArea - (height * Math.Sqrt(Math.Pow(depth, 2) + Math.Pow(height / 2, 2))) - (height * Math.Sqrt(Math.Pow(depth, 2) + Math.Pow(height / 2, 2)))) / height;
    }

    public static double WidthFromVolume(double height, double depth, double volume)
    {
      return Math.Sqrt((3 * volume) / (height * depth));
    }

    public static double HeightFromSurfaceArea(double width, double depth, double surfaceArea)
    {
      return Math.Sqrt(Math.Pow(surfaceArea, 2) - (Math.Pow(width, 2) * Math.Pow(depth, 2))) / Math.Sqrt(Math.Pow(width, 2) + Math.Pow(depth, 2));
    }

    public static double HeightFromVolume(double width, double depth, double volume)
    {
      return Math.Sqrt((3 * volume) / (width * depth));
    }

  }
}