using FishStick.Util;

namespace FishStick.Combat.CombatWordTable
{
  class HitMissTable : Dictionary<bool, List<string>>
  {
    private static readonly Dictionary<bool, List<string>> _table = new() {
      { false, new List<string>() {
        "misses",
        "fails",
        "whiffs",
        "doesn't connect",
        "doesn't land",
        "doesn't make contact",
        "doesn't succeed"
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