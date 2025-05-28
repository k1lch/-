namespace WinQuestion
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
            btnyes = new Button();
            btnno = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // btnyes
            // 
            btnyes.Location = new Point(27, 78);
            btnyes.Name = "btnyes";
            btnyes.Size = new Size(103, 40);
            btnyes.TabIndex = 0;
            btnyes.Text = "Да";
            btnyes.UseVisualStyleBackColor = true;
            btnyes.Click += btnyes_Click;
            // 
            // btnno
            // 
            btnno.Location = new Point(206, 78);
            btnno.Name = "btnno";
            btnno.Size = new Size(103, 40);
            btnno.TabIndex = 1;
            btnno.Text = "Нет";
            btnno.UseVisualStyleBackColor = true;
            btnno.MouseMove += btnno_MouseMove;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(91, 29);
            label1.Name = "label1";
            label1.Size = new Size(136, 15);
            label1.TabIndex = 2;
            label1.Text = "Вы довольны своей зп?";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(330, 157);
            Controls.Add(label1);
            Controls.Add(btnno);
            Controls.Add(btnyes);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "Form1";
            Text = "Насущный вопрос";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnyes;
        private Button btnno;
        private Label label1;
    }
}
