using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Prototype
{
    public partial class Lesson : Form
    {
        int lessonNumber = 1;
       
        string lesson = Prototype.Properties.Resources.Lessons;
        string[] lessons;
        string[] lessonTitle = new string[8];
        string[] lessonText = new string[8];
        string[] lessonProg = new string[8];

        private main mainForm;
        public Lesson(main mf, int lessonNum)
        {
            InitializeComponent();
            lessons = lesson.Split('%');
            int c = 0;
            for(int i = 0; i < lessons.Length-3; i = i+3)
            {
                
                    lessonTitle[c] = lessons[i];
                    lessonText[c] = lessons[i + 1];
                    lessonProg[c] = lessons[i + 2];
               
                
                c++;
            }

            this.mainForm = mf;
            lessonNumber = lessonNum; //Setting lesson number from main form
            labelLTitle.Text = lessonTitle[lessonNumber];
            labelLProg.Text = lessonProg[lessonNumber];
            labelLText.Text = lessonText[lessonNumber];

        }

        //************************************* 
        // Running the lesson program
        //************************************
        private void buttonRun_Click(object sender, EventArgs e)
        {
           buttonLRun.Enabled = false;
           buttonlReset.Enabled = false;
           buttonlStep.Enabled = false;
            mainForm.progSet = true;
            mainForm.prog = labelLProg.Text.Split(';');
            mainForm.button1_Click(sender, e);
           buttonLRun.Enabled = true;
           buttonlReset.Enabled = true;
           buttonlStep.Enabled = true;
        }

        private void buttonlStep_Click(object sender, EventArgs e)
        {

            buttonLRun.Enabled = false;
            buttonlReset.Enabled = false;
            buttonlStep.Enabled = false;
            mainForm.progSet = true;
            mainForm.prog = labelLProg.Text.Split(';');
            mainForm.buttonStep_Click(sender, e);
            buttonLRun.Enabled = true;
            buttonlReset.Enabled = true;
            buttonlStep.Enabled = true;

        }

        private void buttonlReset_Click(object sender, EventArgs e)
        {
            buttonLRun.Enabled = false;
            buttonlReset.Enabled = false;
            buttonlStep.Enabled = false;
            mainForm.buttonReset_Click(sender, e);
            buttonLRun.Enabled = true;
            buttonlReset.Enabled = true;
            buttonlStep.Enabled = true;
        }


        //************************************* 
        // Enabling and disabling buttons on focus change
        //************************************
        private void lesson_Activate(object sender, EventArgs e)
        {
            buttonLRun.Enabled = true;
            buttonlReset.Enabled = true;
            buttonlStep.Enabled = true;
            
        }

        private void lesson_Deactivate(object sender, EventArgs e)
        {
            buttonLRun.Enabled = false;
            buttonlReset.Enabled = false;
            buttonlStep.Enabled = false;
           
        }

        private void buttonLnext_Click(object sender, EventArgs e)
        {
            lessonNumber = lessonNumber+1;
            if (lessonNumber < 8)
            {
                labelLTitle.Text = lessonTitle[lessonNumber];
                labelLProg.Text = lessonProg[lessonNumber];
                labelLText.Text = lessonText[lessonNumber];
            }
            else
            {
                lessonNumber = 0;
                labelLTitle.Text = lessonTitle[lessonNumber];
                labelLProg.Text = lessonProg[lessonNumber];
                labelLText.Text = lessonText[lessonNumber];
            }
        }

        private void buttonLprev_Click(object sender, EventArgs e)
        {
            if (lessonNumber-- > 0)
            {
                lessonNumber = lessonNumber--;
                labelLTitle.Text = lessonTitle[lessonNumber];
                labelLProg.Text = lessonProg[lessonNumber];
                labelLText.Text = lessonText[lessonNumber];
            }
            else
            {
                lessonNumber = 7;
                labelLTitle.Text = lessonTitle[lessonNumber];
                labelLProg.Text = lessonProg[lessonNumber];
                labelLText.Text = lessonText[lessonNumber];
            }
        }
    }
}
