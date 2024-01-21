using FishStick;

namespace Dialogue
{
  class Reply : IReply
  {
    private string _id;

    private (string, int, int) _parsedId { get => ParseId(); }


    public string Text { get; }

    public string NextLineId { get; }

    public bool WasUsed { get => CheckUsage(); }

    public bool Repeatable { get; }

    public void Use()
    {
      // TODO: Is it better to open up the dicitonary here and search for our dialogue, line and
      // reply, or would it be better to simply hold a reference to the dialogue data in this
      // instance? This issue also affects the BaseDialogue class.
      Global.DialogueData[_parsedId.Item1].UseReply(_parsedId.Item2, _parsedId.Item3);
    }

    private bool CheckUsage()
    {
      return Global.DialogueData[_parsedId.Item1].WasReplyUsed(_parsedId.Item2, _parsedId.Item3);
    }

    private (string, int, int) ParseId()
    {
      string[] split = _id.Split(':');
      return (split[0], int.Parse(split[1]), int.Parse(split[2]));
    }

    public Reply(string id, string text, string nextLineId, bool repeatable = true)
    {
      Text = text;
      NextLineId = nextLineId;
      Repeatable = repeatable;
      _id = id;
    }
  }
}
