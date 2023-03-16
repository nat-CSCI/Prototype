using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prototype
{
    public partial class Lesson : Form
    {

        private main mainForm;
        public Lesson(main mf)
        {
            InitializeComponent();
            this.mainForm = mf;
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            string less = Prototype.Properties.Resources.Lesson1;

           mainForm.textBox1.Text = less.Split(':')[1];
           mainForm.button1_Click(sender, e);
        }
    }
}
