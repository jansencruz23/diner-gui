namespace Diner.Forms.UserControls
{
    partial class EntreesView
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
            this.components = new System.ComponentModel.Container();
            this.flowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.lblType = new System.Windows.Forms.Label();
            this.dragControl = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.SuspendLayout();
            // 
            // flowPanel
            // 
            this.flowPanel.AutoScroll = true;
            this.flowPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(31)))));
            this.flowPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowPanel.Location = new System.Drawing.Point(0, 0);
            this.flowPanel.Margin = new System.Windows.Forms.Padding(0, 50, 0, 0);
            this.flowPanel.Name = "flowPanel";
            this.flowPanel.Padding = new System.Windows.Forms.Padding(0, 70, 0, 0);
            this.flowPanel.Size = new System.Drawing.Size(874, 530);
            this.flowPanel.TabIndex = 0;
            // 
            // lblType
            // 
            this.lblType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(31)))));
            this.lblType.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblType.Font = new System.Drawing.Font("Arial Rounded MT Bold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblType.ForeColor = System.Drawing.Color.White;
            this.lblType.Location = new System.Drawing.Point(0, 0);
            this.lblType.Name = "lblType";
            this.lblType.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblType.Size = new System.Drawing.Size(874, 59);
            this.lblType.TabIndex = 1;
            this.lblType.Text = "label1";
            this.lblType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dragControl
            // 
            this.dragControl.DockIndicatorTransparencyValue = 0.6D;
            this.dragControl.DragStartTransparencyValue = 0.75D;
            this.dragControl.TargetControl = this.lblType;
            this.dragControl.UseTransparentDrag = true;
            // 
            // EntreesView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(31)))));
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.flowPanel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "EntreesView";
            this.Size = new System.Drawing.Size(874, 530);
            this.ResumeLayout(false);

        }

        #endregion

        private FlowLayoutPanel flowPanel;
        private Label lblType;
        private Guna.UI2.WinForms.Guna2DragControl dragControl;
    }
}
