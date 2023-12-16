// See https://aka.ms/new-console-template for more information
using Dialogue;
using FishStick.Commands;
using FishStick.Player;
using FishStick.Render;
using FishStick.Scene;
using FishStick.Session;
using FishStick.World;

public class GameController
{
  private WorldController _world;
  private PlayerController _player;
  private DialogueController _dialogues;
  private CommandController _commandController;
  private SessionHistory _sessionHistory;

  public GameController()
  {
    _world = new();
    _player = new(20, "scene-0");
    _dialogues = new(_world, _player);
    _commandController = new(_player, _world, _dialogues);
    _sessionHistory = new();
  }

  public void Start()
  {
    Console.Clear();
    ConsoleController.WriteText("Welcome to {Project FishStick}!\n");
    // Initial scene description before we begin the main gameplay loop
    ConsoleController.DescribeScene(_world.GetScene(_player.GetCurrentSceneId()));
    GamePlayLoop();
    if (!_player.IsAlive)
    {
      HandleDeath(_world);
    }
  }

  private void GamePlayLoop()
  {
    while (_player.IsAlive)
    {
      string input = ConsoleController.ReadCommand(_sessionHistory);
      if (input.Length < 1)
      {
        continue;
      }
      // Simulate "thinking" time
      Thread.Sleep(100);
      _commandController.Execute(input);
    }
  }

  private void HandleDeath(WorldController world)
  {
    DeathScene? death = world.GetScene("death") as DeathScene;
    if (death == null)
    {
      throw new Exception("Death scene not found.");
    }
    ConsoleController.DescribeScene(death);
    while (true)
    {
      string input = ConsoleController.ReadCommand(_sessionHistory);
      // TODO: This is a bit "raw" but works for now, make it pretty later
      if (input == "quit")
      {
        _commandController.Execute("quit");
      }
      if (input == "restart")
      {
        ClearData();
        Start();
      }
    };
  }

  private void ClearData()
  {
    _world = new();
    _player = new(20, "scene-0");
    _dialogues = new(_world, _player);
    _commandController = new(_player, _world, _dialogues);
  }

}