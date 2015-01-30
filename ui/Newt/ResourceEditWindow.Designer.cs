namespace voidsoft.Newt
{
    partial class ResourceEditWindow
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
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.textBoxCultureInfo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxResourceValue = new System.Windows.Forms.TextBox();
            this.textBoxResourceKey = new System.Windows.Forms.TextBox();
            this.textBoxClassName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.textBoxCultureInfo);
            this.groupBox.Controls.Add(this.label4);
            this.groupBox.Controls.Add(this.textBoxResourceValue);
            this.groupBox.Controls.Add(this.textBoxResourceKey);
            this.groupBox.Controls.Add(this.textBoxClassName);
            this.groupBox.Controls.Add(this.label3);
            this.groupBox.Controls.Add(this.label2);
            this.groupBox.Controls.Add(this.label1);
            this.groupBox.Location = new System.Drawing.Point(4, -2);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(357, 310);
            this.groupBox.TabIndex = 0;
            this.groupBox.TabStop = false;
            // 
            // textBoxCultureInfo
            // 
            this.textBoxCultureInfo.Location = new System.Drawing.Point(120, 13);
            this.textBoxCultureInfo.Name = "textBoxCultureInfo";
            this.textBoxCultureInfo.Size = new System.Drawing.Size(229, 20);
            this.textBoxCultureInfo.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(53, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Culture info";
            // 
            // textBoxResourceValue
            // 
            this.textBoxResourceValue.Location = new System.Drawing.Point(119, 109);
            this.textBoxResourceValue.Multiline = true;
            this.textBoxResourceValue.Name = "textBoxResourceValue";
            this.textBoxResourceValue.Size = new System.Drawing.Size(230, 195);
            this.textBoxResourceValue.TabIndex = 3;
            // 
            // textBoxResourceKey
            // 
            this.textBoxResourceKey.Location = new System.Drawing.Point(119, 77);
            this.textBoxResourceKey.Name = "textBoxResourceKey";
            this.textBoxResourceKey.Size = new System.Drawing.Size(230, 20);
            this.textBoxResourceKey.TabIndex = 2;
            // 
            // textBoxClassName
            // 
            this.textBoxClassName.Location = new System.Drawing.Point(120, 44);
            this.textBoxClassName.Name = "textBoxClassName";
            this.textBoxClassName.Size = new System.Drawing.Size(229, 20);
            this.textBoxClassName.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Resource value";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Resource key";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(52, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Class name";
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(265, 314);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(88, 32);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "&OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // ResourceEditWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 358);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.groupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ResourceEditWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Resource";
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox textBoxResourceValue;
        public System.Windows.Forms.TextBox textBoxResourceKey;
        public System.Windows.Forms.TextBox textBoxClassName;
        public System.Windows.Forms.TextBox textBoxCultureInfo;
        private System.Windows.Forms.Label label4;
    }
}