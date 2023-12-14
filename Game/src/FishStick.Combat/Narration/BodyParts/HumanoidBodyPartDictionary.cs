using FishStick.Combat.Narration.Enums;
using FishStick.Util;

namespace FishStick.Combat.Narration.BodyParts
{
  public class HumanoidBodyPartDictionary : IBodyPartDictionary
  {

    // The boolean determines if the content of the item is a prepositional phrase or not
    private static readonly List<(string, bool)> _head = new() {
      ("top of", true),
      ("left side of", true),
      ("right side of", true),
      ("back of", true),
      ("front of", true)
    };

    private static readonly List<(string, bool)> _neck = new()
    {
      ("front of", true),
      ("back of", true),
      ("left side of", true),
      ("right side of", true)
    };

    private static readonly List<(string, bool)> _shoulder = new()
    {
      ("left", false),
      ("right", false)
    };

    private static readonly List<(string, bool)> _chest = new()
    {
      ("left side of", true),
      ("right side of", true),
      ("center of", true),
      ("top of the", true)
    };

    private static readonly List<(string, bool)> _stomach = new()
    {
      ("left side of", true),
      ("right side of", true),
      ("center of", true),
    };

    private static readonly List<(string, bool)> _back = new()
    {
      ("left side of", true),
      ("right side of", true),
      ("center of", true),
      ("upper", false),
      ("lower", false)
    };

    private static readonly List<(string, bool)> _arm = new()
    {
      ("left", false),
      ("right", false)
    };

    private static readonly List<(string, bool)> _hand = new()
    {
      ("left", false),
      ("right", false)
    };

    private static readonly List<(string, bool)> _leg = new()
    {
      ("left", false),
      ("right", false)
    };

    private static readonly List<(string, bool)> _foot = new()
    {
      ("left", false),
      ("right", false)
    };

    private static readonly Dictionary<HumanoidBodyPartEnum, List<(string, bool)>> _adjectiveDictionary = new()
    {
      { HumanoidBodyPartEnum.Head, _head },
      { HumanoidBodyPartEnum.Neck, _neck },
      { HumanoidBodyPartEnum.Shoulder, _shoulder },
      { HumanoidBodyPartEnum.Chest, _chest },
      { HumanoidBodyPartEnum.Stomach, _stomach },
      { HumanoidBodyPartEnum.Back, _back },
      { HumanoidBodyPartEnum.Arm, _arm },
      { HumanoidBodyPartEnum.Hand, _hand },
      { HumanoidBodyPartEnum.Leg, _leg },
      { HumanoidBodyPartEnum.Foot, _foot }
    };

    public static string GetRandomBodyPart(bool isPlayer)
    {
      HumanoidBodyPartEnum bodyPartEnum = HumanoidBodyPartEnum.GetRandomBodyPart();
      string bodyPart = bodyPartEnum.Value;
      string pronoun = isPlayer ? "your" : "their";
      if (_adjectiveDictionary.ContainsKey(bodyPartEnum))
      {
        (string, bool) flavor = ListUtils.GetRandomItem(_adjectiveDictionary[bodyPartEnum]);
        if (flavor.Item2)
        {
          // "the left side of their back"
          return $"the {flavor.Item1} {pronoun} {bodyPart}";
        }
        // "their upper back"
        return $"{pronoun} {flavor.Item1} {bodyPart}";
      }
      // "back"
      return $"{pronoun} {bodyPart}";
    }
  }
}