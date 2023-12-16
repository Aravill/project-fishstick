using FishStick.Player;
using FishStick.Render;
using FishStick.Scene;
using FishStick.World;

namespace FishStick.Commands
{
  class GoCommand(PlayerController player, WorldController world) : ICommand
  {
    private PlayerController _player = player;
    private WorldController _world = world;
    public static string Name = "go";

    void ICommand.Execute(string[] args)
    {
      string targetSceneName = args[0];
      IScene currentScene = _world.GetScene(_player.GetCurrentSceneId());
      ITransition? transition = currentScene
        .Transitions
        .Find(transition => transition.Name == targetSceneName);
      if (transition is null)
      {
        ConsoleController.WriteText($"You cannot go '{targetSceneName}' from here.");
        return;
      }
      _player.SetCurrentSceneId(transition.NextSceneId);
      ConsoleController.DescribeScene(_world.GetScene(_player.GetCurrentSceneId()));
    }
  }
}
