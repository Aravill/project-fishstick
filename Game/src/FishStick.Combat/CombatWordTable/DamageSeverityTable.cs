using FishStick.Util;

namespace FishStick.Combat.CombatWordTable
{
  class DamageSeverityTable : Dictionary<Range, List<string>>
  {
    private static readonly Dictionary<Range, List<string>> _table = new()
    {
      { new Range (0, 10), new List<string>() {
        "a light injury",
        "a surface cut",
        "a minor bruise",
        "a small scrape",
        "a small cut",
        "a small bruise",
      } },
      { new Range (11, 45), new List<string>() {
        "a minor injury",
        "a cut",
        "a bruise",
        "a scrape",
      } },
      { new Range (46, 90), new List<string>() {
        "a major injury",
        "a deep cut",
        "a huge bruise",
        "a serious scrape",
        "a serious cut",
        "a painful bruise",
        "a gushing wound",
      } },
      { new Range (91, 100), new List<string>() {
        "a critical injury",
        "a very painful wound",
        "a bloody wound",
        "a deep laceration",
        "a deep wound with severed muscles",
      } },
    };

    public static Dictionary<Range, List<string>> GetDictionary()
    {
      return _table;
    }

    public static string GetRandomWord(int damagePercentage)
    {
      foreach (KeyValuePair<Range, List<string>> entry in _table)
      {
        if (entry.Key.IsWithinRange(damagePercentage))
        {
          return ListUtils.GetRandomItem(entry.Value);
        }
      }
      return "an injury";
    }
  }
}