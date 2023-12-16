using FishStick.Combat;
using FishStick.Util;

namespace FishStick.Combat.Narration
{
  class InjuryTable
  {

    private static readonly Dictionary<Range, List<string>> _adjectives = new()
    {
      { new Range (0, 10), new List<string>() {
        "light",
        "small",
        "minor",
      } },
      { new Range (11, 45), new List<string>() {
        "",
      } },
      { new Range (46, 90), new List<string>() {
        "major",
        "serious",
        "painful",
      } },
      { new Range (91, 100), new List<string>() {
        "critical",
        "excruciating",
        "bloody",
        "deep",
      } },
    };
    private static readonly Dictionary<DamageTypeEnum, List<string>> _injuryTable = new()
    {
      { DamageTypeEnum.Piercing, new List<string>() {
        "puncture",
        "stab wound",
        "wound",
      } },
      { DamageTypeEnum.Slashing, new List<string>() {
        "cut",
        "gash",
        "laceration",
        "scrape",
        "wound",
      } },
      { DamageTypeEnum.Bludgeoning, new List<string>() {
        "bruise",
        "contusion",
        "fracture",
        "abrasion",
        "wound",
      } },
    };
    private static string GetRandomAdjective(int damagePercentage)
    {
      foreach (KeyValuePair<Range, List<string>> entry in _adjectives)
      {
        if (entry.Key.IsWithinRange(damagePercentage))
        {
          return ListUtils.GetRandomItem(entry.Value);
        }
      }
      return "";
    }

    private static string GetRandomInjury(DamageTypeEnum type)
    {
      return ListUtils.GetRandomItem(_injuryTable[type]);
    }
    public static string GetRandomWordCombination(int damagePercentage, DamageTypeEnum type)
    {
      string adjective = GetRandomAdjective(damagePercentage);
      string injury = GetRandomInjury(type);
      char[] chars = ['a', 'e', 'i', 'o', 'u'];
      bool startsWithVowel = chars.Contains(adjective.Length > 0 ? adjective[0] : injury[0]);
      return $"{(startsWithVowel ? "an" : "a")} {(adjective.Length > 0 ? $"{adjective} " : "")}{injury}";
    }
  };
};
