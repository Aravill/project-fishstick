using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Editor
{
    public partial class Canvas : UserControl
    {
        List<Node> nodes = [];
        Point lastClick = new();
        Node? selectedNode;
        Point? selectPoint;
        List<(Node a, Node b)> connections = [];
        public Canvas()
        {
            InitializeComponent();
        }

        private void NewNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNode(lastClick);
        }

        private Node CreateNode(Point location)
        {
            Node node = new() { Location = location };
            node.OnRemove += RemoveNode;
            node.MouseMove += this.GetHandler_ChildControl_MouseMove();
            node.OnSelect += SelectNode;
            node.OnUnselect += UnselectNode;

            nodes.Add(node);
            Controls.Add(node);

            return node;
        }

    private void Child_MouseMove(object? sender, MouseEventArgs eventArgs)
    {
      if (sender is not Control { } child)
        return;

      eventArgs = eventArgs.WithParentCoordinates(this, child);
      this.OnMouseMove(eventArgs);
    }

    public void AddNode(Point location)
    {
      // TODO: Přepsat na přidání
      CreateNode(location);
    }
    private void SelectNode(object? sender, MouseEventArgs e)
    {
      if (sender is not Node { } node)
        return;

      selectedNode ??= node;
      selectPoint ??= e.Location;

      Controls.SetChildIndex(selectedNode, 0);
    }

        private void UnselectNode(object? sender, MouseEventArgs e)
        {
            selectedNode = null;
            selectPoint = null;
        }

        private void RemoveNode(object? sender, EventArgs e)
        {
            if (sender is not Node { } node)
                return;

            nodes.Remove(node);
            Controls.Remove(node);
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (selectedNode is null || selectPoint is null) return;

            selectedNode.Location = new Point(e.Location.X - selectPoint!.Value.X,
                                              e.Location.Y - selectPoint!.Value.Y);
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            connections.ForEach(connection =>
            {
                e.Graphics.DrawLine(Pens.Black,
                                    connection.a.Location,
                                    connection.b.Location);
            });
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                ContextMenu.Show(this, lastClick = e.Location);
        }

        private void Canvas_DragEnter(object sender, DragEventArgs e)
        {
            e.Message = "Add node.";
            e.Effect = DragDropEffects.Copy;
        }
        protected override Point ScrollToControl(Control activeControl)
        {
            Point pt = this.AutoScrollPosition;
            return pt;
        }

        private void Canvas_DragDrop(object sender, DragEventArgs e)
        {
            AddNode(PointToClient(new Point(e.X, e.Y)));
        }
    }
}
