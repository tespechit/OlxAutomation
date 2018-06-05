namespace OlxAutomation
{
    partial class Form1
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
            this.BtoMonitor = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtoMonitor
            // 
            this.BtoMonitor.Location = new System.Drawing.Point(12, 12);
            this.BtoMonitor.Name = "BtoMonitor";
            this.BtoMonitor.Size = new System.Drawing.Size(138, 64);
            this.BtoMonitor.TabIndex = 0;
            this.BtoMonitor.Text = "Monitorar Olx";
            this.BtoMonitor.UseVisualStyleBackColor = true;
            this.BtoMonitor.Click += new System.EventHandler(this.BtoMonitor_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.BtoMonitor);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtoMonitor;
    }
}

