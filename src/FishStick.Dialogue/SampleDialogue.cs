namespace Dialogue
{
  class SampleDialogue
  {
    public static BaseDialogue GetSampleDialogue()
    {
      // Initiate some testing dialogue
      DialogueLine l1 = new DialogueLine("line1", "Align thine gaze north. There, amidst the trees in the distance, stands the Obelisk.", null, false, "line0");

      DialogueLine l2 = new DialogueLine("line2", "I do not know. I am but a simple Maiden, not knowledgable of Fate and Purpose.", null, false, "line0");

      DialogueLine l3 = new DialogueLine("line3", "I am not sure. The Obelisk shall tell you.", null, false, "line0");

      DialogueLine l4 = new DialogueLine("line4", "Farewell then, stranger.");

      DialogueLine l5 = new DialogueLine("line5", "Greetings, stranger. Thee must be frozen to the bone. Come, share this fire with me.", new List<IReply> {
        new Reply("Do you have any warm clothing I could take?", "line6"),
        new Reply("Who are you?", "line8"),
        new Reply("Where am I?", "line9"),
        new Reply("Why am I here?", "line10"),
        new Reply("Do you have anything to trade?", "line11"),
        new Reply("What are you doing here?", "line12"),
        new Reply("There used to be a bedroll here.", "line13"),
        new Reply("Goodbye.", "line4")
      });

      DialogueLine l6 = new DialogueLine("line6", "Alas, I cannot aid thee. I would offer this scarf, but it is not meant for thee and I have ran out of yarn to make more. However, should thee discover some and bring it back to me, I would knit thee a scarf of thine own.", new List<IReply> {
        new Reply("Where can I find yarn for you?", "line7"),
        new Reply("I have other questions.", "line15"),
      });

      DialogueLine l7 = new DialogueLine("line7", "Perhaps thou could find some in the woods. Though I suppose that is no place to search for yarn.", null, false, "line5"
      );

      DialogueLine l8 = new DialogueLine("line8", "I am nothing more than I seem, a Maiden knitting clothing. Irrelevant, though, as to thee I shall always be only what thou believes me to be. Who art thou?", new List<IReply>
      {
        new Reply("I am… I do not know.", "line14"),
        new Reply("I have other questions.", "line15")
      }
      );

      DialogueLine l9 = new DialogueLine("line9", "Where thou art supposed to be.", new List<IReply>
      {
        new Reply("What does that mean?", "line16"),
        new Reply("I have other questions.", "line15")
      }
      );

      DialogueLine l10 = new DialogueLine("line10", "Our Purpose remains hidden to us all. Thou must discover thine purposes thyself.", null, false, "line5"
      );

      DialogueLine l11 = new DialogueLine("line11", "I am a Maiden, not a merchant.", null, false, "line5"
      );

      DialogueLine l12 = new DialogueLine("line12", "Why, I am knitting.", null, false, "line5"
      );

      DialogueLine l13 = new DialogueLine("line13", "Not to my knowledge.", null, false, "line5"
      );

      DialogueLine l14 = new DialogueLine("line14", "Odd. A stranger to me, and a stranger to thyself. Perhaps thou has not journeyed far enough yet.", new List<IReply>
      {
        new Reply("I need to know who I am.", "line0"),
        new Reply("I have other questions.", "line15"),
      }
      );

      DialogueLine l15 = new DialogueLine("line15", "Ofcourse.", null, false, "line5"
      );

      DialogueLine l16 = new DialogueLine("line16", "Whatever thou believes.", null, false, "line5"
 );

      DialogueLine l0 = new DialogueLine("line0", "Seek the Obelisk then. But be wary, this land is as full of dangers as it is full of beauty. The Obelisk is dangerous.", new List<IReply> {
        new Reply("Where is the Obelisk?", "line1"),
        new Reply("What is the Obelisk?", "line2"),
        new Reply("What do I do when I reach the Obelisk?", "line3"),
        new Reply("I have other questions.", "line15"),
        new Reply("Goodbye.", "line4")
      });
      List<IDialogueLine> lines = new List<IDialogueLine> { l0, l1, l2, l3, l4, l5, l6, l7, l8, l9, l10, l11, l12, l13, l14, l15, l16 };
      return new BaseDialogue("SampleDialogue", lines, "line5");
    }
  }
}