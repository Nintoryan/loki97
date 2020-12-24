namespace loki97
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
            this.encode = new System.Windows.Forms.Button();
            this.decode = new System.Windows.Forms.Button();
            this.decodedText = new System.Windows.Forms.RichTextBox();
            this.encodedText = new System.Windows.Forms.RichTextBox();
            this.decodedBytes = new System.Windows.Forms.RichTextBox();
            this.encodedBytes = new System.Windows.Forms.RichTextBox();
            this.key = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // encode
            // 
            this.encode.Location = new System.Drawing.Point(12, 479);
            this.encode.Name = "encode";
            this.encode.Size = new System.Drawing.Size(249, 42);
            this.encode.TabIndex = 0;
            this.encode.Text = "Зашифровать";
            this.encode.UseVisualStyleBackColor = true;
            this.encode.Click += new System.EventHandler(this.encode_Click);
            // 
            // decode
            // 
            this.decode.Location = new System.Drawing.Point(539, 479);
            this.decode.Name = "decode";
            this.decode.Size = new System.Drawing.Size(249, 42);
            this.decode.TabIndex = 1;
            this.decode.Text = "Расшифровать";
            this.decode.UseVisualStyleBackColor = true;
            this.decode.Click += new System.EventHandler(this.decode_Click);
            // 
            // decodedText
            // 
            this.decodedText.Location = new System.Drawing.Point(0, 30);
            this.decodedText.Name = "decodedText";
            this.decodedText.Size = new System.Drawing.Size(360, 128);
            this.decodedText.TabIndex = 2;
            this.decodedText.Text = "Тестовые данные для зашифровки";
            // 
            // encodedText
            // 
            this.encodedText.Location = new System.Drawing.Point(431, 30);
            this.encodedText.Name = "encodedText";
            this.encodedText.Size = new System.Drawing.Size(357, 128);
            this.encodedText.TabIndex = 3;
            this.encodedText.Text = "";
            // 
            // decodedBytes
            // 
            this.decodedBytes.Location = new System.Drawing.Point(0, 229);
            this.decodedBytes.Name = "decodedBytes";
            this.decodedBytes.Size = new System.Drawing.Size(360, 129);
            this.decodedBytes.TabIndex = 4;
            this.decodedBytes.Text = "";
            // 
            // encodedBytes
            // 
            this.encodedBytes.Location = new System.Drawing.Point(428, 229);
            this.encodedBytes.Name = "encodedBytes";
            this.encodedBytes.Size = new System.Drawing.Size(360, 129);
            this.encodedBytes.TabIndex = 5;
            this.encodedBytes.Text = "";
            // 
            // key
            // 
            this.key.Location = new System.Drawing.Point(204, 425);
            this.key.Name = "key";
            this.key.Size = new System.Drawing.Size(360, 34);
            this.key.TabIndex = 6;
            this.key.Text = "123456789FACB";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(101, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "Расшифрованный текст";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(101, 209);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(172, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "Расшифрованные байты";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(536, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(160, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "Зашифрованный текст";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(536, 209);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(165, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "Зашифрованные байты";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(371, 405);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 17);
            this.label5.TabIndex = 11;
            this.label5.Text = "Ключ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 533);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.key);
            this.Controls.Add(this.encodedBytes);
            this.Controls.Add(this.decodedBytes);
            this.Controls.Add(this.encodedText);
            this.Controls.Add(this.decodedText);
            this.Controls.Add(this.decode);
            this.Controls.Add(this.encode);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button encode;
        private System.Windows.Forms.Button decode;
        private System.Windows.Forms.RichTextBox decodedText;
        private System.Windows.Forms.RichTextBox encodedText;
        private System.Windows.Forms.RichTextBox decodedBytes;
        private System.Windows.Forms.RichTextBox encodedBytes;
        private System.Windows.Forms.RichTextBox key;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}

