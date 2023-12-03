using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Editor
{
  static class FormExtensions
  {
    public static MouseEventArgs WithParentCoordinates(this MouseEventArgs eventArgs, Control parent, Control child)
    {
      Point pointInParent = child.PointToScreen(eventArgs.Location);
      pointInParent = parent.PointToClient(pointInParent);

      return new MouseEventArgs(eventArgs.Button, eventArgs.Clicks, pointInParent.X, pointInParent.Y, eventArgs.Delta);
    }
  }
}
