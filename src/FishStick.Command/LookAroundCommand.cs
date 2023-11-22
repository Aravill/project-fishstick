using FishStick.Player;
using FishStick.Render;
using FishStick.World;

namespace FishStick.Commands
{
  class LookAroundCommand(PlayerController player, WorldController world) : ICommand
  {
    private PlayerController _player = player;
    private WorldController _world = world;
    public static string Name = "look"; // TODO: "look around" is better but we currently do not allow multi-word commands (see CommandController.cs)
    void ICommand.Execute(string[] args)
    {
      ConsoleController.DescribeScene(_world.GetScene(_player.GetCurrentSceneId()));
    }
  }
}
