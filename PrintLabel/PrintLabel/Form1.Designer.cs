namespace PrintLabel
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.connectType = new System.Windows.Forms.ComboBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.lstPrinters = new System.Windows.Forms.ListView();
            this.btnFind = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // connectType
            // 
            this.connectType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.connectType.FormattingEnabled = true;
            this.connectType.Location = new System.Drawing.Point(13, 13);
            this.connectType.Name = "connectType";
            this.connectType.Size = new System.Drawing.Size(80, 21);
            this.connectType.TabIndex = 0;
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(181, 13);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 1;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(262, 13);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(75, 23);
            this.btnPreview.TabIndex = 2;
            this.btnPreview.Text = "Preview";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // lstPrinters
            // 
            this.lstPrinters.HideSelection = false;
            this.lstPrinters.Location = new System.Drawing.Point(13, 41);
            this.lstPrinters.MultiSelect = false;
            this.lstPrinters.Name = "lstPrinters";
            this.lstPrinters.Size = new System.Drawing.Size(334, 137);
            this.lstPrinters.TabIndex = 3;
            this.lstPrinters.UseCompatibleStateImageBehavior = false;
            this.lstPrinters.View = System.Windows.Forms.View.Details;
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(100, 13);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(75, 23);
            this.btnFind.TabIndex = 4;
            this.btnFind.Text = "Find Printer";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 208);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.lstPrinters);
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.connectType);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox connectType;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.ListView lstPrinters;
        private System.Windows.Forms.Button btnFind;
    }
}

