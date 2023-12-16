using System.Reflection;

namespace FishStick.Combat.Narration.Enums
{
  public sealed class HumanoidBodyPartEnum
  {
    public static readonly HumanoidBodyPartEnum Head = new HumanoidBodyPartEnum("head");
    public static readonly HumanoidBodyPartEnum Neck = new HumanoidBodyPartEnum("neck");
    public static readonly HumanoidBodyPartEnum Shoulder = new HumanoidBodyPartEnum("shoulder");
    public static readonly HumanoidBodyPartEnum Chest = new HumanoidBodyPartEnum("chest");
    public static readonly HumanoidBodyPartEnum Stomach = new HumanoidBodyPartEnum("stomach");
    public static readonly HumanoidBodyPartEnum Back = new HumanoidBodyPartEnum("back");
    public static readonly HumanoidBodyPartEnum Arm = new HumanoidBodyPartEnum("arm");
    public static readonly HumanoidBodyPartEnum Hand = new HumanoidBodyPartEnum("hand");
    public static readonly HumanoidBodyPartEnum Leg = new HumanoidBodyPartEnum("leg");
    public static readonly HumanoidBodyPartEnum Foot = new HumanoidBodyPartEnum("foot");

    private HumanoidBodyPartEnum(string value)
    {
      Value = value;
    }
    public string Value { get; private set; }

    public static HumanoidBodyPartEnum GetRandomBodyPart()
    {
      var fields = typeof(HumanoidBodyPartEnum).GetFields(BindingFlags.Public | BindingFlags.Static);
      var random = new Random();
      return (HumanoidBodyPartEnum)fields[random.Next(fields.Length)].GetValue(null)!;
    }
  }
}
