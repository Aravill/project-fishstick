using FishStick.Util;

namespace FishStick.Combat.Narration
{
  class HitMissTable
  {
    private static readonly Dictionary<bool, List<string>> _enemyTable = new() {
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

    private static readonly Dictionary<bool, List<string>> _playerTable = new() {
      { false, new List<string>() {
        "miss",
        "fail",
        "whiff",
        "do not connect",
        "do not land",
        "do not make contact",
        "do not succeed"
      } },
      { true, new List<string>() {
        "hit",
        "strike true",
        "connect",
        "land",
        "make contact",
        "succeed"
      } },
    };

    public static string GetRandomWord(bool hitResult, SubjectEnum subject)
    {
      if (subject == SubjectEnum.Player)
      {
        return ListUtils.GetRandomItem(_playerTable[hitResult]);
      }
      return ListUtils.GetRandomItem(_enemyTable[hitResult]);
    }
  }
}