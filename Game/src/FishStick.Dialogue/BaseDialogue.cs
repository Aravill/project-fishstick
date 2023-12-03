namespace Dialogue
{
    class BaseDialogue : IDialogue
    {
        public string Id { get; }

        public List<IDialogueLine> Lines { get; }

        public IDialogueLine CurrentLine { get; set; }

        void IDialogue.ContinueDialogue(IReply reply)
        {
            reply.WasUsed = true;
            IDialogueLine? nextLine = Lines.Find(line => line.Id == reply.NextLineId);
            if (nextLine == null)
            {
                return;
            }
            CurrentLine = nextLine;
            return;
        }

        void IDialogue.ContinueDialogue(IDialogueLine line)
        {
            if (line.NextLineId == null)
            {
                return;
            }
            IDialogueLine? nextLine = Lines.Find(nextLine => nextLine.Id == line.NextLineId);
            if (nextLine == null)
            {
                return;
            }
            CurrentLine = nextLine;
            return;
        }

        public BaseDialogue(string id, List<IDialogueLine> lines, string startingLineId)
        {
            Id = id;
            Lines = lines;
            CurrentLine = lines.Find(line => line.Id == startingLineId) ?? lines[0];
        }
    }
}
