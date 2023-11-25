namespace Dialogue
{
  interface IDialogueLine
  {
    string Text { get; }
    List<IDialogueLine> NextLines { get; }
  }
}