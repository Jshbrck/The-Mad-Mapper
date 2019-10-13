using MapGenerator.Entities;

namespace MapGenerator
{
    partial class MainWindow
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MainScreen = new MapGenerator.Entities.GameWindow();
            this.TileSelector = new MapGenerator.Entities.TileSelectionBox();
            this.TilePreview = new MapGenerator.Entities.SidePanel();
            this.SuspendLayout();
            // 
            // MainScreen
            // 
            this.MainScreen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainScreen.Location = new System.Drawing.Point(12, 12);
            this.MainScreen.Name = "MainScreen";
            this.MainScreen.Size = new System.Drawing.Size(968, 800);
            this.MainScreen.TabIndex = 0;
            // 
            // TileSelector
            // 
            this.TileSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TileSelector.FormattingEnabled = true;
            this.TileSelector.Location = new System.Drawing.Point(996, 12);
            this.TileSelector.Name = "TileSelector";
            this.TileSelector.Size = new System.Drawing.Size(206, 602);
            this.TileSelector.TabIndex = 6;
            this.TileSelector.SelectedIndexChanged += new System.EventHandler(this.TileSelector_SelectedIndexChanged);
            // 
            // TilePreview
            // 
            this.TilePreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.TilePreview.GetAutoInvalidation = false;
            this.TilePreview.Location = new System.Drawing.Point(996, 624);
            this.TilePreview.Name = "TilePreview";
            this.TilePreview.Size = new System.Drawing.Size(206, 188);
            this.TilePreview.TabIndex = 5;
            this.TilePreview.Text = "sidePanel1";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1214, 823);
            this.Controls.Add(this.TileSelector);
            this.Controls.Add(this.TilePreview);
            this.Controls.Add(this.MainScreen);
            this.Name = "MainWindow";
            this.Text = "MainWindow";
            this.ResumeLayout(false);

        }

        #endregion
        private GameWindow MainScreen;
        private SidePanel TilePreview;
        private TileSelectionBox TileSelector;
    }
}