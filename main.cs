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
           
            textBox1.Text = textBox1.Text.Insert(textBox1.SelectionStart, e.Data.GetData(DataFormats.Text).ToString())+"\r\n";
            textBox1.Select(textBox1.Text.Length, 0);

        }



        private void pictureBox_block(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            if (pb.Name != "pictureBoxTurnLeft" || pb.Name != "pictureBoxMove")
            {
                int r = tableLayoutPanel1.GetRow(pb);
                int c = tableLayoutPanel1.GetColumn(pb);
                if (grid[r,c].getBlocked())
                {
                    pb.Image = blank;
                    grid[r,c].setBlocked(); 
                }
                else {
                    pb.Image = block;
                    grid[r, c].setBlocked();
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

        private void pictureBoxWhile_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            label1.Text = "while(){\r\n }\r";
            pictureBoxMove.DoDragDrop("while(){\r\n\r\n}\r\n", DragDropEffects.All);
        }


        //*******************************

        private async void button1_Click(object sender, EventArgs e)
        {
            String s = textBox1.Text.ToString();
            String[] prog = s.Split('\n');
            bool ifTrue = false;
            bool whileLoop = false;
            bool getActions = false;                //Signifies if getting the contents of the loop or not
            string condition = "";                  //Condition that needs to be met for loop
            string[] x;                             //While loop string
            string[] loopActions = new string[100]; //actions performed in loop
            int numActions = 0;                     //Numbers of actions in a loop
            int place = 0;                          //Place in loop
            bool forLoop = false;
            bool greaterthan = false;              //Show whether for loop is greater than or less than
            int startValue = 0;                    //Starting value for a for loop
            foreach (string progItem in prog)
            {
                label1.Text = progItem;
                
                if (progItem.Contains("while")) //While loop
                {
                    whileLoop = true;
                    getActions = true;
                    x = progItem.Split('(');
                    
                    condition = x[1];
                
                }
                else if (progItem.Contains("if"))
                {
                    ifTrue = true;
                    x = progItem.Split('(');
                    condition = x[1];
                    getActions = true;
                }
                else if (progItem.Contains("for"))
                {
                    forLoop = true;
                    getActions = true;
                    x = progItem.Split(';');
                    condition = x[1];
                    string y = x[0];
                    startValue = int.Parse(x[0].Split('=')[1]);
                    if (progItem.Contains('>'))
                    {
                        greaterthan = true;
                    }
                }
                else if (getActions) //While loop
                {
                    if(progItem != "}\r")
                    {
                        loopActions[place] = progItem;
                        place++;
                        numActions++;
                    }
                    else
                    {
                        label1.Text = "finsihed";
                        getActions = false;
                        place = 0;
                    }
                }
                else if (whileLoop) //While loop
                {
                    if(condition.Contains("blocked"))
                    {
                        while (moveIsValid())
                        {
                            for (int i = 0; i < numActions; i++)
                            {
                                    checkAction(loopActions[i]);
                                    await Task.Delay(1000);
                                
                            }
                            
                        }

                    }
                }
                else if (ifTrue)
                {
                    if (condition.Contains("blocked"))
                    {
                        if (moveIsValid())
                        {
                            for (int i = 0; i < numActions; i++)
                            {
                                checkAction(loopActions[i]);
                                await Task.Delay(1000);

                            }

                        }

                    }
                    if (condition.Contains("painted"))
                    {
                        if (grid[activeRow, activeColumn].getPaint())
                        {
                            for (int i = 0; i < numActions; i++)
                            {
                                checkAction(loopActions[i]);
                                await Task.Delay(1000);

                            }

                        }

                    }
                }
                else if (forLoop)
                {
                    //int i = int.Parse(condition.Split('<')[1]);
                    //int j = startValue;
                    for (int i = startValue; i < int.Parse(condition.Split('<')[1]); i++)
                    {
                        for (int j = 0; j < numActions; j++)
                        {
                            checkAction(loopActions[j]);
                            await Task.Delay(1000);
                        }

                    }
                }
                else
                {
                    checkAction(progItem);
                    await Task.Delay(1000);
                }
                

               
           
            }
        }

        private int nextRow()
        {
            if (direction == 2)
            {
                return activeRow - 1;
            }
            else if (direction == 4)
            {
                return activeRow + 1;
            }
            else
            {
                return activeRow;
            }
        }

        private int nextCol()
        {
            if (direction == 1)
            {
                return activeColumn+1;
            }
            else if (direction == 3)
            {
                return activeColumn - 1;
            }
            else
            {
                return activeColumn;
            }
        }

        private bool moveIsValid()
        {
            int r = nextRow();
            int c = nextCol();
            if((r <= 6 && r >=0) && (c <= 6 && c >= 0)) {
                
                if (grid[r, c].getBlocked())
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
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

        public void checkAction(string action)
        {
            if (action == "Move\r")
            {
                move();
            }
            else if (action == "TurnLeft\r")
            {
                turn();
            }
            else if (action == "Paint\r")
            {
                if (!grid[activeRow, activeColumn].getPaint())
                {
                    grid[activeRow, activeColumn].setImage(paint);
                    grid[activeRow, activeColumn].setPaint();
                }

            }
            else if (action == "Erase\r")
            {
                grid[activeRow, activeColumn].setImage(arrow);
                grid[activeRow, activeColumn].setPaint();
            }

        }
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
          

                if (moveIsValid())
                {
                int r = nextRow();
                int c = nextCol();
                     if (grid[activeRow, activeColumn].getPaint())
                     {
                       grid[activeRow, activeColumn].setImage(black);
                     }
                      else
                     {
                        grid[activeRow, activeColumn].setImage(blank);
                        }


                    if (grid[r, c].getPaint())
                    {
                        grid[r, c].setImage(paint);
                }
                    else { 
                        grid[r, c].setImage(arrow);
                }

                   activeRow = r;
                   activeColumn = c;

                }
                
                }
            
    }
}
