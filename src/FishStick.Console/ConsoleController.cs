using FishStick.Item;
using FishStick.Scene;
using System.Collections.Generic;

namespace FishStick.Render
{
  class ConsoleController
  {
    private static readonly Dictionary<string, ConsoleColor> _keywords = new()
    {
      { "east", ConsoleColor.Red },
      { "key", ConsoleColor.Blue }
    };


    public static void WriteText(string text)
    {
      WriteSlowly(text)();
      Console.WriteLine();
    }
    public static void DescribeScene(IScene scene)
    {
      ConsoleWriter.Write(scene.Description)
        .Slowly()
        .WithHighlighting(_keywords)
        .WithColor(ConsoleColor.DarkGray)
        .ToConsole();

      foreach (ITransition transition in scene.Transitions)
      {
        ConsoleWriter.Write(transition.Description)
        .Slowly()
        .WithHighlighting(_keywords)
        .WithColor(ConsoleColor.Yellow)
        .ToConsole();
      }
      foreach (IItem item in scene.Items)
      {
        ConsoleWriter.Write(item.SceneDescription)
        .Slowly()
        .WithHighlighting(_keywords)
        .WithColor(ConsoleColor.DarkGray)
        .ToConsole();
      }
      Console.WriteLine();
    }

    public static string ReadCommand()
    {
      string? commandText = null;
      while (commandText == null)
      {
        commandText = Console.ReadLine();
        if (commandText == null)
        {
          continue;
        }
      }
      return commandText;
    }

    private static Action WriteSlowly(string message) => () =>
    {
      foreach (char c in message)
      {

        // TODO: It would be nice to let the user press a button like space to
        // skip the slow typing effect. For that, this typing effect should
        // probably be in a separate thread.
        // Could use something like this https://stackoverflow.com/questions/62610803/c-sharp-manually-stopping-an-asynchronous-for-statement-typewriter-effect
        Console.Write(c);
        Thread.Sleep(20);
      }
    };
  }
  public class ConsoleWriter
  {
    private string _message = string.Empty;
    private ConsoleColor _foregroundColor = Console.ForegroundColor;
    private ConsoleColor _backgroundColor = Console.BackgroundColor;
    private bool _writeSlowly = false;
    private Dictionary<string, ConsoleColor> _highlightedWords = new Dictionary<string, ConsoleColor>();

    private ConsoleWriter() { }

    public static ConsoleWriter Write(string message)
    {
      return new ConsoleWriter { _message = message };
    }

    public ConsoleWriter Slowly()
    {
      _writeSlowly = true;
      return this;
    }

    public ConsoleWriter WithColor(ConsoleColor foregroundColor, ConsoleColor? backgroundColor = null)
    {
      _foregroundColor = foregroundColor;
      _backgroundColor = backgroundColor ?? _backgroundColor;
      return this;
    }

    public ConsoleWriter WithHighlighting(Dictionary<string, ConsoleColor> highlightedWords)
    {
      _highlightedWords = highlightedWords;
      return this;
    }

    public void ToConsole()
    {
      Console.ForegroundColor = _foregroundColor;
      Console.BackgroundColor = _backgroundColor;

      string[] words = _message.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

      foreach (var word in words)
      {
        var keyword = word.Trim(new[] { ',', '.' });
        bool isHighlighted = _highlightedWords.TryGetValue(keyword, out var highlightColor);
        Console.ForegroundColor = isHighlighted ? highlightColor : _foregroundColor;

        WriteWord(word, isHighlighted);

        Console.Write(" ");
      }

      Console.ResetColor();
    }

    private void WriteWord(string word, bool isHighlighted)
    {
      foreach (char c in word)
      {
        Console.Write(c);
        if (_writeSlowly)
        {
          Thread.Sleep(20);
        }
      }
    }
  }

}