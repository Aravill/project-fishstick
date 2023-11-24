using FishStick.Player;
using FishStick.World;

namespace FishStick.Scripts
{
  class ScriptController
  {
    public void ExecuteScript(PlayerController player, WorldController world, string scriptName, string[] args)
    {
      switch (scriptName)
      {
        case "RevealScript":
          new RevealScript().Execute(player, world, args);
          break;
        default:
          return;
      }
    }
  }
}
