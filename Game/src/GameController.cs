using Dialogue;
using FishStick.Commands;
using FishStick.Commands.Autocompletion;
using FishStick.Player;
using FishStick.Render;
using FishStick.Scene;
using FishStick.Session;
using FishStick.World;

try
{
  WorldController world = new();
  PlayerController player = new(20, "scene-0");
  DialogueController dialogues = new(world, player);
  CommandController commandController = new(player, world, dialogues);
  SessionHistory sessionHistory = new();

  IScene currentScene = world.GetScene(player.GetCurrentSceneId());

  // TODO: Make some initialization func or something, this is pretty ugly
  CommandAutocomplete commandAutocomplete = new();
  // register commands into autocompletion context
  commandAutocomplete.RegisterCommand(commandController.GetCommandKeywords());
  // register interactable scene objects - TODO: there should also be an update of these on scene changes in the future
  commandAutocomplete.RegisterCommand(currentScene.GetSceneItemsNames());
  commandAutocomplete.RegisterCommand(currentScene.GetSceneNPCsNames());
  commandAutocomplete.RegisterCommand(currentScene.GetSceneInteractableElementsNames());

  Console.Clear();
  ConsoleController.WriteText("Welcome to {Project FishStick}!\n");

  // Initial scene description before we begin the main gameplay loop
  ConsoleController.DescribeScene(currentScene);
  while (true)
  {
    string input = ConsoleController.ReadCommand(sessionHistory, commandAutocomplete);
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
