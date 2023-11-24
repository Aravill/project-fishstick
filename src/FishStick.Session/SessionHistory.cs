namespace FishStick.Session
{
  class SessionHistory
  {
    private List<string> _history = new();
    private int _historyIndex = 0;

    public void Add(string input)
    {
      _history.Add(input);
      _historyIndex = _history.Count;
    }

    public void ResetPosition()
    {
      _historyIndex = _history.Count;
    }

    public string GetPrevious()
    {
      if (_historyIndex == 0) return "";
      _historyIndex--;
      return _history[_historyIndex];
    }

    public string GetNext()
    {
      if (_historyIndex == _history.Count) return "";
      _historyIndex++;
      return _history[_historyIndex];
    }
  }
}