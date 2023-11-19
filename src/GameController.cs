// See https://aka.ms/new-console-template for more information
using FishStick.Commands;
using FishStick.Exception;
using FishStick.Player;
using FishStick.Render;
using FishStick.World;

WorldController world = new();
PlayerController player = new(20);
CommandController commandController = new(player, world);

ConsoleController.WriteText("Welcome to ProjectFishStick!");

while (true)
{
  ConsoleController.DescribeScene(world.GetScene(player.GetCurrentSceneId()));
  bool commandSuccess = false;
  // TODO: Maybe this loop could be inside the CommandController?
  while (!commandSuccess)
  {
    string command = ConsoleController.ReadCommand();
    try
    {
      commandController.Execute(command);
      commandSuccess = true;
    }
    // TODO: This exception could be generalized, basically any user input
    // error should be caught here and the command input loop should continue.
    catch (TransitionNotFoundException)
    {
      // TODO: This will print out the whole command, for 'go' command we need just the target scene argument.
      ConsoleController.WriteText($"I don't know where '{command}' is.");
    }
    catch (CommandNotFoundException)
    {
      ConsoleController.WriteText($"I don't understand what you mean by '{command}'.");
    }
  }

}