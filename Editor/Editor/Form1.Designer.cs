namespace Editor
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            contextMenuStrip1 = new ContextMenuStrip(components);
            AddNodeToolStripMenuItem = new ToolStripMenuItem();
            treeView = new TreeView();
            NodeCanvas = new Canvas();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { AddNodeToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(127, 26);
            // 
            // AddNodeToolStripMenuItem
            // 
            AddNodeToolStripMenuItem.Name = "AddNodeToolStripMenuItem";
            AddNodeToolStripMenuItem.Size = new Size(126, 22);
            AddNodeToolStripMenuItem.Text = "Add node";
            // 
            // treeView
            // 
            treeView.AllowDrop = true;
            treeView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            treeView.Location = new Point(12, 12);
            treeView.Name = "treeView";
            treeView.Size = new Size(169, 426);
            treeView.TabIndex = 1;
            treeView.ItemDrag += TreeView_ItemDrag;
            // 
            // NodeCanvas
            // 
            NodeCanvas.AllowDrop = true;
            NodeCanvas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            NodeCanvas.AutoScroll = true;
            NodeCanvas.BorderStyle = BorderStyle.FixedSingle;
            NodeCanvas.Location = new Point(187, 12);
            NodeCanvas.Name = "NodeCanvas";
            NodeCanvas.Size = new Size(601, 426);
            NodeCanvas.TabIndex = 2;
            // 
            // MainForm
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(NodeCanvas);
            Controls.Add(treeView);
            Name = "MainForm";
            Text = "Editor";
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem AddNodeToolStripMenuItem;
        private TreeView treeView;
        private Canvas NodeCanvas;
    }
}
