using FishStick.Util;

namespace FishStick.Combat.Narration
{
  class HitMissTable
  {
    private static readonly Dictionary<bool, List<string>> _table = new() {
      { false, new List<string>() {
        "misses",
        "fails",
        "whiffs",
        "does not connect",
        "does not land",
        "does not make contact",
        "does not succeed"
      } },
      { true, new List<string>() {
        "hits",
        "strikes true",
        "connects",
        "lands",
        "makes contact",
        "succeeds"
      } },
    };

    public static Dictionary<bool, List<string>> GetDictionary()
    {
      return _table;
    }

    public static string GetRandomWord(bool hitResult)
    {
      return ListUtils.GetRandomItem(_table[hitResult]);
    }
  }
}