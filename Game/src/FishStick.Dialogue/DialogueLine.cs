namespace Dialogue
{
    class DialogueLine : IDialogueLine
    {
        public string Id { get; }
        public string Text { get; }

        public bool IsDialogueExit => Replies == null && NextLineId == null;
        public bool? ReadNextLine { get; }
        public string? NextLineId { get; }
        public List<IReply>? Replies { get; }

        public DialogueLine(
            string id,
            string text,
            List<IReply>? replies = null,
            bool? readNextLine = null,
            string? nextLineId = null
        )
        {
            Id = id;
            Text = text;
            Replies = replies;
            ReadNextLine = readNextLine;
            NextLineId = nextLineId;
        }
    }
}
