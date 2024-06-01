using System.Text.RegularExpressions;

namespace FishStick.Extensions
{
  public static partial class StringExtensions
  {
    public static string RemoveTags(this string text) =>
      text.Replace("{", "").Replace("}", "");

    public static List<string> FindTaggedWords(this string text) => TaggedWordRegex().Matches(text)
                                                                                     .Select(match => match.Groups[1].Value)
                                                                                     .ToList();

    public static IEnumerable<string> Concat(this string text, IEnumerable<string> next)
    {
      return toEnumerable(text).Concat(next);

      // hax
      IEnumerable<string> toEnumerable(string toEnum)
      {
        yield return toEnum;
      }
    }

    [GeneratedRegex(@"\{([\w ]+)\}", RegexOptions.IgnoreCase, "cs-CZ")]
    private static partial Regex TaggedWordRegex();
  }
}
