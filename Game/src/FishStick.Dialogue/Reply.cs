using System.Runtime.Serialization;

namespace Dialogue
{
  [DataContract]
  class Reply : IReply
  {
    [DataMember]
    public string Text { get; }

    [DataMember]
    public string NextLineId { get; }

    [DataMember]
    public bool WasUsed { get; set; }

    [DataMember]
    public bool Repeatable { get; }

    public Reply(string text, string nextLineId, bool repeatable = true)
    {
      Text = text;
      NextLineId = nextLineId;
      WasUsed = false;
      Repeatable = repeatable;
    }
  }
}
