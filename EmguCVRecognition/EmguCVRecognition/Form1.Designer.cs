namespace EmguCVRecognition
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.imageBox1 = new Emgu.CV.UI.ImageBox();
            this.imageBox2 = new Emgu.CV.UI.ImageBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.label8 = new System.Windows.Forms.Label();
            this.trackBar3 = new System.Windows.Forms.TrackBar();
            this.label9 = new System.Windows.Forms.Label();
            this.trackBar4 = new System.Windows.Forms.TrackBar();
            this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBox2 = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBox3 = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBox4 = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.trackBar5 = new System.Windows.Forms.TrackBar();
            this.maskedTextBox5 = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBox6 = new System.Windows.Forms.MaskedTextBox();
            this.trackBar6 = new System.Windows.Forms.TrackBar();
            this.label11 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox2)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar6)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(126, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "load images";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // imageBox1
            // 
            this.imageBox1.Location = new System.Drawing.Point(580, 12);
            this.imageBox1.Name = "imageBox1";
            this.imageBox1.Size = new System.Drawing.Size(480, 272);
            this.imageBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imageBox1.TabIndex = 2;
            this.imageBox1.TabStop = false;
            this.imageBox1.Click += new System.EventHandler(this.imageBox1_Click);
            // 
            // imageBox2
            // 
            this.imageBox2.Location = new System.Drawing.Point(580, 340);
            this.imageBox2.Name = "imageBox2";
            this.imageBox2.Size = new System.Drawing.Size(480, 272);
            this.imageBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imageBox2.TabIndex = 2;
            this.imageBox2.TabStop = false;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 48);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(126, 199);
            this.listBox1.TabIndex = 3;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 282);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(126, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "track shapes";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "no shapes chosen";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(12, 311);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(126, 23);
            this.button4.TabIndex = 7;
            this.button4.Text = "show paths";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "no shapes tracked";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 253);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(126, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "clear chosen shapes";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(12, 340);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(124, 17);
            this.checkBox1.TabIndex = 10;
            this.checkBox1.Text = "subtract background";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(147, 48);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(203, 564);
            this.panel1.TabIndex = 12;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(371, 48);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(203, 564);
            this.panel2.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(151, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "chosen shapes";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(375, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "tracked shapes";
            // 
            // trackBar1
            // 
            this.trackBar1.AutoSize = false;
            this.trackBar1.LargeChange = 2;
            this.trackBar1.Location = new System.Drawing.Point(12, 389);
            this.trackBar1.Maximum = 7;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(126, 20);
            this.trackBar1.TabIndex = 16;
            this.trackBar1.Tag = "";
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 373);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "filter kernel size";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 421);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "hue tolerance";
            // 
            // trackBar2
            // 
            this.trackBar2.AutoSize = false;
            this.trackBar2.LargeChange = 20;
            this.trackBar2.Location = new System.Drawing.Point(12, 437);
            this.trackBar2.Maximum = 90;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(126, 20);
            this.trackBar2.TabIndex = 18;
            this.trackBar2.Tag = "";
            this.trackBar2.TickFrequency = 10;
            this.trackBar2.Value = 10;
            this.trackBar2.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 467);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "sat tolerance";
            // 
            // trackBar3
            // 
            this.trackBar3.AutoSize = false;
            this.trackBar3.LargeChange = 50;
            this.trackBar3.Location = new System.Drawing.Point(12, 483);
            this.trackBar3.Maximum = 255;
            this.trackBar3.Name = "trackBar3";
            this.trackBar3.Size = new System.Drawing.Size(126, 20);
            this.trackBar3.TabIndex = 20;
            this.trackBar3.Tag = "";
            this.trackBar3.TickFrequency = 20;
            this.trackBar3.Value = 20;
            this.trackBar3.Scroll += new System.EventHandler(this.trackBar3_Scroll);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 517);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 13);
            this.label9.TabIndex = 23;
            this.label9.Text = "val tolerance";
            // 
            // trackBar4
            // 
            this.trackBar4.AutoSize = false;
            this.trackBar4.LargeChange = 50;
            this.trackBar4.Location = new System.Drawing.Point(12, 533);
            this.trackBar4.Maximum = 255;
            this.trackBar4.Name = "trackBar4";
            this.trackBar4.Size = new System.Drawing.Size(126, 20);
            this.trackBar4.TabIndex = 22;
            this.trackBar4.Tag = "";
            this.trackBar4.TickFrequency = 20;
            this.trackBar4.Value = 20;
            this.trackBar4.Scroll += new System.EventHandler(this.trackBar4_Scroll);
            // 
            // maskedTextBox1
            // 
            this.maskedTextBox1.Location = new System.Drawing.Point(97, 366);
            this.maskedTextBox1.Mask = "##";
            this.maskedTextBox1.Name = "maskedTextBox1";
            this.maskedTextBox1.PromptChar = ' ';
            this.maskedTextBox1.Size = new System.Drawing.Size(39, 20);
            this.maskedTextBox1.TabIndex = 28;
            this.maskedTextBox1.Text = "1";
            this.maskedTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.maskedTextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.maskedTextBox1_KeyDown);
            // 
            // maskedTextBox2
            // 
            this.maskedTextBox2.Location = new System.Drawing.Point(97, 414);
            this.maskedTextBox2.Mask = "##";
            this.maskedTextBox2.Name = "maskedTextBox2";
            this.maskedTextBox2.PromptChar = ' ';
            this.maskedTextBox2.Size = new System.Drawing.Size(39, 20);
            this.maskedTextBox2.TabIndex = 29;
            this.maskedTextBox2.Text = "10";
            this.maskedTextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.maskedTextBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.maskedTextBox2_KeyDown);
            // 
            // maskedTextBox3
            // 
            this.maskedTextBox3.Location = new System.Drawing.Point(97, 460);
            this.maskedTextBox3.Mask = "###";
            this.maskedTextBox3.Name = "maskedTextBox3";
            this.maskedTextBox3.PromptChar = ' ';
            this.maskedTextBox3.Size = new System.Drawing.Size(39, 20);
            this.maskedTextBox3.TabIndex = 30;
            this.maskedTextBox3.Text = "20";
            this.maskedTextBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.maskedTextBox3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.maskedTextBox3_KeyDown);
            // 
            // maskedTextBox4
            // 
            this.maskedTextBox4.Location = new System.Drawing.Point(97, 510);
            this.maskedTextBox4.Mask = "###";
            this.maskedTextBox4.Name = "maskedTextBox4";
            this.maskedTextBox4.PromptChar = ' ';
            this.maskedTextBox4.Size = new System.Drawing.Size(39, 20);
            this.maskedTextBox4.TabIndex = 31;
            this.maskedTextBox4.Text = "20";
            this.maskedTextBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.maskedTextBox4.KeyDown += new System.Windows.Forms.KeyEventHandler(this.maskedTextBox4_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(580, 292);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "position";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 573);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(51, 13);
            this.label10.TabIndex = 33;
            this.label10.Text = "Dilatation";
            // 
            // trackBar5
            // 
            this.trackBar5.AutoSize = false;
            this.trackBar5.LargeChange = 10;
            this.trackBar5.Location = new System.Drawing.Point(12, 589);
            this.trackBar5.Maximum = 40;
            this.trackBar5.Name = "trackBar5";
            this.trackBar5.Size = new System.Drawing.Size(126, 20);
            this.trackBar5.TabIndex = 34;
            this.trackBar5.Tag = "";
            this.trackBar5.TickFrequency = 10;
            this.trackBar5.Value = 10;
            this.trackBar5.Scroll += new System.EventHandler(this.trackBar5_Scroll);
            // 
            // maskedTextBox5
            // 
            this.maskedTextBox5.Location = new System.Drawing.Point(102, 570);
            this.maskedTextBox5.Mask = "##";
            this.maskedTextBox5.Name = "maskedTextBox5";
            this.maskedTextBox5.PromptChar = ' ';
            this.maskedTextBox5.Size = new System.Drawing.Size(39, 20);
            this.maskedTextBox5.TabIndex = 35;
            this.maskedTextBox5.Text = "10";
            this.maskedTextBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.maskedTextBox5.KeyDown += new System.Windows.Forms.KeyEventHandler(this.maskedTextBox5_KeyDown);
            // 
            // maskedTextBox6
            // 
            this.maskedTextBox6.Location = new System.Drawing.Point(105, 620);
            this.maskedTextBox6.Mask = "##";
            this.maskedTextBox6.Name = "maskedTextBox6";
            this.maskedTextBox6.PromptChar = ' ';
            this.maskedTextBox6.Size = new System.Drawing.Size(39, 20);
            this.maskedTextBox6.TabIndex = 38;
            this.maskedTextBox6.Text = "70";
            this.maskedTextBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.maskedTextBox6.KeyDown += new System.Windows.Forms.KeyEventHandler(this.maskedTextBox6_KeyDown);
            // 
            // trackBar6
            // 
            this.trackBar6.AutoSize = false;
            this.trackBar6.LargeChange = 10;
            this.trackBar6.Location = new System.Drawing.Point(15, 639);
            this.trackBar6.Maximum = 99;
            this.trackBar6.Name = "trackBar6";
            this.trackBar6.Size = new System.Drawing.Size(126, 20);
            this.trackBar6.TabIndex = 37;
            this.trackBar6.Tag = "";
            this.trackBar6.TickFrequency = 10;
            this.trackBar6.Value = 70;
            this.trackBar6.Scroll += new System.EventHandler(this.trackBar6_Scroll);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(15, 623);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(88, 13);
            this.label11.TabIndex = 36;
            this.label11.Text = "Background ratio";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1129, 714);
            this.Controls.Add(this.maskedTextBox6);
            this.Controls.Add(this.trackBar6);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.maskedTextBox5);
            this.Controls.Add(this.trackBar5);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.maskedTextBox4);
            this.Controls.Add(this.maskedTextBox3);
            this.Controls.Add(this.maskedTextBox2);
            this.Controls.Add(this.maskedTextBox1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.trackBar4);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.trackBar3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.trackBar2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.imageBox2);
            this.Controls.Add(this.imageBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Shape Tracking";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar6)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private Emgu.CV.UI.ImageBox imageBox1;
        private Emgu.CV.UI.ImageBox imageBox2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TrackBar trackBar3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TrackBar trackBar4;
        private System.Windows.Forms.MaskedTextBox maskedTextBox1;
        private System.Windows.Forms.MaskedTextBox maskedTextBox2;
        private System.Windows.Forms.MaskedTextBox maskedTextBox3;
        private System.Windows.Forms.MaskedTextBox maskedTextBox4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TrackBar trackBar5;
        private System.Windows.Forms.MaskedTextBox maskedTextBox5;
        private System.Windows.Forms.MaskedTextBox maskedTextBox6;
        private System.Windows.Forms.TrackBar trackBar6;
        private System.Windows.Forms.Label label11;
    }
}

