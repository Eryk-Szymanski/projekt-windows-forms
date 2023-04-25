namespace Projekt
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileButton = new System.Windows.Forms.ToolStripMenuItem();
            this.newFileButton = new System.Windows.Forms.ToolStripMenuItem();
            this.loadButton = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.saveButton = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitButton = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsButton = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundColorButton = new System.Windows.Forms.ToolStripMenuItem();
            this.helpButton = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutButton = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.brushButton = new System.Windows.Forms.ToolStripButton();
            this.pencilButton = new System.Windows.Forms.ToolStripButton();
            this.sprayButton = new System.Windows.Forms.ToolStripButton();
            this.rubberButton = new System.Windows.Forms.ToolStripButton();
            this.textBoxerButton = new System.Windows.Forms.ToolStripButton();
            this.penColorButton = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.canvasPictureBox = new System.Windows.Forms.PictureBox();
            this.layerList = new System.Windows.Forms.ListView();
            this.Warstwy = new System.Windows.Forms.ColumnHeader();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.addLayerBtn = new System.Windows.Forms.ToolStripButton();
            this.removeLayerBtn = new System.Windows.Forms.ToolStripButton();
            this.duplicateLayerBtn = new System.Windows.Forms.ToolStripButton();
            this.hideLayerBtn = new System.Windows.Forms.ToolStripButton();
            this.penSizeBar = new System.Windows.Forms.TrackBar();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.canvasPictureBox)).BeginInit();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.penSizeBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileButton,
            this.toolsButton,
            this.helpButton});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.menuStrip1.Size = new System.Drawing.Size(927, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileButton
            // 
            this.fileButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newFileButton,
            this.loadButton,
            this.toolStripSeparator,
            this.saveButton,
            this.toolStripSeparator1,
            this.exitButton});
            this.fileButton.Name = "fileButton";
            this.fileButton.Size = new System.Drawing.Size(46, 24);
            this.fileButton.Text = "&Plik";
            // 
            // newFileButton
            // 
            this.newFileButton.Image = ((System.Drawing.Image)(resources.GetObject("newFileButton.Image")));
            this.newFileButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newFileButton.Name = "newFileButton";
            this.newFileButton.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newFileButton.Size = new System.Drawing.Size(193, 26);
            this.newFileButton.Text = "&Nowy";
            this.newFileButton.Click += new System.EventHandler(this.newFileButton_Click);
            // 
            // loadButton
            // 
            this.loadButton.Image = ((System.Drawing.Image)(resources.GetObject("loadButton.Image")));
            this.loadButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.loadButton.Name = "loadButton";
            this.loadButton.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.loadButton.Size = new System.Drawing.Size(193, 26);
            this.loadButton.Text = "&Otwórz";
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(190, 6);
            // 
            // saveButton
            // 
            this.saveButton.Image = ((System.Drawing.Image)(resources.GetObject("saveButton.Image")));
            this.saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveButton.Name = "saveButton";
            this.saveButton.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveButton.Size = new System.Drawing.Size(193, 26);
            this.saveButton.Text = "&Zapisz";
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(190, 6);
            // 
            // exitButton
            // 
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(193, 26);
            this.exitButton.Text = "Wyjdź";
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // toolsButton
            // 
            this.toolsButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backgroundColorButton});
            this.toolsButton.Name = "toolsButton";
            this.toolsButton.Size = new System.Drawing.Size(90, 24);
            this.toolsButton.Text = "Narzędzia";
            // 
            // backgroundColorButton
            // 
            this.backgroundColorButton.Name = "backgroundColorButton";
            this.backgroundColorButton.Size = new System.Drawing.Size(113, 26);
            this.backgroundColorButton.Text = "Tło";
            this.backgroundColorButton.Click += new System.EventHandler(this.selectColor);
            // 
            // helpButton
            // 
            this.helpButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutButton});
            this.helpButton.Name = "helpButton";
            this.helpButton.Size = new System.Drawing.Size(68, 24);
            this.helpButton.Text = "Pomoc";
            // 
            // aboutButton
            // 
            this.aboutButton.Name = "aboutButton";
            this.aboutButton.Size = new System.Drawing.Size(163, 26);
            this.aboutButton.Text = "&Informacje";
            this.aboutButton.Click += new System.EventHandler(this.aboutButton_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Silver;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.brushButton,
            this.pencilButton,
            this.sprayButton,
            this.rubberButton,
            this.textBoxerButton,
            this.penColorButton});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip1.Size = new System.Drawing.Size(30, 178);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // brushButton
            // 
            this.brushButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.brushButton.Image = ((System.Drawing.Image)(resources.GetObject("brushButton.Image")));
            this.brushButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.brushButton.Name = "brushButton";
            this.brushButton.Size = new System.Drawing.Size(29, 24);
            this.brushButton.Text = "Pędzel";
            this.brushButton.Click += new System.EventHandler(this.toolBoxItem_Click);
            // 
            // pencilButton
            // 
            this.pencilButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pencilButton.Image = ((System.Drawing.Image)(resources.GetObject("pencilButton.Image")));
            this.pencilButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pencilButton.Name = "pencilButton";
            this.pencilButton.Size = new System.Drawing.Size(29, 24);
            this.pencilButton.Text = "Ołówek";
            this.pencilButton.Click += new System.EventHandler(this.toolBoxItem_Click);
            // 
            // sprayButton
            // 
            this.sprayButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.sprayButton.Image = ((System.Drawing.Image)(resources.GetObject("sprayButton.Image")));
            this.sprayButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.sprayButton.Name = "sprayButton";
            this.sprayButton.Size = new System.Drawing.Size(29, 24);
            this.sprayButton.Text = "Spray";
            this.sprayButton.Click += new System.EventHandler(this.toolBoxItem_Click);
            // 
            // rubberButton
            // 
            this.rubberButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.rubberButton.Image = ((System.Drawing.Image)(resources.GetObject("rubberButton.Image")));
            this.rubberButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.rubberButton.Name = "rubberButton";
            this.rubberButton.Size = new System.Drawing.Size(29, 24);
            this.rubberButton.Text = "Gumka";
            this.rubberButton.Click += new System.EventHandler(this.toolBoxItem_Click);
            // 
            // textBoxerButton
            // 
            this.textBoxerButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.textBoxerButton.Image = ((System.Drawing.Image)(resources.GetObject("textBoxerButton.Image")));
            this.textBoxerButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.textBoxerButton.Name = "textBoxerButton";
            this.textBoxerButton.Size = new System.Drawing.Size(29, 24);
            this.textBoxerButton.Text = "Pole tekstowe";
            this.textBoxerButton.Click += new System.EventHandler(this.toolBoxItem_Click);
            // 
            // penColorButton
            // 
            this.penColorButton.AutoSize = false;
            this.penColorButton.BackColor = System.Drawing.SystemColors.Desktop;
            this.penColorButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.penColorButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.penColorButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.penColorButton.Name = "penColorButton";
            this.penColorButton.Size = new System.Drawing.Size(20, 20);
            this.penColorButton.Text = "Wybór koloru";
            this.penColorButton.Click += new System.EventHandler(this.selectColor);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.canvasPictureBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.layerList);
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip2);
            this.splitContainer1.Size = new System.Drawing.Size(927, 579);
            this.splitContainer1.SplitterDistance = 705;
            this.splitContainer1.TabIndex = 2;
            // 
            // canvasPictureBox
            // 
            this.canvasPictureBox.BackColor = System.Drawing.Color.White;
            this.canvasPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvasPictureBox.Location = new System.Drawing.Point(0, 0);
            this.canvasPictureBox.Name = "canvasPictureBox";
            this.canvasPictureBox.Size = new System.Drawing.Size(705, 579);
            this.canvasPictureBox.TabIndex = 3;
            this.canvasPictureBox.TabStop = false;
            this.canvasPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.canvasPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.canvasPictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // layerList
            // 
            this.layerList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.layerList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Warstwy});
            this.layerList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layerList.GridLines = true;
            this.layerList.Location = new System.Drawing.Point(0, 0);
            this.layerList.Margin = new System.Windows.Forms.Padding(10);
            this.layerList.Name = "layerList";
            this.layerList.Size = new System.Drawing.Size(218, 552);
            this.layerList.TabIndex = 3;
            this.layerList.UseCompatibleStateImageBehavior = false;
            this.layerList.View = System.Windows.Forms.View.Details;
            this.layerList.Click += new System.EventHandler(this.layerList_Click);
            // 
            // Warstwy
            // 
            this.Warstwy.Text = "Warstwy";
            this.Warstwy.Width = 500;
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackColor = System.Drawing.Color.Silver;
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addLayerBtn,
            this.removeLayerBtn,
            this.duplicateLayerBtn,
            this.hideLayerBtn});
            this.toolStrip2.Location = new System.Drawing.Point(0, 552);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(218, 27);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // addLayerBtn
            // 
            this.addLayerBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addLayerBtn.Image = ((System.Drawing.Image)(resources.GetObject("addLayerBtn.Image")));
            this.addLayerBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addLayerBtn.Name = "addLayerBtn";
            this.addLayerBtn.Size = new System.Drawing.Size(29, 24);
            this.addLayerBtn.Text = "Dodaj warstwę";
            this.addLayerBtn.Click += new System.EventHandler(this.selectLayerButton);
            // 
            // removeLayerBtn
            // 
            this.removeLayerBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.removeLayerBtn.Image = ((System.Drawing.Image)(resources.GetObject("removeLayerBtn.Image")));
            this.removeLayerBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.removeLayerBtn.Name = "removeLayerBtn";
            this.removeLayerBtn.Size = new System.Drawing.Size(29, 24);
            this.removeLayerBtn.Text = "Usuń warstwę";
            this.removeLayerBtn.Click += new System.EventHandler(this.selectLayerButton);
            // 
            // duplicateLayerBtn
            // 
            this.duplicateLayerBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.duplicateLayerBtn.Image = ((System.Drawing.Image)(resources.GetObject("duplicateLayerBtn.Image")));
            this.duplicateLayerBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.duplicateLayerBtn.Name = "duplicateLayerBtn";
            this.duplicateLayerBtn.Size = new System.Drawing.Size(29, 24);
            this.duplicateLayerBtn.Text = "Duplikuj warstwę";
            this.duplicateLayerBtn.Click += new System.EventHandler(this.selectLayerButton);
            // 
            // hideLayerBtn
            // 
            this.hideLayerBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.hideLayerBtn.Image = ((System.Drawing.Image)(resources.GetObject("hideLayerBtn.Image")));
            this.hideLayerBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.hideLayerBtn.Name = "hideLayerBtn";
            this.hideLayerBtn.Size = new System.Drawing.Size(29, 24);
            this.hideLayerBtn.Text = "Ukryj warstwę";
            this.hideLayerBtn.Click += new System.EventHandler(this.selectLayerButton);
            // 
            // penSizeBar
            // 
            this.penSizeBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.penSizeBar.Location = new System.Drawing.Point(0, 0);
            this.penSizeBar.Maximum = 100;
            this.penSizeBar.Minimum = 1;
            this.penSizeBar.Name = "penSizeBar";
            this.penSizeBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.penSizeBar.Size = new System.Drawing.Size(30, 397);
            this.penSizeBar.TabIndex = 5;
            this.penSizeBar.Value = 5;
            this.penSizeBar.Scroll += new System.EventHandler(this.penSizeBar_Scroll);
            // 
            // splitContainer2
            // 
            this.splitContainer2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Left;
            this.splitContainer2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.splitContainer2.Location = new System.Drawing.Point(0, 24);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.toolStrip1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.penSizeBar);
            this.splitContainer2.Size = new System.Drawing.Size(30, 579);
            this.splitContainer2.SplitterDistance = 178;
            this.splitContainer2.TabIndex = 3;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(927, 603);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Not Paint!";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.canvasPictureBox)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.penSizeBar)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileButton;
        private ToolStripMenuItem newFileButton;
        private ToolStripMenuItem loadButton;
        private ToolStripSeparator toolStripSeparator;
        private ToolStripMenuItem saveButton;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem exitButton;
        private ToolStripMenuItem toolsButton;
        private ToolStripMenuItem helpButton;
        private ToolStripMenuItem aboutButton;
        private ToolStrip toolStrip1;
        private ToolStripButton brushButton;
        private ToolStripButton pencilButton;
        private ToolStripButton textBoxerButton;
        private SplitContainer splitContainer1;
        private ToolStrip toolStrip2;
        private ToolStripButton addLayerBtn;
        private ToolStripButton removeLayerBtn;
        private ToolStripButton duplicateLayerBtn;
        private ToolStripButton hideLayerBtn;
        private PictureBox canvasPictureBox;
        private ListView layerList;
        private ToolStripButton sprayButton;
        private ToolStripButton rubberButton;
        private ToolStripButton penColorButton;
        private TrackBar penSizeBar;
        private SplitContainer splitContainer2;
        private ToolStripMenuItem backgroundColorButton;
        private ColumnHeader Warstwy;
    }
}