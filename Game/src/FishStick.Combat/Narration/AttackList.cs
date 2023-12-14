using FishStick.Util;

namespace FishStick.Combat.Narration
{
  public class AttackList
  {

    private static readonly List<string> _list = new() {
      "attacks you",
      "strikes out at you",
      "takes a swing at you",
      "swings at you",
      "charges you",
      "lunges at you",
      "attacks you",
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