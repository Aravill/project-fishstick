namespace Dialogue
{
  class Reply : IReply
  {
    public string Text { get; }

    public string NextLineId { get; }

    public bool WasUsed { get; set; }

    public bool Repeatable { get; }

    public Reply(string text, string nextLineId, bool repeatable = true)
    {
      Text = text;
      NextLineId = nextLineId;
      WasUsed = false;
      Repeatable = repeatable;
    }
  }
}
