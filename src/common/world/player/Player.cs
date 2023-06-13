using Sandbox.Common.Worlds.Entities;
using Sandbox.Common.Worlds.Levels;

namespace Sandbox.Common.Worlds.Players
{
  public class Player : LivingEntity
  {

    public Level Level { get; private set; }
    public Player()
    {
    }
  }
}