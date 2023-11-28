using System.Text.RegularExpressions;
using System.Xml;
using FishStick.Item;
using FishStick.Scene;
using FishStick.Session;
using Scene;

namespace FishStick.Render
{
  class ConsoleController
  {

    public class GameCursor
    {
      public char cursorSymbol = '▮';
      public int cursorIndex = 0;
    }
    public static void WriteText(string text)
    {
      string withoutTags = text.FindTags(out var tags)
                               .RemoveTagMarkers();

      ConsoleWriter.Write(withoutTags)
        .Slowly()
        .WithColor(ConsoleColor.DarkGray)
        .WithHighlighting(tags.ToDictionary(tag => tag, tag => ConsoleColor.DarkYellow))
        .ToConsole();

      Console.WriteLine();
    }
    public static void DescribeScene(IScene scene)
    {
      var description = scene.Description;
      var transitions = scene.Transitions.Select(t => t.Description);
      var items = scene.Items.Where(i => !i.Hidden).Select(i => i.SceneDescription);
      var elements = scene.Elements.Where(e => !e.Hidden).Select(e => e.SceneDescription);

      var textList = description.Concat(transitions)
                                .Concat(items)
                                .Concat(elements);

      var allText = string.Join(' ', textList)
                          .FindTags(out var tags)
                          .RemoveTagMarkers();

      ConsoleWriter.Write(allText)
        .Slowly()
        .WithColor(ConsoleColor.DarkGray)
        .WithHighlighting(tags.ToDictionary(tag => tag, tag => ConsoleColor.DarkYellow))
        .ToConsole();

      Console.WriteLine();
    }
    public static string ReadCommand()
    {
      string? commandText;
      while (string.IsNullOrWhiteSpace(commandText = Console.ReadLine())) ;

      return commandText!;
    }
  public static string ReadCommand(SessionHistory history)
  {
    Console.CursorVisible = false;
    string? finalInput = null;
    GameCursor cursor = new();
    // Fake the cursor
    string writtenInput = cursor.cursorSymbol.ToString();
    Console.Write(writtenInput);
    do
    {
      ConsoleKeyInfo readKeyResult = Console.ReadKey(true);
      switch (readKeyResult.Key)
      {
        case ConsoleKey.UpArrow:
          ClearCurrentConsoleLine();
          writtenInput = history.GetPrevious() + cursor.cursorSymbol;
          Console.Write(writtenInput);
          cursor.cursorIndex = writtenInput.Length - 1;
          break;
        case ConsoleKey.DownArrow:
          ClearCurrentConsoleLine();
          writtenInput = history.GetNext() + cursor.cursorSymbol;
          Console.Write(writtenInput);
          cursor.cursorIndex = writtenInput.Length - 1;
          break;
        case ConsoleKey.Backspace:
          if (cursor.cursorIndex > 0)
          {
            ClearCurrentConsoleLine();
            // Remove the cursor symbol + last character from the end of the string
            if (cursor.cursorIndex == 0)
            {
              break;
            }
            // Remove the symbol before the cursor
            writtenInput = writtenInput.Remove(cursor.cursorIndex - 1, 1);
            cursor.cursorIndex--;
            Console.Write(writtenInput);
          }
          break;
        case ConsoleKey.Enter:
          // Don't do anything if the user hasn't typed anything yet
          if (writtenInput.Length > 1)
          {
            ClearCurrentConsoleLine();
            finalInput = RemoveCursor(writtenInput, cursor.cursorIndex);
            Console.Write(finalInput);
          }
          break;
        case ConsoleKey.LeftArrow:
          ClearCurrentConsoleLine();
          writtenInput = MoveCursor(false, writtenInput, cursor);
          Console.Write(writtenInput);
          break;
        case ConsoleKey.RightArrow:
          ClearCurrentConsoleLine();
          writtenInput = MoveCursor(true, writtenInput, cursor);
          Console.Write(writtenInput);
          break;
        default:
          // Clear what's been written so far
          ClearCurrentConsoleLine();
          // Add the new character + cursor symbol and add it to the written input
          string added = readKeyResult.KeyChar.ToString();
          writtenInput = writtenInput.Insert(cursor.cursorIndex, added);
          cursor.cursorIndex++;
          Console.Write(writtenInput);
          break;
      }
    }
    while (finalInput == null || finalInput.Length < 1);
    Console.WriteLine();
    history.Add(finalInput);
    Console.CursorVisible = true;
    return finalInput;
  }

  private static string MoveCursor(bool forward, string text, GameCursor cursor)
  {
    // Make sure we don't go out of bounds
    if (cursor.cursorIndex == 0 && !forward) return text;
    // -2 because our cursor is at the end of the string, always addind 1 additional symbol and the user cannot move past itself
    if (cursor.cursorIndex == text.Length - 1 && forward) return text;
    // Remove cursor from its current index
    text = text.Remove(cursor.cursorIndex, 1);
    // Shift the cursor index
    cursor.cursorIndex = forward ? cursor.cursorIndex + 1 : cursor.cursorIndex - 1;
    // Assemble the string with the cursor symbol
    text = text.Insert(cursor.cursorIndex, cursor.cursorSymbol.ToString());
    return text;
  }

  private static string RemoveCursor(string input, int cursorIndex)
  {
    return input.Remove(cursorIndex, 1);
  }
  public static void ClearCurrentConsoleLine()
  {
    int currentLineCursor = Console.CursorTop;
    Console.SetCursorPosition(0, Console.CursorTop);
    Console.Write(new string(' ', Console.WindowWidth));
    Console.SetCursorPosition(0, currentLineCursor);
  }

}
public static class StringExtensions
{
  public static string RemoveTagMarkers(this string text) => text.Replace("{", "").Replace("}", "");
  public static string FindTags(this string text, out List<string> tags)
  {
    tags = Regex.Matches(text, @"\{([\w ]+)\}", RegexOptions.IgnoreCase).Select(match => match.Groups[1].Value).ToList();
    return text;
  }
  public static IEnumerable<string> Concat(this string text, IEnumerable<string> next)
  {
    return toEnumerable(text).Concat(next);

    // hax
    IEnumerable<string> toEnumerable(string toEnum)
    {
      yield return toEnum;
    }
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