using FishStick.Util;

namespace FishStick.Combat.CombatWordTable
{
  class PlayerBodyPartList : List<string>
  {
    private static readonly List<string> _list = new() {
      "head",
      "neck",
      "left shoulder",
      "right shoulder",
      "chest",
      "stomach",
      "back",
      "left arm",
      "right arm",
      "left hand",
      "right hand",
      "left leg",
      "right leg",
      "left foot",
      "right foot",
    };

    public static List<string> GetList()
    {
      return _list;
    }
    public static string GetRandomWord()
    {
      return ListUtils.GetRandomItem(_list);
    }
  }
}