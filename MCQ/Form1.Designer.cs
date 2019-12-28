namespace MCQ
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
            this.fileOpenButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.sheetBtn = new System.Windows.Forms.Button();
            this.detectMcqBtn = new System.Windows.Forms.Button();
            this.sheetDetectImage = new Emgu.CV.UI.ImageBox();
            this.bubbleImage = new Emgu.CV.UI.ImageBox();
            this.orignalImage = new Emgu.CV.UI.ImageBox();
            this.bubbleDetectBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.sheetDetectImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bubbleImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.orignalImage)).BeginInit();
            this.SuspendLayout();
            // 
            // fileOpenButton
            // 
            this.fileOpenButton.Location = new System.Drawing.Point(36, 38);
            this.fileOpenButton.Name = "fileOpenButton";
            this.fileOpenButton.Size = new System.Drawing.Size(75, 23);
            this.fileOpenButton.TabIndex = 0;
            this.fileOpenButton.Text = "Open File";
            this.fileOpenButton.UseVisualStyleBackColor = true;
            this.fileOpenButton.Click += new System.EventHandler(this.Button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Location = new System.Drawing.Point(32, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "Student Information";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(801, 434);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Exit";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // sheetBtn
            // 
            this.sheetBtn.Location = new System.Drawing.Point(148, 38);
            this.sheetBtn.Name = "sheetBtn";
            this.sheetBtn.Size = new System.Drawing.Size(101, 23);
            this.sheetBtn.TabIndex = 5;
            this.sheetBtn.Text = "Detect Mcq Sheet";
            this.sheetBtn.UseVisualStyleBackColor = true;
            this.sheetBtn.Click += new System.EventHandler(this.Button3_Click);
            // 
            // detectMcqBtn
            // 
            this.detectMcqBtn.Location = new System.Drawing.Point(293, 38);
            this.detectMcqBtn.Name = "detectMcqBtn";
            this.detectMcqBtn.Size = new System.Drawing.Size(122, 23);
            this.detectMcqBtn.TabIndex = 6;
            this.detectMcqBtn.Text = "Dectect MCQS";
            this.detectMcqBtn.UseVisualStyleBackColor = true;
            this.detectMcqBtn.Click += new System.EventHandler(this.Button4_Click);
            // 
            // sheetDetectImage
            // 
            this.sheetDetectImage.Location = new System.Drawing.Point(330, 139);
            this.sheetDetectImage.Name = "sheetDetectImage";
            this.sheetDetectImage.Size = new System.Drawing.Size(217, 262);
            this.sheetDetectImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.sheetDetectImage.TabIndex = 2;
            this.sheetDetectImage.TabStop = false;
            // 
            // bubbleImage
            // 
            this.bubbleImage.Location = new System.Drawing.Point(612, 139);
            this.bubbleImage.Name = "bubbleImage";
            this.bubbleImage.Size = new System.Drawing.Size(252, 262);
            this.bubbleImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.bubbleImage.TabIndex = 2;
            this.bubbleImage.TabStop = false;
            // 
            // orignalImage
            // 
            this.orignalImage.Location = new System.Drawing.Point(36, 139);
            this.orignalImage.Name = "orignalImage";
            this.orignalImage.Size = new System.Drawing.Size(213, 262);
            this.orignalImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.orignalImage.TabIndex = 2;
            this.orignalImage.TabStop = false;
            // 
            // bubbleDetectBtn
            // 
            this.bubbleDetectBtn.Location = new System.Drawing.Point(470, 38);
            this.bubbleDetectBtn.Name = "bubbleDetectBtn";
            this.bubbleDetectBtn.Size = new System.Drawing.Size(91, 23);
            this.bubbleDetectBtn.TabIndex = 7;
            this.bubbleDetectBtn.Text = "Find Bubbles";
            this.bubbleDetectBtn.UseVisualStyleBackColor = true;
            this.bubbleDetectBtn.Click += new System.EventHandler(this.BubbleDetectBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(28)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(888, 480);
            this.Controls.Add(this.bubbleDetectBtn);
            this.Controls.Add(this.orignalImage);
            this.Controls.Add(this.bubbleImage);
            this.Controls.Add(this.sheetDetectImage);
            this.Controls.Add(this.detectMcqBtn);
            this.Controls.Add(this.sheetBtn);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.fileOpenButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.sheetDetectImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bubbleImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.orignalImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button fileOpenButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button sheetBtn;
        private System.Windows.Forms.Button detectMcqBtn;
        private Emgu.CV.UI.ImageBox sheetDetectImage;
        private Emgu.CV.UI.ImageBox bubbleImage;
        private Emgu.CV.UI.ImageBox orignalImage;
        private System.Windows.Forms.Button bubbleDetectBtn;
    }
}

