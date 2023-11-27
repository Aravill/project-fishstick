namespace Dialogue
{
  interface IDialogue
  {
    string Id { get; }
    List<IDialogueLine> Lines { get; }
    IDialogueLine CurrentLine { get; }
    public void ContinueDialogue(IReply reply);
    public void ContinueDialogue(IDialogueLine line);
  }
}