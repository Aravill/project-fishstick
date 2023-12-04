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
      string withoutTags = text.FindTaggedWords(out var taggedWords)
                               .RemoveTagMarkers();

      ConsoleWriter.Write(withoutTags)
        .Slowly()
        .WithColor(ConsoleColor.DarkGray)
        .WithHighlighting(taggedWords, ConsoleColor.DarkYellow)
        .ToConsole();

      Console.WriteLine();
    }
    public static void DescribeScene(IScene scene)
    {
      var description = scene.Description;
      var transitions = scene.Transitions.Select(t => t.Description);
      var items       = scene.Items.Where(i => !i.Hidden).Select(i => i.SceneDescription);
      var elements    = scene.Elements.Where(e => !e.Hidden).Select(e => e.SceneDescription);

      var textList = description.Concat(transitions)
                                .Concat(items)
                                .Concat(elements);

      var allText = string.Join(' ', textList)
                          .FindTaggedWords(out var taggedWords)
                          .RemoveTagMarkers();

      ConsoleWriter.Write(allText)
        .Slowly()
        .WithColor(ConsoleColor.DarkGray)
        .WithHighlighting(taggedWords, ConsoleColor.DarkYellow)
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

        // Don't do anything if the user hasn't typed anything yet
        if (readKeyResult.Key == ConsoleKey.Enter && writtenInput.Length <= 1)
          continue;

        ClearCurrentConsoleLine();

        writtenInput = HandleInputKey(readKeyResult, history, cursor, ref writtenInput, ref finalInput);
        Console.Write(writtenInput);
      }
      while (finalInput == null || finalInput.Length < 1);
      
      Console.WriteLine();
      history.Add(finalInput);
      Console.CursorVisible = true;
      
      return finalInput;
    }

    private static string HandleInputKey(ConsoleKeyInfo readKeyResult,
                                          SessionHistory history,
                                          GameCursor cursor,
                                          ref string writtenInput,
                                          ref string? finalInput) 
    => readKeyResult.Key switch
    {
      ConsoleKey.UpArrow => PreviousCommand(history, cursor),
      ConsoleKey.DownArrow => NextCommand(history, cursor),
      ConsoleKey.Backspace => Backspace(cursor, writtenInput),
      ConsoleKey.Enter => ConfirmInput(cursor, writtenInput, ref finalInput),
      ConsoleKey.LeftArrow => MoveCursor(false, writtenInput, cursor),
      ConsoleKey.RightArrow => MoveCursor(true, writtenInput, cursor),
      _ => AddChar(cursor, writtenInput, readKeyResult)
    };

    private static string AddChar(GameCursor cursor, string writtenInput, ConsoleKeyInfo readKeyResult)
    {
      // Add the new character + cursor symbol and add it to the written input
      string added = readKeyResult.KeyChar.ToString();
      writtenInput = writtenInput.Insert(cursor.cursorIndex, added);
      cursor.cursorIndex++;
      return writtenInput;
    }

    private static string ConfirmInput(GameCursor cursor, string writtenInput, ref string? finalInput)
    {
      if (writtenInput.Length <= 1)
        return writtenInput;

      finalInput = RemoveCursor(writtenInput, cursor.cursorIndex);
      return finalInput;
    }

    private static string Backspace(GameCursor cursor, string writtenInput)
    {
      if (cursor.cursorIndex <= 0)
        return writtenInput;
      
      // Remove the symbol before the cursor
      writtenInput = writtenInput.Remove(cursor.cursorIndex - 1, 1);
      cursor.cursorIndex--;
      return writtenInput;
    }

    private static string NextCommand(SessionHistory history, GameCursor cursor)
    {
      string writtenInput = history.GetNext() + cursor.cursorSymbol;
      cursor.cursorIndex = writtenInput.Length - 1;
      return writtenInput;
    }

    private static string PreviousCommand(SessionHistory history, GameCursor cursor)
    {
      string writtenInput = history.GetPrevious() + cursor.cursorSymbol;
      cursor.cursorIndex = writtenInput.Length - 1;
      return writtenInput;
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
}