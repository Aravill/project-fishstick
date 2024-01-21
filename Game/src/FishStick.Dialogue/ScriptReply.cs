using FishStick.Scripts;

namespace Dialogue
{
  class ScriptReply : Reply
  {
    public List<IDialogueScript> Scripts { get; }

    public ScriptReply(
      string id,
      string text,
      string nextLineId,
      IDialogueScript script,
      bool repeatable = false
    )
      : base(id, text, nextLineId, repeatable)
    {
      Scripts = [script];
    }

    public ScriptReply(
      string id,
      string text,
      string nextLineId,
      List<IDialogueScript> scripts,
      bool repeatable = false
    )
      : base(id, text, nextLineId, repeatable)
    {
      Scripts = scripts;
    }
  }
}
