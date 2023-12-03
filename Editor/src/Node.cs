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
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace Editor
{
  public partial class Node : UserControl
  {
    public BindingList<string> Transitions = [];
    public event EventHandler? OnRemove;
    public event MouseEventHandler? OnSelect;
    public event MouseEventHandler? OnUnselect;

    bool selected = false;
    public Node()
    {
      InitializeComponent();

      ConnectionListBox.DataSource = Transitions;
      OnSelect += OnSelectHandler;
      OnUnselect += OnUnselectHandler;

      foreach (Control control in Controls)
      {
        control.MouseMove += Child_MouseMove;
      }
    }

    private void Child_MouseMove(object? sender, MouseEventArgs eventArgs)
    {
      if (sender is not Control { } child)
        return;

      eventArgs = eventArgs.WithParentCoordinates(this, child);
      this.OnMouseMove(eventArgs);
    }

    private void OnSelectHandler(object? sender, MouseEventArgs e)
    {
      selected = true;
      BackColor = Color.PeachPuff;
    }

    private void OnUnselectHandler(object? sender, MouseEventArgs e)
    {
      selected = false;
      BackColor = SystemColors.Info;
    }

    public void Remove()
    {
      OnRemove?.Invoke(this, EventArgs.Empty);
    }

    private void RemoveToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.Remove();
    }

    [Obsolete]
    private void Node_MouseClick(object sender, MouseEventArgs e)
    {

    }

    int cexit = 0; // todo: remove
    private void addTransitionToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Transitions.Add($"Exit_{cexit++}");
    }

    private void Node_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
        NodeMenuStrip.Show(this, e.Location);

      if (e.Button == MouseButtons.Left)
      {
        if (selected)
          OnUnselect?.Invoke(this, e);
        else
          OnSelect?.Invoke(this, e);
      }
    }

    [Obsolete]
    private void Node_MouseUp(object sender, MouseEventArgs e)
    {

    }

    private void Node_Paint(object sender, PaintEventArgs e)
    {

    }

    private void Node_MouseMove(object? sender, MouseEventArgs e)
    {

    }
  }
}
