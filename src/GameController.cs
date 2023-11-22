// See https://aka.ms/new-console-template for more information
using FishStick.Commands;
using FishStick.Player;
using FishStick.Render;
using FishStick.World;

WorldController world = new();
PlayerController player = new(20);
CommandController commandController = new(player, world);

ConsoleController.WriteText("Welcome to ProjectFishStick!\n");

// Initial scene description before we begin the main gameplay loop
ConsoleController.DescribeScene(world.GetScene(player.GetCurrentSceneId()));
while (true)
{
  string command = ConsoleController.ReadCommand();
  if (command.Length < 1)
  {
    continue;
  }
  commandController.Execute(command);
}