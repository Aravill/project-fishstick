using FishStick.Combat.Narration.BodyParts;

namespace FishStick.Combat.Narration
{
  class BodyPartGenerator
  {

    public static string GetRandomBodyPart(CreatureTypeEnum creatureType, SubjectEnum subject)
    {
      switch (creatureType)
      {
        case CreatureTypeEnum.Humanoid:
          return HumanoidBodyPartDictionary.GetRandomBodyPart(subject);
        default:
          return "body";
      }
    }
  }
}