using FishStick.Util;

namespace FishStick.Combat.Narration
{
  class InjuryVerbTable
  {
    private static readonly Dictionary<DamageTypeEnum, List<string>> _table = new()
    {
      { DamageTypeEnum.Piercing, new List<string>() {
        "pierce",
        "puncture",
        "stab",
        "impale",
        "poke",
        "jab",
        "skewer",
        "spike",
      } },
      { DamageTypeEnum.Slashing, new List<string>() {
        "slash",
        "cut",
        "slice",
        "gash",
        "hack",
        "chop",
        "cleave",
        "lacerate",
        "carve",
      } },
      { DamageTypeEnum.Bludgeoning, new List<string>() {
        "bludgeon",
        "pound",
        "smash",
        "crush",
        "pummel",
        "thrash",
        "beat",
        "batter",
        "thrash",
        "clobber",
        "whack",
      } },
    };

    public static Dictionary<DamageTypeEnum, List<string>> GetDictionary()
    {
      return _table;
    }

    public static string GetRandomWord(DamageTypeEnum type)
    {
      return ListUtils.GetRandomItem(_table[type]);
    }
  }
}