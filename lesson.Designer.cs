namespace Prototype
{
    partial class Lesson
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
            this.labelLText = new System.Windows.Forms.Label();
            this.buttonLRun = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.buttonlStep = new System.Windows.Forms.Button();
            this.buttonlReset = new System.Windows.Forms.Button();
            this.labelLTitle = new System.Windows.Forms.Label();
            this.labelLProg = new System.Windows.Forms.Label();
            this.buttonLprev = new System.Windows.Forms.Button();
            this.buttonLnext = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelLText
            // 
            this.labelLText.Location = new System.Drawing.Point(3, 66);
            this.labelLText.Name = "labelLText";
            this.labelLText.Size = new System.Drawing.Size(727, 368);
            this.labelLText.TabIndex = 68;
            this.labelLText.Text = "Lesson Text";
            // 
            // buttonLRun
            // 
            this.buttonLRun.Location = new System.Drawing.Point(22, 723);
            this.buttonLRun.Name = "buttonLRun";
            this.buttonLRun.Size = new System.Drawing.Size(113, 40);
            this.buttonLRun.TabIndex = 71;
            this.buttonLRun.Text = "Run";
            this.buttonLRun.UseVisualStyleBackColor = true;
            this.buttonLRun.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(969, 38);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(113, 40);
            this.button2.TabIndex = 72;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // buttonlStep
            // 
            this.buttonlStep.Location = new System.Drawing.Point(304, 723);
            this.buttonlStep.Name = "buttonlStep";
            this.buttonlStep.Size = new System.Drawing.Size(113, 40);
            this.buttonlStep.TabIndex = 74;
            this.buttonlStep.Text = "Step";
            this.buttonlStep.UseVisualStyleBackColor = true;
            this.buttonlStep.Click += new System.EventHandler(this.buttonlStep_Click);
            // 
            // buttonlReset
            // 
            this.buttonlReset.Location = new System.Drawing.Point(583, 723);
            this.buttonlReset.Name = "buttonlReset";
            this.buttonlReset.Size = new System.Drawing.Size(113, 40);
            this.buttonlReset.TabIndex = 75;
            this.buttonlReset.Text = "Reset";
            this.buttonlReset.UseVisualStyleBackColor = true;
            this.buttonlReset.Click += new System.EventHandler(this.buttonlReset_Click);
            // 
            // labelLTitle
            // 
            this.labelLTitle.AutoSize = true;
            this.labelLTitle.Location = new System.Drawing.Point(3, 18);
            this.labelLTitle.Name = "labelLTitle";
            this.labelLTitle.Size = new System.Drawing.Size(53, 25);
            this.labelLTitle.TabIndex = 76;
            this.labelLTitle.Text = "Title";
            // 
            // labelLProg
            // 
            this.labelLProg.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelLProg.AutoSize = true;
            this.labelLProg.Location = new System.Drawing.Point(287, 455);
            this.labelLProg.Name = "labelLProg";
            this.labelLProg.Size = new System.Drawing.Size(70, 25);
            this.labelLProg.TabIndex = 69;
            this.labelLProg.Text = "label2";
            // 
            // buttonLprev
            // 
            this.buttonLprev.Location = new System.Drawing.Point(510, 18);
            this.buttonLprev.Name = "buttonLprev";
            this.buttonLprev.Size = new System.Drawing.Size(84, 45);
            this.buttonLprev.TabIndex = 77;
            this.buttonLprev.Text = "Prev";
            this.buttonLprev.UseVisualStyleBackColor = true;
            this.buttonLprev.Click += new System.EventHandler(this.buttonLprev_Click);
            // 
            // buttonLnext
            // 
            this.buttonLnext.Location = new System.Drawing.Point(635, 18);
            this.buttonLnext.Name = "buttonLnext";
            this.buttonLnext.Size = new System.Drawing.Size(84, 45);
            this.buttonLnext.TabIndex = 78;
            this.buttonLnext.Text = "Next";
            this.buttonLnext.UseVisualStyleBackColor = true;
            this.buttonLnext.Click += new System.EventHandler(this.buttonLnext_Click);
            // 
            // Lesson
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 789);
            this.Controls.Add(this.buttonLnext);
            this.Controls.Add(this.buttonLprev);
            this.Controls.Add(this.labelLTitle);
            this.Controls.Add(this.buttonlReset);
            this.Controls.Add(this.buttonlStep);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.buttonLRun);
            this.Controls.Add(this.labelLProg);
            this.Controls.Add(this.labelLText);
            this.Name = "Lesson";
            this.Text = "lesson";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelLText;
        private System.Windows.Forms.Button buttonLRun;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button buttonlStep;
        private System.Windows.Forms.Button buttonlReset;
        private System.Windows.Forms.Label labelLTitle;
        private System.Windows.Forms.Label labelLProg;
        private System.Windows.Forms.Button buttonLprev;
        private System.Windows.Forms.Button buttonLnext;
    }
}