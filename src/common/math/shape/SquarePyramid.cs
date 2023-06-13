namespace Sandbox.Common.Maths.Shapes
{
  public class SquarePyramid
  {
    public double BaseSideWidth;
    public double BaseSideHeight;
    public double Height;

    public SquarePyramid(double baseSideWidth = 1, double baseSideHeight = 1, double height = 1)
    {
      BaseSideWidth = baseSideWidth;
      BaseSideHeight = baseSideHeight;
      Height = height;
    }

    public double Volume()
    {
      return (1 / 3) * BaseSideWidth * BaseSideHeight * Height;
    }

    public double SurfaceArea()
    {
      return BaseSideWidth * BaseSideHeight + BaseSideWidth * Math.Sqrt(Math.Pow(Height, 2) + Math.Pow(BaseSideWidth / 2, 2)) + BaseSideHeight * Math.Sqrt(Math.Pow(Height, 2) + Math.Pow(BaseSideHeight / 2, 2));
    }

    /* ---------- Static methods ---------- */
    public static double SurfaceArea(double baseSideWidth, double baseSideHeight, double height)
    {
      return baseSideWidth * baseSideHeight + baseSideWidth * Math.Sqrt(Math.Pow(height, 2) + Math.Pow(baseSideWidth / 2, 2)) + baseSideHeight * Math.Sqrt(Math.Pow(height, 2) + Math.Pow(baseSideHeight / 2, 2));
    }

    public static double Volume(double baseSideWidth, double baseSideHeight, double height)
    {
      return (1 / 3) * baseSideWidth * baseSideHeight * height;
    }

    public static double BaseSideWidthFromSurfaceArea(double baseSideHeight, double height, double surfaceArea)
    {
      return (surfaceArea - (baseSideHeight * Math.Sqrt(Math.Pow(height, 2) + Math.Pow(baseSideHeight / 2, 2))) - (baseSideHeight * Math.Sqrt(Math.Pow(height, 2) + Math.Pow(baseSideHeight / 2, 2)))) / baseSideHeight;
    }

    public static double BaseSideHeightFromSurfaceArea(double baseSideWidth, double height, double surfaceArea)
    {
      return (surfaceArea - (baseSideWidth * Math.Sqrt(Math.Pow(height, 2) + Math.Pow(baseSideWidth / 2, 2))) - (baseSideWidth * Math.Sqrt(Math.Pow(height, 2) + Math.Pow(baseSideWidth / 2, 2)))) / baseSideWidth;
    }

    public static double HeightFromSurfaceArea(double baseSideWidth, double baseSideHeight, double surfaceArea)
    {
      return Math.Sqrt(Math.Pow(surfaceArea, 2) - (Math.Pow(baseSideWidth, 2) * Math.Pow(baseSideHeight, 2))) / Math.Sqrt(Math.Pow(baseSideWidth, 2) + Math.Pow(baseSideHeight, 2));
    }

    public static double BaseSideWidthFromVolume(double baseSideHeight, double height, double volume)
    {
      return Math.Sqrt((3 * volume) / (baseSideHeight * height));
    }

    public static double BaseSideHeightFromVolume(double baseSideWidth, double height, double volume)
    {
      return Math.Sqrt((3 * volume) / (baseSideWidth * height));
    }

    public static double HeightFromVolume(double baseSideWidth, double baseSideHeight, double volume)
    {
      return (3 * volume) / (baseSideWidth * baseSideHeight);
    }
  }
}