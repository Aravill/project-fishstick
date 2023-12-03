namespace Editor
{
    partial class Canvas
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            ContextMenu = new ContextMenuStrip(components);
            newNodeToolStripMenuItem = new ToolStripMenuItem();
            ContextMenu.SuspendLayout();
            SuspendLayout();
            // 
            // ContextMenu
            // 
            ContextMenu.Items.AddRange(new ToolStripItem[] { newNodeToolStripMenuItem });
            ContextMenu.Name = "ContextMenu";
            ContextMenu.Size = new Size(131, 26);
            // 
            // newNodeToolStripMenuItem
            // 
            newNodeToolStripMenuItem.Name = "newNodeToolStripMenuItem";
            newNodeToolStripMenuItem.Size = new Size(130, 22);
            newNodeToolStripMenuItem.Text = "New Node";
            newNodeToolStripMenuItem.Click += NewNodeToolStripMenuItem_Click;
            // 
            // Canvas
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BorderStyle = BorderStyle.FixedSingle;
            Name = "Canvas";
            Size = new Size(298, 298);
            DragDrop += Canvas_DragDrop;
            DragEnter += Canvas_DragEnter;
            Paint += Canvas_Paint;
            MouseDown += Canvas_MouseDown;
            MouseMove += Canvas_MouseMove;
            ContextMenu.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ContextMenuStrip ContextMenu;
        private ToolStripMenuItem newNodeToolStripMenuItem;
    }
}
