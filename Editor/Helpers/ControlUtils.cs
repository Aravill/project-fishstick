using Avalonia.Controls;

namespace AvaloniaEditor.Helpers
{
  public static class ControlUtils
  {
    public static T? FindParentOfType<T>(Control control) where T : class
    {
      var parent = control.Parent;
      while (parent != null)
      {
        if (parent is T)
          return parent as T;
        parent = parent.Parent;
      }
      return null;
    }
  }
}