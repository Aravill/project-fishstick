namespace Dialogue
{
  public interface IReply
  {
    string Text { get; }
    string? NextLineId { get; }

    bool WasUsed { get; }

    bool Repeatable { get; }

    void Use();
  }
}
