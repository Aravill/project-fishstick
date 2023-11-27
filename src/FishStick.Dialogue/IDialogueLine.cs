namespace Dialogue
{
  interface IDialogueLine
  {
    string Id { get; }
    string Text { get; }

    bool IsDialogueExit { get; }

    // Sometimes, we do not want the next line to be read, this is used when lines lead back onto
    // lines that preceed them
    bool? ReadNextLine { get; }

    // Lines can lead into each other like replies do, but they don't have to.
    string? NextLineId { get; }

    // A DialogueLine can have a list of replies, or it can be the end of the dialogue.
    // Imagine something an NPC says with no replies, like "Begone!"
    List<IReply>? Replies { get; }
  }
}