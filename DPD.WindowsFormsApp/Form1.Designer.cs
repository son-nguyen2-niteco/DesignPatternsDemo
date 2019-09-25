namespace DPD.WindowsFormsApp
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
            this.doTwoThingsButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // doTwoThingsButton
            // 
            this.doTwoThingsButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular,
                System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.doTwoThingsButton.Location = new System.Drawing.Point(216, 114);
            this.doTwoThingsButton.Name = "doTwoThingsButton";
            this.doTwoThingsButton.Size = new System.Drawing.Size(160, 38);
            this.doTwoThingsButton.TabIndex = 0;
            this.doTwoThingsButton.Text = "Do two things";
            this.doTwoThingsButton.UseVisualStyleBackColor = true;
            this.doTwoThingsButton.Click += new System.EventHandler(this.doTwoThingsButton_Click1);
            this.doTwoThingsButton.Click += new System.EventHandler(this.doTwoThingsButton_Click2);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 283);
            this.Controls.Add(this.doTwoThingsButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button doTwoThingsButton;
    }
}