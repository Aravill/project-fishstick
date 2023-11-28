using System.Text.RegularExpressions;

namespace FishStick.Render
{
  public static class StringExtensions
{
  public static string RemoveTagMarkers(this string text) => text.Replace("{", "").Replace("}", "");
  public static string FindTags(this string text, out List<string> tags)
  {
    tags = Regex.Matches(text, @"\{([\w ]+)\}", RegexOptions.IgnoreCase).Select(match => match.Groups[1].Value).ToList();
    return text;
  }
  public static IEnumerable<string> Concat(this string text, IEnumerable<string> next)
  {
    return toEnumerable(text).Concat(next);

    // hax
    IEnumerable<string> toEnumerable(string toEnum)
    {
      yield return toEnum;
    }
  }
}
}