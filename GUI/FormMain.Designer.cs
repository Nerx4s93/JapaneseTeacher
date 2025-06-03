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
            this.moduleHeader1 = new JapaneseTeacher.UI.ModuleHeader();
            this.SuspendLayout();
            // 
            // moduleHeader1
            // 
            this.moduleHeader1.BackColor = System.Drawing.Color.Transparent;
            this.moduleHeader1.BackgroundColor = System.Drawing.Color.Orange;
            this.moduleHeader1.Description = "Описание";
            this.moduleHeader1.DescriptionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.moduleHeader1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold);
            this.moduleHeader1.Location = new System.Drawing.Point(61, 12);
            this.moduleHeader1.Name = "moduleHeader1";
            this.moduleHeader1.Size = new System.Drawing.Size(654, 107);
            this.moduleHeader1.TabIndex = 0;
            this.moduleHeader1.Tag = "1";
            this.moduleHeader1.Text = "moduleHeader1";
            this.moduleHeader1.Theme = "Тема";
            this.moduleHeader1.ThemeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(179)))));
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.moduleHeader1);
            this.CurrentSceneId = 1;
            this.DoubleBuffered = true;
            this.Name = "FormMain";
            this.Text = "Изучение японского";
            this.Shown += new System.EventHandler(this.FormMain_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private UI.ModuleHeader moduleHeader1;
    }
}