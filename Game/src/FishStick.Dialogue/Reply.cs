namespace Dialogue
{
    class Reply : IReply
    {
        public string Text { get; }

        public string NextLineId { get; }

        public bool WasUsed { get; set; }

        public Reply(string text, string nextLineId)
        {
            Text = text;
            NextLineId = nextLineId;
            WasUsed = false;
        }
    }
}
