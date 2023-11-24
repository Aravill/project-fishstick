// See https://aka.ms/new-console-template for more information
using FishStick.Commands;
using FishStick.Player;
using FishStick.Render;
using FishStick.Session;
using FishStick.World;

WorldController world = new();
PlayerController player = new(20);
CommandController commandController = new(player, world);
SessionHistory sessionHistory = new();

ConsoleController.WriteText("Welcome to ProjectFishStick!\n");

// Initial scene description before we begin the main gameplay loop
ConsoleController.DescribeScene(world.GetScene(player.GetCurrentSceneId()));
while (true)
{
  string input = ConsoleController.ReadCommand(sessionHistory);
  sessionHistory.Add(input);
  if (input.Length < 1)
  {
    continue;
  }
  commandController.Execute(input);
}