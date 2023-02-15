using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;


namespace Prototype
{
    /// <summary>
    // is this working?
    /// </summary>
    public partial class main : Form
    {    
        
        int activeRow = 6;
        int activeColumn = 0;
        int direction = 1;
        bool[,] blocked = new bool[7,7];
        string[] string_loop = new string[100];
        

        box[,] grid = new box[7, 7];

        Image arrow = Image.FromFile("C:\\Users\\natdy\\OneDrive\\Desktop\\Prototype\\arrow.png");
        Image blank = Image.FromFile("C:\\Users\\natdy\\OneDrive\\Desktop\\Prototype\\white.png");
        Image block = Image.FromFile("C:\\Users\\natdy\\OneDrive\\Desktop\\Prototype\\blocked.png");
        Image paint = Image.FromFile("C:\\Users\\natdy\\OneDrive\\Desktop\\Prototype\\black.png");
        Image black = Image.FromFile("C:\\Users\\natdy\\OneDrive\\Desktop\\Prototype\\painted.png");

        public main()
        {
            InitializeComponent();
        }

        private void main_Load(object sender, EventArgs e)
        {
           
            int i = 6;
            int j = 6;
            
            foreach (PictureBox pb in tableLayoutPanel1.Controls)
            {

                grid[i, j] = new box(pb, i, j);
                    
                
                j--;

                if(i < 0)
                {
                    i = 6;
                }
                if(j < 0)
                {
                    j = 6;
                    i--;
                }


            }

            grid[6,0].setImage(arrow);
        }

        



        private void textBox1_DragEnter(object sender, DragEventArgs e)
        {
            label1.Text = "enter";

            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void textBox1_DragDrop(object sender,
        System.Windows.Forms.DragEventArgs e)
        {
            int i;
            String s;

            // Get start position to drop the text.  
            i = textBox1.SelectionStart;
            s = textBox1.Text.Substring(i);
            textBox1.Text = textBox1.Text.Substring(0, i);

            // Drop the text on to the RichTextBox.  
            textBox1.Text = textBox1.Text +
               e.Data.GetData(DataFormats.Text).ToString();
            textBox1.Text = s + textBox1.Text ;
         
        }



        private void pictureBox_block(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            if (pb.Name != "pictureBoxTurnLeft" || pb.Name != "pictureBoxMove")
            {
                int r = tableLayoutPanel1.GetRow(pb);
                int c = tableLayoutPanel1.GetColumn(pb);
                if (blocked[r, c])
                {
                    pb.Image = blank;
                    blocked[r, c] = false; 
                }
                else {
                    pb.Image = block;
                    blocked[r, c] = true;
                }
             
            }
            
        }


        //***************************************
        /*  
         Dragging functions for picture boxes
         */
        private void pictureBoxMove_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            label1.Text = "down";
            pictureBoxMove.DoDragDrop("Move\r\n", DragDropEffects.All);
        }

        private void pictureBoxTurnLeft_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            label1.Text = "down";
            pictureBoxMove.DoDragDrop("TurnLeft\r\n", DragDropEffects.All);
        }

