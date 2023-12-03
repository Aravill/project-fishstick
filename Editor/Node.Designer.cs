namespace Editor
{
    partial class Node
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
      NodeLabel = new Label();
      NodeMenuStrip = new ContextMenuStrip(components);
      addTransitionToolStripMenuItem = new ToolStripMenuItem();
      deleteToolStripMenuItem = new ToolStripMenuItem();
      ConnectionListBox = new ListBox();
      ConnectionListLabel = new Label();
      NodeMenuStrip.SuspendLayout();
      SuspendLayout();
      // 
      // NodeLabel
      // 
      NodeLabel.AutoSize = true;
      NodeLabel.Location = new Point(5, 2);
      NodeLabel.Name = "NodeLabel";
      NodeLabel.Size = new Size(69, 15);
      NodeLabel.TabIndex = 0;
      NodeLabel.Text = "I am a node";
      // 
      // NodeMenuStrip
      // 
      NodeMenuStrip.Items.AddRange(new ToolStripItem[] { addTransitionToolStripMenuItem, deleteToolStripMenuItem });
      NodeMenuStrip.Name = "NodeMenuStrip";
      NodeMenuStrip.Size = new Size(151, 48);
      // 
      // addTransitionToolStripMenuItem
      // 
      addTransitionToolStripMenuItem.Name = "addTransitionToolStripMenuItem";
      addTransitionToolStripMenuItem.Size = new Size(150, 22);
      addTransitionToolStripMenuItem.Text = "Add Transition";
      addTransitionToolStripMenuItem.Click += addTransitionToolStripMenuItem_Click;
      // 
      // deleteToolStripMenuItem
      // 
      deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
      deleteToolStripMenuItem.Size = new Size(150, 22);
      deleteToolStripMenuItem.Text = "Remove";
      deleteToolStripMenuItem.Click += RemoveToolStripMenuItem_Click;
      // 
      // ConnectionListBox
      // 
      ConnectionListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      ConnectionListBox.FormattingEnabled = true;
      ConnectionListBox.ItemHeight = 15;
      ConnectionListBox.Location = new Point(5, 35);
      ConnectionListBox.Name = "ConnectionListBox";
      ConnectionListBox.RightToLeft = RightToLeft.No;
      ConnectionListBox.Size = new Size(178, 169);
      ConnectionListBox.TabIndex = 1;
      // 
      // ConnectionListLabel
      // 
      ConnectionListLabel.AutoSize = true;
      ConnectionListLabel.Location = new Point(5, 17);
      ConnectionListLabel.Name = "ConnectionListLabel";
      ConnectionListLabel.Size = new Size(69, 15);
      ConnectionListLabel.TabIndex = 2;
      ConnectionListLabel.Text = "Connection";
      // 
      // Node
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      BackColor = SystemColors.Info;
      BorderStyle = BorderStyle.FixedSingle;
      Controls.Add(ConnectionListLabel);
      Controls.Add(ConnectionListBox);
      Controls.Add(NodeLabel);
      Name = "Node";
      Padding = new Padding(2);
      Size = new Size(188, 219);
      Paint += Node_Paint;
      MouseClick += Node_MouseClick;
      MouseDown += Node_MouseDown;
      MouseMove += Node_MouseMove;
      MouseUp += Node_MouseUp;
      NodeMenuStrip.ResumeLayout(false);
      ResumeLayout(false);
      PerformLayout();
    }

    #endregion

    private Label NodeLabel;
        private ContextMenuStrip NodeMenuStrip;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private Label ConnectionListLabel;
        private ToolStripMenuItem addTransitionToolStripMenuItem;
    public ListBox ConnectionListBox;
  }
}
