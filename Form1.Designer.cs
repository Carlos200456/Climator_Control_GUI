namespace Colimator_Control_GUI
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.buttonIrisOpen = new System.Windows.Forms.Button();
            this.buttonIrisClose = new System.Windows.Forms.Button();
            this.buttonVColOpen = new System.Windows.Forms.Button();
            this.buttonVColClose = new System.Windows.Forms.Button();
            this.buttonRotCW = new System.Windows.Forms.Button();
            this.buttonRotCCW = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonWriteM0 = new System.Windows.Forms.Button();
            this.buttonWriteM1 = new System.Windows.Forms.Button();
            this.buttonWriteM2 = new System.Windows.Forms.Button();
            this.buttonM0 = new System.Windows.Forms.Button();
            this.buttonM1 = new System.Windows.Forms.Button();
            this.buttonM2 = new System.Windows.Forms.Button();
            this.textBoxP1 = new System.Windows.Forms.TextBox();
            this.textBoxP2 = new System.Windows.Forms.TextBox();
            this.buttonCE = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // buttonIrisOpen
            // 
            this.buttonIrisOpen.Location = new System.Drawing.Point(12, 12);
            this.buttonIrisOpen.Name = "buttonIrisOpen";
            this.buttonIrisOpen.Size = new System.Drawing.Size(75, 75);
            this.buttonIrisOpen.TabIndex = 0;
            this.buttonIrisOpen.Text = "Iris Open";
            this.buttonIrisOpen.UseVisualStyleBackColor = true;
            this.buttonIrisOpen.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonIrisOpen_MouseDown);
            this.buttonIrisOpen.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonIris_MouseUp);
            // 
            // buttonIrisClose
            // 
            this.buttonIrisClose.Location = new System.Drawing.Point(12, 93);
            this.buttonIrisClose.Name = "buttonIrisClose";
            this.buttonIrisClose.Size = new System.Drawing.Size(75, 75);
            this.buttonIrisClose.TabIndex = 1;
            this.buttonIrisClose.Text = "Iris Close";
            this.buttonIrisClose.UseVisualStyleBackColor = true;
            this.buttonIrisClose.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonIrisClose_MouseDown);
            this.buttonIrisClose.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonIris_MouseUp);
            // 
            // buttonVColOpen
            // 
            this.buttonVColOpen.Location = new System.Drawing.Point(93, 12);
            this.buttonVColOpen.Name = "buttonVColOpen";
            this.buttonVColOpen.Size = new System.Drawing.Size(75, 75);
            this.buttonVColOpen.TabIndex = 2;
            this.buttonVColOpen.Text = "Coll Open";
            this.buttonVColOpen.UseVisualStyleBackColor = true;
            this.buttonVColOpen.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonVColsOpen_MouseDown);
            this.buttonVColOpen.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonVColOpen_MouseUp);
            // 
            // buttonVColClose
            // 
            this.buttonVColClose.Location = new System.Drawing.Point(93, 93);
            this.buttonVColClose.Name = "buttonVColClose";
            this.buttonVColClose.Size = new System.Drawing.Size(75, 75);
            this.buttonVColClose.TabIndex = 3;
            this.buttonVColClose.Text = "Coll Close";
            this.buttonVColClose.UseVisualStyleBackColor = true;
            this.buttonVColClose.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonVColsClose_MouseDown);
            this.buttonVColClose.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonVColClose_MouseUp);
            // 
            // buttonRotCW
            // 
            this.buttonRotCW.Location = new System.Drawing.Point(174, 12);
            this.buttonRotCW.Name = "buttonRotCW";
            this.buttonRotCW.Size = new System.Drawing.Size(75, 75);
            this.buttonRotCW.TabIndex = 4;
            this.buttonRotCW.Text = "Rot CW";
            this.buttonRotCW.UseVisualStyleBackColor = true;
            this.buttonRotCW.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonRotCW_MouseDown);
            this.buttonRotCW.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonRotCW_MouseUp);
            // 
            // buttonRotCCW
            // 
            this.buttonRotCCW.Location = new System.Drawing.Point(174, 93);
            this.buttonRotCCW.Name = "buttonRotCCW";
            this.buttonRotCCW.Size = new System.Drawing.Size(75, 75);
            this.buttonRotCCW.TabIndex = 5;
            this.buttonRotCCW.Text = "Rot CCW";
            this.buttonRotCCW.UseVisualStyleBackColor = true;
            this.buttonRotCCW.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonRotCCW_MouseDown);
            this.buttonRotCCW.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonRotCCW_MouseUp);
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(12, 191);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(75, 23);
            this.buttonReset.TabIndex = 6;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // buttonWriteM0
            // 
            this.buttonWriteM0.Location = new System.Drawing.Point(93, 191);
            this.buttonWriteM0.Name = "buttonWriteM0";
            this.buttonWriteM0.Size = new System.Drawing.Size(75, 23);
            this.buttonWriteM0.TabIndex = 7;
            this.buttonWriteM0.Text = "Write M0";
            this.buttonWriteM0.UseVisualStyleBackColor = true;
            this.buttonWriteM0.Click += new System.EventHandler(this.buttonWriteM0_Click);
            // 
            // buttonWriteM1
            // 
            this.buttonWriteM1.Location = new System.Drawing.Point(93, 220);
            this.buttonWriteM1.Name = "buttonWriteM1";
            this.buttonWriteM1.Size = new System.Drawing.Size(75, 23);
            this.buttonWriteM1.TabIndex = 8;
            this.buttonWriteM1.Text = "Write M1";
            this.buttonWriteM1.UseVisualStyleBackColor = true;
            this.buttonWriteM1.Click += new System.EventHandler(this.buttonWriteM1_Click);
            // 
            // buttonWriteM2
            // 
            this.buttonWriteM2.Location = new System.Drawing.Point(93, 249);
            this.buttonWriteM2.Name = "buttonWriteM2";
            this.buttonWriteM2.Size = new System.Drawing.Size(75, 23);
            this.buttonWriteM2.TabIndex = 9;
            this.buttonWriteM2.Text = "Write M2";
            this.buttonWriteM2.UseVisualStyleBackColor = true;
            this.buttonWriteM2.Click += new System.EventHandler(this.buttonWriteM2_Click);
            // 
            // buttonM0
            // 
            this.buttonM0.Location = new System.Drawing.Point(269, 12);
            this.buttonM0.Name = "buttonM0";
            this.buttonM0.Size = new System.Drawing.Size(75, 49);
            this.buttonM0.TabIndex = 10;
            this.buttonM0.Text = "Iris M0";
            this.buttonM0.UseVisualStyleBackColor = true;
            this.buttonM0.Click += new System.EventHandler(this.buttonM0_Click);
            // 
            // buttonM1
            // 
            this.buttonM1.Location = new System.Drawing.Point(269, 65);
            this.buttonM1.Name = "buttonM1";
            this.buttonM1.Size = new System.Drawing.Size(75, 49);
            this.buttonM1.TabIndex = 11;
            this.buttonM1.Text = "Iris M1";
            this.buttonM1.UseVisualStyleBackColor = true;
            this.buttonM1.Click += new System.EventHandler(this.buttonM1_Click);
            // 
            // buttonM2
            // 
            this.buttonM2.Location = new System.Drawing.Point(269, 118);
            this.buttonM2.Name = "buttonM2";
            this.buttonM2.Size = new System.Drawing.Size(75, 49);
            this.buttonM2.TabIndex = 12;
            this.buttonM2.Text = "Iris M2";
            this.buttonM2.UseVisualStyleBackColor = true;
            this.buttonM2.Click += new System.EventHandler(this.buttonM2_Click);
            // 
            // textBoxP1
            // 
            this.textBoxP1.Location = new System.Drawing.Point(185, 192);
            this.textBoxP1.Name = "textBoxP1";
            this.textBoxP1.Size = new System.Drawing.Size(45, 20);
            this.textBoxP1.TabIndex = 16;
            // 
            // textBoxP2
            // 
            this.textBoxP2.Location = new System.Drawing.Point(185, 221);
            this.textBoxP2.Name = "textBoxP2";
            this.textBoxP2.Size = new System.Drawing.Size(45, 20);
            this.textBoxP2.TabIndex = 17;
            // 
            // buttonCE
            // 
            this.buttonCE.Location = new System.Drawing.Point(12, 220);
            this.buttonCE.Name = "buttonCE";
            this.buttonCE.Size = new System.Drawing.Size(75, 35);
            this.buttonCE.TabIndex = 23;
            this.buttonCE.Text = "Clear EEPROM";
            this.buttonCE.UseVisualStyleBackColor = true;
            this.buttonCE.Click += new System.EventHandler(this.buttonCE_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(236, 191);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 24;
            this.button1.Text = "Hi Speed";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(236, 220);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 25;
            this.button2.Text = "Low Speed";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 282);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonCE);
            this.Controls.Add(this.textBoxP2);
            this.Controls.Add(this.textBoxP1);
            this.Controls.Add(this.buttonM2);
            this.Controls.Add(this.buttonM1);
            this.Controls.Add(this.buttonM0);
            this.Controls.Add(this.buttonWriteM2);
            this.Controls.Add(this.buttonWriteM1);
            this.Controls.Add(this.buttonWriteM0);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.buttonRotCCW);
            this.Controls.Add(this.buttonRotCW);
            this.Controls.Add(this.buttonVColClose);
            this.Controls.Add(this.buttonVColOpen);
            this.Controls.Add(this.buttonIrisClose);
            this.Controls.Add(this.buttonIrisOpen);
            this.Name = "Form1";
            this.Text = "Col_Control";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Button buttonIrisOpen;
        private System.Windows.Forms.Button buttonIrisClose;
        private System.Windows.Forms.Button buttonVColOpen;
        private System.Windows.Forms.Button buttonVColClose;
        private System.Windows.Forms.Button buttonRotCW;
        private System.Windows.Forms.Button buttonRotCCW;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Button buttonWriteM0;
        private System.Windows.Forms.Button buttonWriteM1;
        private System.Windows.Forms.Button buttonWriteM2;
        private System.Windows.Forms.Button buttonM0;
        private System.Windows.Forms.Button buttonM1;
        private System.Windows.Forms.Button buttonM2;
        private System.Windows.Forms.TextBox textBoxP1;
        private System.Windows.Forms.TextBox textBoxP2;
        private System.Windows.Forms.Button buttonCE;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