        private void pictureBoxPaint_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            label1.Text = "down";
            pictureBoxMove.DoDragDrop("Paint\r\n", DragDropEffects.All);
        }

        private void pictureBoxErase_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            label1.Text = "down";
            pictureBoxMove.DoDragDrop("Erase\r\n", DragDropEffects.All);
        }


        //*******************************

        private async void button1_Click(object sender, EventArgs e)
        {
            String s = textBox1.Text.ToString();
            String[] prog = s.Split('\n');
            //bool loop = false;
            //int place = 0;
            //bool condition;
            foreach (string progItem in prog)
            {
                label1.Text = progItem;
                
                //if(progItem == "while\r")
                //{ 
                //}
                //else if(progItem == "{\r")
                //{
                //    loop = true;
                //}
                //else if (loop)
                //{
                //    string_loop[place] = progItem;
                //    place++;
                //}
                //else if(progItem == "}\r")
                //{
                //    loop = false;
                //}
               if (progItem == "Move\r") 
                {
                    move();
                }
                else if (progItem == "TurnLeft\r")
                {
                    turn();
                }
                else if (progItem == "Paint\r")
                {
                    if (!grid[activeRow, activeColumn].getPaint())
                    {
                        grid[activeRow, activeColumn].setImage(paint);
                        grid[activeRow, activeColumn].setPaint();
                    }

                }
                else if(progItem == "Erase\r")
                {
                    grid[activeRow, activeColumn].setImage(arrow);
                    grid[activeRow, activeColumn].setPaint();
                }

                await Task.Delay(1000);
           
            }
        }

        private void buttonLeft_Click(object sender, EventArgs e)
        {
            turn();
        }

       

        private void buttonMove_Click(object sender, EventArgs e)
        {
            move();
        }

       
        //Movement functions

        public void turn()
        {
            if (grid[activeRow, activeColumn].getPaint())
            {
                arrow.RotateFlip(RotateFlipType.Rotate270FlipNone);
                paint.RotateFlip(RotateFlipType.Rotate270FlipNone);
                grid[activeRow, activeColumn].setImage(paint);

            }
            else
            {
                arrow.RotateFlip(RotateFlipType.Rotate270FlipNone);
                paint.RotateFlip(RotateFlipType.Rotate270FlipNone);
                grid[activeRow, activeColumn].setImage(arrow);
            }
            if (direction + 1 <= 4)
            {
                direction++;
            }
            else
            {
                direction = 1;
            }

        }
        public void move() {
          
                if (direction == 1 && activeColumn + 1 <= 6 && !blocked[activeRow, activeColumn + 1])
                {

                     if (grid[activeRow, activeColumn].getImage() == paint)
                     {
                       grid[activeRow, activeColumn].setImage(black);
                     }
                      else
                     {
                        grid[activeRow, activeColumn].setImage(blank);
                }


                    if (grid[activeRow, activeColumn + 1].getImage() == black)
                    {
                        grid[activeRow, activeColumn + 1].setImage(paint);
                }
                    else { 
                        grid[activeRow, activeColumn + 1].setImage(arrow);
                }

                    activeColumn++;

                }
                else if (direction == 3 && activeColumn - 1 >= 0 && !blocked[activeRow, activeColumn - 1])
                {
                     if (grid[activeRow, activeColumn].getImage() == paint)
                     {
                        grid[activeRow, activeColumn].setImage(black);
                     }
                     else
                     {
                        grid[activeRow, activeColumn].setImage(blank);
                }

                    if (grid[activeRow, activeColumn - 1].getImage() == black)
                    {
                        grid[activeRow, activeColumn - 1].setImage(paint);
                }
                    else
                    {
                        grid[activeRow, activeColumn - 1].setImage(arrow);
                }

                    activeColumn--;
                }
                else if (direction == 2 && activeRow - 1 >= 0 && !blocked[activeRow - 1, activeColumn])
                {
                     if (grid[activeRow, activeColumn].getImage() == paint)
                     {
                        grid[activeRow, activeColumn].setImage(black);
                }
                     else
                     {
                        grid[activeRow, activeColumn].setImage(blank);
                }

                if (grid[activeRow-1, activeColumn].getImage() == black)
                {
                    grid[activeRow-1, activeColumn].setImage(paint);
                }
                else
                {
                    grid[activeRow-1, activeColumn].setImage(arrow);
                }

                    activeRow--;

                }
                else if (direction == 4 && activeRow + 1 <= 6 && !blocked[activeRow + 1, activeColumn])
                {
                    if (grid[activeRow, activeColumn].getImage() == paint)
                    {
                        grid[activeRow, activeColumn].setImage(black);
                }
                    else
                    {
                        grid[activeRow, activeColumn].setImage(blank);
                }

                    if (grid[activeRow + 1, activeColumn].getImage() == black)
                    {
                        grid[activeRow + 1, activeColumn].setImage(paint);
                }
                    else
                    {
                        grid[activeRow + 1, activeColumn].setImage(arrow);
                }

                activeRow++;
                }
            
        }

       
    }
}
