// See https://aka.ms/new-console-template for more information
using Dialogue;
using FishStick.Commands;
using FishStick.Player;
using FishStick.Render;
using FishStick.Session;
using FishStick.World;

try
{
  WorldController world = new();
  PlayerController player = new(20, "scene-0");
  DialogueController dialogues = new(world, player);
  CommandController commandController = new(player, world, dialogues);
  SessionHistory sessionHistory = new();

  Console.Clear();
  ConsoleController.WriteText("Welcome to {Project FishStick}!\n");

  // Initial scene description before we begin the main gameplay loop
  ConsoleController.DescribeScene(world.GetScene(player.GetCurrentSceneId()));
  while (true)
  {
    string input = ConsoleController.ReadCommand(sessionHistory);
    if (input.Length < 1)
    {
      continue;
    }
    // Simulate "thinking" time
    Thread.Sleep(100);
    commandController.Execute(input);
  }
}
catch (Exception exception)
{
  Console.WriteLine(exception.Message);
  Console.WriteLine(exception.StackTrace);
  Console.CursorVisible = true;
  Environment.Exit(0);
}
