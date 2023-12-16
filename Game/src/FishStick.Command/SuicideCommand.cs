using FishStick.Player;
using FishStick.Render;
using FishStick.Scene;
using FishStick.World;

namespace FishStick.Commands
{
  class SuicideCommand(PlayerController player, WorldController world) : ICommand
  {
    private PlayerController _player = player;
    private WorldController _world = world;
    public static string Name = "suicide";

    void ICommand.Execute(string[] args)
    {
      _player.TakeDamage(999999);
    }
  }
}
