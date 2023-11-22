using FishStick.Item;
using FishStick.Scene;
using Scene;

namespace FishStick.Render
{
  class ConsoleController
  {
    public static void WriteText(string text)
    {
      ConsoleWriter.Write(text)
        .Slowly()
        .WithColor(ConsoleColor.DarkGray)
        .ToConsole();
      Console.WriteLine();
    }
    public static void DescribeScene(IScene scene)
    {
      string allText = scene.Description;

      foreach (ITransition transition in scene.Transitions)
      {
        allText += " " + transition.Description;
      }
      foreach (IItem item in scene.Items)
      {
        allText += " " + item.SceneDescription;
      }
      foreach (IElement element in scene.Elements)
      {
        if (element.Hidden) continue;
        allText += " " + element.SceneDescription;
      }
      ConsoleWriter.Write(allText)
        .Slowly()
        .WithColor(ConsoleColor.DarkGray)
        .ToConsole();
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
  }
  public class ConsoleWriter
  {
    private string _message = string.Empty;
    private ConsoleColor _foregroundColor = Console.ForegroundColor;
    private ConsoleColor _backgroundColor = Console.BackgroundColor;
    private bool _writeSlowly = false;
    private Dictionary<string, ConsoleColor> _highlightedPhrases = new Dictionary<string, ConsoleColor>();

    private ConsoleWriter() { }

    public static ConsoleWriter Write(string message)
    {
      return new ConsoleWriter { _message = message };
    }

    public void SetSlowly(bool slowly) => _writeSlowly = slowly;

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

    public ConsoleWriter WithHighlighting(Dictionary<string, ConsoleColor>? highlightedWords)
    {
      _highlightedPhrases = highlightedWords ?? _highlightedPhrases;
      return this;
    }

    public void ToConsole()
    {
      Console.ForegroundColor = _foregroundColor;
      Console.BackgroundColor = _backgroundColor;

      int currentPos = 0;

      while (currentPos < _message.Length)
      {
        int nextWordPos = _message.Length;
        ConsoleColor nextWordColor = _foregroundColor;
        string? nextPhrase = null;

        // Find the closest highlighted word
        foreach (var pair in _highlightedPhrases)
        {
          int pos = _message.IndexOf(value: pair.Key, startIndex: currentPos);
          if (pos >= 0 && pos < nextWordPos)
          {
            nextWordPos = pos;
            nextWordColor = pair.Value;
            nextPhrase = pair.Key;
          }
        }

        // Print text before the highlighted word
        WriteWord(_message.Substring(currentPos, nextWordPos - currentPos));

        // If a highlighted word is found, print it in its color
        if (nextPhrase != null)
        {
          Console.ForegroundColor = nextWordColor;
          WriteWord("[" + nextPhrase + "]");
          Console.ForegroundColor = _foregroundColor;

          currentPos = nextWordPos + nextPhrase.Length;
        }
        else
        {
          currentPos = _message.Length; // No more words to highlight
        }
      }

      Console.ResetColor();
      Console.Write(" "); // Trailing space.
    }

    private void WriteWord(string word)
    {
      // TODO: It would be nice to let the user press a button like space to
      // skip the slow typing effect. For that, this typing effect should
      // probably be in a separate thread.
      // Could use something like this https://stackoverflow.com/questions/62610803/c-sharp-manually-stopping-an-asynchronous-for-statement-typewriter-effect
      var ts = new CancellationTokenSource();
      CancellationToken ct = ts.Token;
      Task interruptSlow = Task.Run(() =>
      {
        while (true)
        {
          if (Console.KeyAvailable)
          {
            Console.ReadKey(true);
            _writeSlowly = false;
            break;
          }
          if (ct.IsCancellationRequested)
          {
            break;
          }
        }
      });
      for (int i = 0; i < word.Length; i++)
      {
        if (_writeSlowly)
        {
          Console.Write(word[i]);
          Thread.Sleep(20);
        }
        else
        {
          Console.Write(word[i..]);
          break;
        }
      }
      ts.Cancel();
    }
  }

}