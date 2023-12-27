
namespace FishStick.SimpleDialogues
{
  public static class MaidenDialogues
  {
    public static void Init()
    {
      Global.Dialogues["maiden.first-meeting"] = [];
      Global.Dialogues["maiden.first-meeting"][0] = ("Farewell then, stranger, freeze to thine liking.", null);
      Global.Dialogues["maiden.first-meeting"][1] = ("Greetings, stranger. Thee must be frozen to the bone. Come, share this fire with me.", new()
        {
          ("Thank you.", 2, null),
          ("I don't think so.", 0, null),
        });
      Global.Dialogues["maiden.first-meeting"][1] = ("Greetings, stranger. Thee must be frozen to the bone. Come, share this fire with me.", []);
      Global.Dialogues["maiden.first-meeting"][1] = ("Greetings, stranger. Thee must be frozen to the bone. Come, share this fire with me.", []);
      Global.Dialogues["maiden.first-meeting"][2] = ("Wonderful! Come and sit.", null);
    }
  }
}

