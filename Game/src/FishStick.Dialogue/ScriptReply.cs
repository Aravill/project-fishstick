using FishStick.Scripts;

namespace Dialogue
{
  class ScriptReply : Reply
  {
    public IDialogueScript[] Scripts { get; }

    public ScriptReply(
      string text,
      string nextLineId,
      IDialogueScript script,
      bool repeatable = false
    )
      : base(text, nextLineId, repeatable)
    {
      Scripts = [script];
    }

    public ScriptReply(
      string text,
      string nextLineId,
      IDialogueScript[] scripts,
      bool repeatable = false
    )
      : base(text, nextLineId, repeatable)
    {
      Scripts = scripts;
    }
  }
}
