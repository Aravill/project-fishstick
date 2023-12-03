using System.Xml.Linq;

namespace Editor
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            var node = treeView.Nodes.Add("Hello node!");
            node.Nodes.Add("subnode 1");
            node.Nodes.Add("subnode 2");

            var node2 = treeView.Nodes.Add("Hello node 2!");
            node2.Nodes.Add("subnode 1");
            node2.Nodes.Add("subnode 2");
        }

        private void TreeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Copy);
        }
    }

    static class Extensions
    {
        public static void AddConnection(this List<(Node a, Node b)> list, Node a, Node b) =>
            list.Add((a, b));
    }
}
