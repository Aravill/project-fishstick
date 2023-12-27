using System.Runtime.Serialization;

namespace Dialogue
{
  [DataContract]
  class BaseDialogue : IDialogue
  {
    [DataMember]
    public string Id { get; }
    [DataMember]
    public List<IDialogueLine> Lines { get; }
    [DataMember]
    public IDialogueLine CurrentLine { get; set; }
    [DataMember]
    public IDialogueCondition? Condition { get; }

    [DataMember]
    public bool WasHad { get; set; }

    [DataMember]
    public bool Repeatable { get; set; }

    [DataMember]
    public int Order { get; set; }

    [DataMember]
    private IDialogueLine _startingLine;

    void IDialogue.ContinueDialogue(IReply reply)
    {
      reply.WasUsed = true;
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
      IDialogueCondition? condition = null,
      bool wasHad = false
    )
    {
      Id = id;
      Lines = lines;
      CurrentLine = lines.Find(line => line.Id == startingLineId) ?? lines[0];
      _startingLine = CurrentLine;
      Condition = condition;
      WasHad = wasHad;
      Order = order;
      Repeatable = repeatable;
    }
  }
}
