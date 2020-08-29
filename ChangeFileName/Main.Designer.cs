namespace ChangeFileName
{
    partial class Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.change = new System.Windows.Forms.Button();
            this.hasBeChanged = new System.Windows.Forms.TextBox();
            this.changeTo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.fileSelected = new System.Windows.Forms.ListBox();
            this.selectFileToChange = new System.Windows.Forms.Button();
            this.fileSelectedMedia = new System.Windows.Forms.ListBox();
            this.selectMedia = new System.Windows.Forms.Button();
            this.renamesort = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // change
            // 
            this.change.Location = new System.Drawing.Point(39, 415);
            this.change.Name = "change";
            this.change.Size = new System.Drawing.Size(75, 23);
            this.change.TabIndex = 0;
            this.change.Text = "修改";
            this.change.UseVisualStyleBackColor = true;
            this.change.Click += new System.EventHandler(this.Change_Click);
            // 
            // hasBeChanged
            // 
            this.hasBeChanged.Location = new System.Drawing.Point(146, 366);
            this.hasBeChanged.Name = "hasBeChanged";
            this.hasBeChanged.Size = new System.Drawing.Size(100, 21);
            this.hasBeChanged.TabIndex = 1;
            // 
            // changeTo
            // 
            this.changeTo.Location = new System.Drawing.Point(146, 388);
            this.changeTo.Name = "changeTo";
            this.changeTo.Size = new System.Drawing.Size(100, 21);
            this.changeTo.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 369);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "需要替换的文字：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 391);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "替换为：";
            // 
            // fileSelected
            // 
            this.fileSelected.ItemHeight = 12;
            this.fileSelected.Location = new System.Drawing.Point(39, 50);
            this.fileSelected.Name = "fileSelected";
            this.fileSelected.Size = new System.Drawing.Size(409, 304);
            this.fileSelected.TabIndex = 5;
            // 
            // selectFileToChange
            // 
            this.selectFileToChange.Location = new System.Drawing.Point(39, 21);
            this.selectFileToChange.Name = "selectFileToChange";
            this.selectFileToChange.Size = new System.Drawing.Size(135, 23);
            this.selectFileToChange.TabIndex = 6;
            this.selectFileToChange.Text = "选择需要修改的文件";
            this.selectFileToChange.UseVisualStyleBackColor = true;
            this.selectFileToChange.Click += new System.EventHandler(this.Select_Click);
            // 
            // fileSelectedMedia
            // 
            this.fileSelectedMedia.FormattingEnabled = true;
            this.fileSelectedMedia.ItemHeight = 12;
            this.fileSelectedMedia.Location = new System.Drawing.Point(454, 50);
            this.fileSelectedMedia.Name = "fileSelectedMedia";
            this.fileSelectedMedia.Size = new System.Drawing.Size(500, 304);
            this.fileSelectedMedia.TabIndex = 7;
            // 
            // selectMedia
            // 
            this.selectMedia.Location = new System.Drawing.Point(454, 21);
            this.selectMedia.Name = "selectMedia";
            this.selectMedia.Size = new System.Drawing.Size(107, 23);
            this.selectMedia.TabIndex = 8;
            this.selectMedia.Text = "选择对应的媒体文件";
            this.selectMedia.UseVisualStyleBackColor = true;
            this.selectMedia.Click += new System.EventHandler(this.selectMedia_Click);
            // 
            // renamesort
            // 
            this.renamesort.Location = new System.Drawing.Point(121, 415);
            this.renamesort.Name = "renamesort";
            this.renamesort.Size = new System.Drawing.Size(101, 23);
            this.renamesort.TabIndex = 9;
            this.renamesort.Text = "重新命名排序";
            this.renamesort.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.renamesort.UseVisualStyleBackColor = true;
            this.renamesort.Click += new System.EventHandler(this.renamesort_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(966, 458);
            this.Controls.Add(this.renamesort);
            this.Controls.Add(this.selectMedia);
            this.Controls.Add(this.fileSelectedMedia);
            this.Controls.Add(this.selectFileToChange);
            this.Controls.Add(this.fileSelected);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.changeTo);
            this.Controls.Add(this.hasBeChanged);
            this.Controls.Add(this.change);
            this.Name = "Main";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button change;
        private System.Windows.Forms.TextBox hasBeChanged;
        private System.Windows.Forms.TextBox changeTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox fileSelected;
        private System.Windows.Forms.Button selectFileToChange;
        private System.Windows.Forms.ListBox fileSelectedMedia;
        private System.Windows.Forms.Button selectMedia;
        private System.Windows.Forms.Button renamesort;
    }
}

