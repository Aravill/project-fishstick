using FishStick.Combat.Narration.BodyParts;

namespace FishStick.Combat.Narration
{
  class BodyPartGenerator
  {

    public static string GetRandomBodyPart(CreatureTypeEnum creatureType, bool isPlayer)
    {
      switch (creatureType)
      {
        case CreatureTypeEnum.Humanoid:
          return HumanoidBodyPartDictionary.GetRandomBodyPart(isPlayer);
        default:
          return "body";
      }
    }
  }
}