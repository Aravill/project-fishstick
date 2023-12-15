using FishStick.Util;

namespace FishStick.Combat.Narration
{
  public class AttackList
  {

    private static readonly List<string> _enemyList = new() {
      "attacks",
      "strikes out at",
      "takes a swing at",
      "swings at",
      "charges",
      "lunges at",
    };

    private static readonly List<string> _playerList = new() {
      "attack",
      "strike out at",
      "take a swing at",
      "swing at",
      "charge",
      "lunge at",
    };

    public static string GetRandomWord(SubjectEnum subject)
    {
      if (subject == SubjectEnum.Player)
      {
        return ListUtils.GetRandomItem(_playerList);
      }
      return ListUtils.GetRandomItem(_enemyList);
    }
  }
}