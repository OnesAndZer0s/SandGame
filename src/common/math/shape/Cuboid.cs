namespace Sandbox.Common.Maths.Shapes
{
  public class Cuboid
  {
    public double Width;
    public double Height;
    public double Depth;
    public Cuboid(double width = 1, double height = 1, double depth = 1)
    {
      Width = width;
      Height = height;
      Depth = depth;
    }

    public double Volume()
    {
      return Width * Height * Depth;
    }

    public double SurfaceArea()
    {
      return (2 * Width * Height) + (2 * Width * Depth) + (2 * Height * Depth);
    }

    public double Perimeter()
    {
      return 4 * (Width + Height + Depth);
    }

    /* ---------- Static methods ---------- */
    public static double Perimeter(double width, double height, double depth)
    {
      return 4 * (width + height + depth);
    }

    public static double SurfaceArea(double width, double height, double depth)
    {
      return (2 * width * height) + (2 * width * depth) + (2 * height * depth);
    }

    public static double Volume(double width, double height, double depth)
    {
      return width * height * depth;
    }

    public static double WidthFromSurfaceArea(double height, double depth, double surfaceArea)
    {
      return (surfaceArea - (2 * height * depth)) / (2 * height + 2 * depth);
    }

    public static double WidthFromVolume(double height, double depth, double volume)
    {
      return volume / (height * depth);
    }

    public static double HeightFromSurfaceArea(double width, double depth, double surfaceArea)
    {
      return (surfaceArea - (2 * width * depth)) / (2 * width + 2 * depth);
    }

    public static double HeightFromVolume(double width, double depth, double volume)
    {
      return volume / (width * depth);
    }

    public static double DepthFromSurfaceArea(double width, double height, double surfaceArea)
    {
      return (surfaceArea - (2 * width * height)) / (2 * width + 2 * height);
    }

    public static double DepthFromVolume(double width, double height, double volume)
    {
      return volume / (width * height);
    }

  }
}