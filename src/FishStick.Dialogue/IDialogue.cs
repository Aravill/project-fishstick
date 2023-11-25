namespace Dialogue
{
  interface IDialogue
  {
    string Id { get; }
    List<IDialogueLine> Lines { get; }
    IDialogueLine CurrentLine { get; }
    IDialogueLine? GetNextLine(IReply reply);
  }
}