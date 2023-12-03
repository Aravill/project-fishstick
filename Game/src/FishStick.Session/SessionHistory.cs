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

        public string GetPrevious()
        {
            _historyIndex--;
            if (_historyIndex < 0)
            {
                _historyIndex = -1;
                return "";
            }
            ;
            return _history[_historyIndex];
        }

        public string GetNext()
        {
            _historyIndex++;
            if (_historyIndex >= _history.Count)
            {
                _historyIndex = _history.Count;
                return "";
            }
            ;
            return _history[_historyIndex];
        }
    }
}
