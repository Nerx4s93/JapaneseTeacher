namespace JapaneseTeacher.GUI
{
    partial class FormMain
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
            this.panelBody = new System.Windows.Forms.Panel();
            this.buttonModuleAlphabets = new JapaneseTeacher.UI.AnimatedPressButton();
            this.buttonModuleStreet = new JapaneseTeacher.UI.AnimatedPressButton();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // panelBody
            // 
            this.panelBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelBody.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBody.Location = new System.Drawing.Point(221, 12);
            this.panelBody.Name = "panelBody";
            this.panelBody.Size = new System.Drawing.Size(725, 527);
            this.panelBody.TabIndex = 0;
            // 
            // buttonModuleAlphabets
            // 
            this.buttonModuleAlphabets.Active = true;
            this.buttonModuleAlphabets.ActiveBackgroundColor = System.Drawing.Color.Lime;
            this.buttonModuleAlphabets.BackColor = System.Drawing.Color.Transparent;
            this.buttonModuleAlphabets.CustomAutoSize = false;
            this.buttonModuleAlphabets.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.buttonModuleAlphabets.ForeColor = System.Drawing.Color.White;
            this.buttonModuleAlphabets.Location = new System.Drawing.Point(12, 51);
            this.buttonModuleAlphabets.Name = "buttonModuleAlphabets";
            this.buttonModuleAlphabets.Size = new System.Drawing.Size(203, 51);
            this.buttonModuleAlphabets.TabIndex = 1;
            this.buttonModuleAlphabets.Text = "Алфавит";
            this.buttonModuleAlphabets.Click += new System.EventHandler(this.buttonModuleAlphabets_Click);
            // 
            // buttonModuleStreet
            // 
            this.buttonModuleStreet.Active = true;
            this.buttonModuleStreet.ActiveBackgroundColor = System.Drawing.Color.Lime;
            this.buttonModuleStreet.BackColor = System.Drawing.Color.Transparent;
            this.buttonModuleStreet.CustomAutoSize = false;
            this.buttonModuleStreet.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.buttonModuleStreet.ForeColor = System.Drawing.Color.White;
            this.buttonModuleStreet.Location = new System.Drawing.Point(12, 108);
            this.buttonModuleStreet.Name = "buttonModuleStreet";
            this.buttonModuleStreet.Size = new System.Drawing.Size(203, 51);
            this.buttonModuleStreet.TabIndex = 2;
            this.buttonModuleStreet.Text = "Улица";
            this.buttonModuleStreet.Click += new System.EventHandler(this.buttonModuleStreet_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 39);
            this.label1.TabIndex = 3;
            this.label1.Text = "Модули";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(958, 551);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonModuleStreet);
            this.Controls.Add(this.buttonModuleAlphabets);
            this.Controls.Add(this.panelBody);
            this.DoubleBuffered = true;
            this.Name = "FormMain";
            this.Text = "FormMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Shown += new System.EventHandler(this.FormMain_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelBody;
        private UI.AnimatedPressButton buttonModuleAlphabets;
        private UI.AnimatedPressButton buttonModuleStreet;
        private System.Windows.Forms.Label label1;
    }
}