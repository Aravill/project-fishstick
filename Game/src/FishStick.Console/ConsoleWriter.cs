using FishStick.Extensions;

namespace FishStick.Render
{
  public class ConsoleWriter
  {
    private string _message = string.Empty;
    private ConsoleColor _foregroundColor = Console.ForegroundColor;
    private ConsoleColor _backgroundColor = Console.BackgroundColor;
    private bool _writeSlowly = false;
    private int _millisecondsDelay;
    private Dictionary<string, ConsoleColor> _highlightedPhrases =
      new Dictionary<string, ConsoleColor>();

    private ConsoleWriter() { }

    public static ConsoleWriter Write(string message)
    {
      return new ConsoleWriter { _message = message };
    }

    public ConsoleWriter NoTags()
    {
      _message = _message.RemoveTags();
      return this;
    }

    public void SetSlowly(bool slowly) => _writeSlowly = slowly;

    public ConsoleWriter Slowly(int millisecondsDelay = 20)
    {
      _writeSlowly = true;
      _millisecondsDelay = millisecondsDelay;
      return this;
    }

    public ConsoleWriter Color(
      ConsoleColor foregroundColor,
      ConsoleColor? backgroundColor = null
    )
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

    public ConsoleWriter WithHighlighting(IEnumerable<string> highlightedWords, ConsoleColor highlighColor)
    {
      _highlightedPhrases = highlightedWords.ToDictionary(word => word, word => highlighColor);
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
      for (int i = 0; i < word.Length; i++)
      {
        if (_writeSlowly)
        {
          // Check for key press
          if (Console.KeyAvailable)
          {
            Console.ReadKey(true); // Clears the key press
            _writeSlowly = false;
          }

          Console.Write(word[i]);
          Thread.Sleep(_millisecondsDelay);  // For async do await Task.Delay(20);
        }
        else
        {
          Console.Write(word[i..]); // Write the rest of the word
          break;
        }
      }
    }
  }
}