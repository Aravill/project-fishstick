namespace Dialogue
{
    interface IReply
    {
        string Text { get; }
        string? NextLineId { get; }

        bool WasUsed { get; set; }
    }
}
