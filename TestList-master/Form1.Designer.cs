﻿namespace TestList
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
            groupBox1 = new GroupBox();
            memberList = new CheckedListBox();
            peopleList = new ComboBox();
            buttonAdd = new Button();
            buttonDelete = new Button();
            buttonSort = new Button();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Location = new Point(17, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(84, 60);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Список участников";
            // 
            // memberList
            // 
            memberList.FormattingEnabled = true;
            memberList.Location = new Point(119, 14);
            memberList.Name = "memberList";
            memberList.Size = new Size(96, 58);
            memberList.TabIndex = 1;
            memberList.SelectedIndexChanged += checkedListBox1_SelectedIndexChanged;
            // 
            // peopleList
            // 
            peopleList.FormattingEnabled = true;
            peopleList.Items.AddRange(new object[] { "Иванов И. И.", "Петров П. П.", "Василий П. П." });
            peopleList.Location = new Point(234, 12);
            peopleList.Name = "peopleList";
            peopleList.Size = new Size(74, 23);
            peopleList.TabIndex = 2;
            peopleList.SelectedIndexChanged += peopleList_SelectedIndexChanged;
            // 
            // buttonAdd
            // 
            buttonAdd.Location = new Point(12, 92);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new Size(89, 32);
            buttonAdd.TabIndex = 3;
            buttonAdd.Text = "Добавить";
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd.Click += buttonAdd_Click;
            // 
            // buttonDelete
            // 
            buttonDelete.Location = new Point(119, 92);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(96, 32);
            buttonDelete.TabIndex = 4;
            buttonDelete.Text = "Удалить";
            buttonDelete.UseVisualStyleBackColor = true;
            buttonDelete.Click += buttonDelete_Click;
            // 
            // buttonSort
            // 
            buttonSort.Location = new Point(234, 92);
            buttonSort.Name = "buttonSort";
            buttonSort.Size = new Size(94, 32);
            buttonSort.TabIndex = 5;
            buttonSort.Text = "Сортировать";
            buttonSort.UseVisualStyleBackColor = true;
            buttonSort.Click += buttonSort_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(394, 271);
            Controls.Add(buttonSort);
            Controls.Add(buttonDelete);
            Controls.Add(buttonAdd);
            Controls.Add(peopleList);
            Controls.Add(memberList);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "Form1";
            Text = "Работа со списками";
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private CheckedListBox memberList;
        private ComboBox peopleList;
        private Button buttonAdd;
        private Button buttonDelete;
        private Button buttonSort;
    }
}
