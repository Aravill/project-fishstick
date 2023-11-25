namespace Dialogue
{
  interface IReply
  {
    string Text { get; }
    IDialogueLine? NextLine { get; }
  }
}