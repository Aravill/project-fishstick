namespace Dialogue
{
  public interface IDialogue
  {
    string Id { get; }
    List<IDialogueLine> Lines { get; }
    IDialogueLine CurrentLine { get; }
    public void ContinueDialogue(IReply reply);
    public void ContinueDialogue(IDialogueLine line);

    public void EndDialogue();
    public IDialogueCondition? Condition { get; }

    int Order { get; }
    bool WasHad { get; set; }
    bool Repeatable { get; set; }
  }
}
