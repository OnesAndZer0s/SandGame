
namespace Sandbox.Common.Mods
{
  public class Mod
  {
    public string ModID { get; private set; }

    public string Name { get; private set; }
    public string Description { get; private set; }

    public string Author { get; private set; }

    public string? URL { get; private set; }

    public string[]? Dependencies { get; private set; }

    public int Major { get; private set; }
    public int Minor { get; private set; }
    public int Patch { get; private set; }

    public bool isEnabled = true;

    public Mod(
      string modid,
      string name,
      int major = 0,
      int minor = 0,
      int patch = 1,
      string description = "This is the start of something great.",
      string author = "You!",
      string? url = null,
      string[]? dependencies = null
    )
    {
      ModID = modid;
      Name = name;
      Major = major;
      Minor = minor;
      Patch = patch;
      Description = description;
      Author = author;
      URL = url;
      Dependencies = dependencies;
    }

    public virtual async void PreInit(ModEventBus eventBus) { }

    public virtual async void Init(ModEventBus eventBus) { }

    public virtual async void PostInit(ModEventBus eventBus) { }
  }
}