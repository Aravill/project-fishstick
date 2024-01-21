using FishStick;

namespace Dialogue
{
  public class BaseDialogue : IDialogue
  {
    public string Id { get; }

    public List<IDialogueLine> Lines { get; }

    public IDialogueLine CurrentLine { get; set; }

    public IDialogueCondition? Condition { get; }

    public bool WasHad { get => CheckUsage(); }

    public bool Repeatable { get; set; }

    public int Order { get; set; }

    private IDialogueLine _startingLine;

    void IDialogue.ContinueDialogue(IReply reply)
    {
      reply.Use();
      IDialogueLine? nextLine = Lines.Find(line => line.Id == reply.NextLineId);
      if (nextLine == null)
      {
        return;
      }
      CurrentLine = nextLine;
      return;
    }

    void IDialogue.ContinueDialogue(IDialogueLine line)
    {
      if (line.NextLineId == null)
      {
        return;
      }
      IDialogueLine? nextLine = Lines.Find(nextLine => nextLine.Id == line.NextLineId);
      if (nextLine == null)
      {
        return;
      }
      CurrentLine = nextLine;
      return;
    }

    void IDialogue.Use()
    {
      Global.DialogueData[Id].WasHad = true;
    }

    private bool CheckUsage()
    {
      return Global.DialogueData[Id].WasHad;
    }

    void IDialogue.EndDialogue()
    {
      CurrentLine = _startingLine;
    }

    public BaseDialogue(
      string id,
      List<IDialogueLine> lines,
      string startingLineId,
      int order,
      bool repeatable = true,
      IDialogueCondition? condition = null
    )
    {
      Id = id;
      Lines = lines;
      CurrentLine = lines.Find(line => line.Id == startingLineId) ?? lines[0];
      _startingLine = CurrentLine;
      Condition = condition;
      Order = order;
      Repeatable = repeatable;
    }
  }
}
