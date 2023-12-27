
namespace FishStick.SimpleDialogues
{
  public static class MaidenDialogues
  {
    public static void Init()
    {
      Global.Dialogues["maiden.first-meeting"] = new();

      Global.Dialogues["maiden.first-meeting"][0] = new();
      Global.Dialogues["maiden.first-meeting"][0][0] = ("Farewell then, stranger, freeze to thine liking.", null, true);

      Global.Dialogues["maiden.first-meeting"][1] = new();
      Global.Dialogues["maiden.first-meeting"][1][0] = ("Greetings, stranger. Thee must be frozen to the bone. Come, share this fire with me.", null, null);
      Global.Dialogues["maiden.first-meeting"][1][1] = ("Thank you.", 2, true);
      Global.Dialogues["maiden.first-meeting"][1][2] = ("I don't think so.", 0, true);

      Global.Dialogues["maiden.first-meeting"][2] = new();
      Global.Dialogues["maiden.first-meeting"][2][0] = ("Wonderful! Come and sit.", null, null);

      // ===

      Global.Dialogues["maiden.generic"] = new();

      Global.Dialogues["maiden.generic"][0] = new();
      Global.Dialogues["maiden.generic"][0][0] = ("Farewell then, stranger.", 1, false);

      Global.Dialogues["maiden.generic"][1] = new();
      Global.Dialogues["maiden.generic"][1][0] = ("Why art thou wandering the cold forest, stranger?", null, true);
      Global.Dialogues["maiden.generic"][1][1] = ("Do you have any warm clothing I could take?", 2, true);
      Global.Dialogues["maiden.generic"][1][2] = ("Who are you?", 4, true);
      Global.Dialogues["maiden.generic"][1][3] = ("Where am I?", 5, true);
      Global.Dialogues["maiden.generic"][1][4] = ("Why am I here?", 7, true);
      Global.Dialogues["maiden.generic"][1][5] = ("What are you doing here?", 8, true);
      Global.Dialogues["maiden.generic"][1][6] = ("Goodbye.", 0, true);

      Global.Dialogues["maiden.generic"][2] = new();
      Global.Dialogues["maiden.generic"][2][0] = ("Alas, I cannot aid thee. I would offer this scarf, but it is not meant for thee and I have ran out of yarn to make more. However, should thee discover some and bring it back to me, I would knit thee a scarf of thine own.", 1, false);
      Global.Dialogues["maiden.generic"][2][1] = ("Where can I find yarn for you?", 3, true);
      Global.Dialogues["maiden.generic"][2][2] = ("I have other questions.", 10, true);

      Global.Dialogues["maiden.generic"][3] = new();
      Global.Dialogues["maiden.generic"][3][0] = ("Perhaps thou could find some in the woods nearby.", 1, false);

      Global.Dialogues["maiden.generic"][4] = new();
      Global.Dialogues["maiden.generic"][4][0] = ("I am nothing more than I seem, a Maiden knitting clothing. Irrelevant, though, as to thee I shall always be only what thou believes me to be. Who art thou?", null, true);
      Global.Dialogues["maiden.generic"][4][1] = ("I am… I do not know.", 5, true);
      Global.Dialogues["maiden.generic"][4][2] = ("I have other questions.", 10, true);

      Global.Dialogues["maiden.generic"][5] = new();
      Global.Dialogues["maiden.generic"][5][0] = ("Where thou art supposed to be.", null, true);
      Global.Dialogues["maiden.generic"][5][1] = ("What does that mean?", 6, true);
      Global.Dialogues["maiden.generic"][5][2] = ("I have other questions.", 10, true);

      Global.Dialogues["maiden.generic"][6] = new();
      Global.Dialogues["maiden.generic"][6][0] = ("I am a Maiden, not a merchant.", 1, false);

      Global.Dialogues["maiden.generic"][7] = new();
      Global.Dialogues["maiden.generic"][7][0] = ("Our Purpose remains hidden to us all. Thou must discover thine purposes thyself.", 1, false);

      Global.Dialogues["maiden.generic"][8] = new();
      Global.Dialogues["maiden.generic"][8][0] = ("Why, I am knitting.", 1, false);

      Global.Dialogues["maiden.generic"][9] = new();
      Global.Dialogues["maiden.generic"][9][0] = ("Odd. A stranger to me, and a stranger to thyself. Perhaps thou has not journeyed far enough yet.", null, true);
      Global.Dialogues["maiden.generic"][9][1] = ("I need to know who I am.", 11, true);
      Global.Dialogues["maiden.generic"][9][2] = ("I have other questions.", 10, true);

      Global.Dialogues["maiden.generic"][10] = new();
      Global.Dialogues["maiden.generic"][10][0] = ("Very well, ask them.", 1, false);

      Global.Dialogues["maiden.generic"][11] = new();
      Global.Dialogues["maiden.generic"][11][0] = ("Seek the Obelisk then. But be wary, this land is as full of dangers as it is full of beauty. The Obelisk is dangerous.", null, true);
      Global.Dialogues["maiden.generic"][11][1] = ("Where is the Obelisk?", 12, true);
      Global.Dialogues["maiden.generic"][11][2] = ("What is the Obelisk?", 13, true);
      Global.Dialogues["maiden.generic"][11][3] = ("What do I do when I reach it?", 14, true);
      Global.Dialogues["maiden.generic"][11][4] = ("I have other questions.", 10, true);
      Global.Dialogues["maiden.generic"][11][5] = ("Goodbye.", 0, true);

      Global.Dialogues["maiden.generic"][12] = new();
      Global.Dialogues["maiden.generic"][12][0] = ("Align thine gaze north. There, amidst the trees in the distance, stands the Obelisk.", 11, true);

      Global.Dialogues["maiden.generic"][13] = new();
      Global.Dialogues["maiden.generic"][13][0] = ("I do not know. I am but a simple Maiden, not knowledgeable of Fate and Purpose.", 11, true);

      Global.Dialogues["maiden.generic"][14] = new();
      Global.Dialogues["maiden.generic"][14][0] = ("The Obelisk shall tell you.", 11, true);
    }
  }
}

