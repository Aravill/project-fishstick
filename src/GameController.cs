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
  // TODO: This loop is badly designed. The whole game loop could be generalized
  // as folows:
  // 0. Game reads a starting prompt to the user (like a description of the very first scene)
  // 1. Game waits for user input
  // 2. User inputs a command
  // 3. Game executes the command
  // 4. Positive or negative feedback is given to the user
  // 5. Game goes back to step 1
  // Making the loop as written above will allow us to delegate result handling
  // of commands to the command instances themselves instead of catching
  // errors on this level and deciding here.
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
    catch (ItemNotFoundException)
    {
      ConsoleController.WriteText($"There is no '{command}' in the room.");
    }
    catch (ItemUnspecifiedException)
    {
      ConsoleController.WriteText("Take what?");
    }
  }

}