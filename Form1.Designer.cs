using System;
using System.Globalization;
using System.Runtime.CompilerServices;
namespace Await_Async {
    partial class Form1 {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose (bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose ();
            }
            base.Dispose (disposing);
        }

        #region Windows Form Designer generated code
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ProgressBar progressBar1;
        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent () {

            this.button1 = new System.Windows.Forms.Button ();
            this.button2 = new System.Windows.Forms.Button ();
            this.button3 = new System.Windows.Forms.Button ();
            this.button4 = new System.Windows.Forms.Button ();
            this.button5 = new System.Windows.Forms.Button ();
            this.progressBar1 = new System.Windows.Forms.ProgressBar ();
            this.textBox1 = new System.Windows.Forms.TextBox ();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point (0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size (1000, 30);
            this.button1.TabIndex = 0;
            this.button1.Text = "Sencron";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.button1.Click += new System.EventHandler (this.button1_Click);
            //
            //button2
            //
            this.button2.Location = new System.Drawing.Point (0, 30);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size (1000, 30);
            this.button2.TabIndex = 0;
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.button2.Text = "Asencron";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler (this.button2_Click);
            //
            //button3
            //
            this.button3.Location = new System.Drawing.Point (0, 60);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size (1000, 30);
            this.button3.TabIndex = 0;
            this.button3.Text = "Paralel";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler (this.button3_Click);
            //
            //button4
            //
            this.button4.Location = new System.Drawing.Point (0, 90);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size (1000, 30);
            this.button4.TabIndex = 0;
            this.button4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.button4.Text = "Cancel Operation";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler (this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point (0, 120);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size (1000, 30);
            this.button5.TabIndex = 0;
            this.button5.Text = "Paralel Sencron";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.button5.Click += new System.EventHandler (this.button5_Click);
            //
            //textBox
            //
            this.textBox1.Location = new System.Drawing.Point (0, 180);
            this.textBox1.Size = new System.Drawing.Size (1000, 600);
            this.textBox1.Multiline = true;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point (0, 150);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size (1000, 30);
            this.progressBar1.TabIndex = 0;

            this.components = new System.ComponentModel.Container ();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size (800, 450);
            this.Text = "Form1";
            this.Controls.Add (button1);
            this.Controls.Add (button2);
            this.Controls.Add (button3);
            this.Controls.Add (button4);
            this.Controls.Add (button5);
            this.Controls.Add (progressBar1);
            this.Controls.Add (textBox1);
        }

        #endregion
    }
}