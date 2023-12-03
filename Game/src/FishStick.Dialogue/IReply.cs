namespace Dialogue
{
  public interface IReply
  {
    string Text { get; }
    string? NextLineId { get; }

    bool WasUsed { get; set; }

    bool Repeatable { get; }
  }
}
