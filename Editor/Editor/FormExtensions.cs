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

        public static MouseEventHandler GetHandler_ChildControl_MouseMove(this Control thisControl) 
            => (object? sender, MouseEventArgs e) => ChildControl_MouseMove(thisControl, sender, e);
        private static void ChildControl_MouseMove(Control thisControl, object? sender, MouseEventArgs e)
        {
            if (sender is not Control { } childControl)
                return;

            Point pointInParent = childControl.PointToScreen(e.Location);
            pointInParent = thisControl.PointToClient(pointInParent);

            // Invoke the parent's MouseMove event manually
            MouseEventArgs argsForParent = new MouseEventArgs(e.Button, e.Clicks, pointInParent.X, pointInParent.Y, e.Delta);
            thisControl.OnMouseMove(argsForParent);
        }

        private static void OnMouseMove(this Control thisControl, MouseEventArgs argsForParent)
        {
            //thisControl.OnMouseMove(argsForParent);

            MethodInfo methodInfo = typeof(Control).GetMethod("OnMouseMove", BindingFlags.NonPublic | BindingFlags.Instance);

            if (methodInfo != null)
            {
                // Call the private method on the object
                methodInfo.Invoke(thisControl, new[] { argsForParent });
            }
            else
            {
                throw new MissingMethodException("PrivateMethod not found");
            }
        }
    }
}
