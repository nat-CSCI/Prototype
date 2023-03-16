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
using System.IO;

namespace Prototype
{
    public partial class main : Form
    {

        static int activeRow = 6;
        static int activeColumn = 0;
        static int direction = 1;
        static bool error = false;
        static int progPlace = 0;
        static int loopPlace = 0;
        static String s;
        static String[] prog;
        static bool ifTrue = false;
        static bool whileLoop = false;
        static bool getActions = false;                //Signifies if getting the contents of the loop or not
        static string condition = "";                  //Condition that needs to be met for loop
        static string[] x;                             //While loop string
        static string[] loopActions = new string[100]; //actions performed in loop
        static int numActions = 0;                     //Numbers of actions in a loop
        static int place = 0;                          //Place in loop
        static bool forLoop = false;
        static bool greaterthan = false;              //Show whether for loop is greater than or less than
        static int startValue = 0;                    //Starting value for a for loop
        static bool up = true;
        static int loopVariable = 0;
        static bool runElse = false;
        static bool progDone = false;
        static int counter = 0;

        box[,] grid = new box[7, 7];

        Image arrow = Prototype.Properties.Resources.arrow;
        Image blank = Prototype.Properties.Resources.white;
        Image block = Prototype.Properties.Resources.blocked;
        Image paint = Prototype.Properties.Resources.black;
        Image black = Prototype.Properties.Resources.painted;

        public main()
        {
            InitializeComponent();

            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    PictureBox p = new PictureBox();
                    

                    p.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
                    p.Image = blank;
                    p.Size = new System.Drawing.Size(119, 118);
                    p.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
                    p.Click += new System.EventHandler(this.pictureBox_block);
                    grid[i, j] = new box(p, i, j);
                }
            }
        }

        private void main_Load(object sender, EventArgs e)
        {

            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    tableLayoutPanel1.Controls.Add(grid[i,j].getPictureBox(), j, i);
                }
            }

            //foreach (PictureBox pb in tableLayoutPanel1.Controls)
            //{

            //    grid[tableLayoutPanel1.GetRow(pb), tableLayoutPanel1.GetColumn(pb)] = new box(pb, tableLayoutPanel1.GetRow(pb), tableLayoutPanel1.GetColumn(pb));
            //}

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
           
            textBox1.Text = textBox1.Text.Insert(textBox1.SelectionStart, e.Data.GetData(DataFormats.Text).ToString());
            textBox1.Select(textBox1.Text.Length, 0);

        }



        private void pictureBox_block(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;

           
            if (pb.Parent.Equals(tableLayoutPanel1))
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
         Dragging function for picture boxes
         */

        private void pictureBox_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            
            PictureBox pb = (PictureBox)sender;
            if (pb.Name == "pictureBoxMove")
            {
                pb.DoDragDrop("Move\r\n", DragDropEffects.All);
            }
            else if (pb.Name == "pictureBoxTurnLeft")
            {
                pb.DoDragDrop("TurnLeft\r\n", DragDropEffects.All);
            }
            else if (pb.Name == "pictureBoxTurnRight")
            {
                pb.DoDragDrop("TurnRight\r\n", DragDropEffects.All);
            }
            else if (pb.Name == "pictureBoxTurnAround")
            {
                pb.DoDragDrop("TurnAround\r\n", DragDropEffects.All);
            }
            else if (pb.Name == "pictureBoxPaint")
            {
                pb.DoDragDrop("Paint\r\n", DragDropEffects.All);
            }
            else if (pb.Name == "pictureBoxErase")
            {
                pb.DoDragDrop("Erase\r\n", DragDropEffects.All);
            }
            else if (pb.Name == "pictureBoxWhile")
            {
                pb.DoDragDrop("while(){\r\n\r\n}\r\n", DragDropEffects.All);
            }
            else if (pb.Name == "pictureBoxFor")
            {
                pb.DoDragDrop("for( ; ; ){\r\n\r\n}\r\n", DragDropEffects.All);
            }
            else if (pb.Name == "pictureBoxIf")
            {
                pb.DoDragDrop("if( ){\r\n\r\n}\r\n", DragDropEffects.All);
            }
            else if (pb.Name == "pictureBoxPainted")
            {
                pb.DoDragDrop("isPainted", DragDropEffects.All);
            }
            else if (pb.Name == "pictureBoxNotPainted")
            {
                pb.DoDragDrop("notPainted", DragDropEffects.All);
            }
            else if (pb.Name == "pictureBoxNotBlocked")
            {
                pb.DoDragDrop("notBlocked", DragDropEffects.All);
            }
            else if (pb.Name == "pictureBoxLeftBlocked")
            {
                pb.DoDragDrop("leftBlocked", DragDropEffects.All);

            }
            else if (pb.Name == "pictureBoxRightBlocked")
            {
                pb.DoDragDrop("rightBlocked", DragDropEffects.All);
            }
            else if (pb.Name == "pictureBoxBehindBlocked")
            {
                pb.DoDragDrop("behindBlocked", DragDropEffects.All);
            }
          } 


        //*******************************

        public async void button1_Click(object sender, EventArgs e)
        {
            s = textBox1.Text.ToString().ToLower();
            prog = s.Split('\n');
            ifTrue = false;
            whileLoop = false;
            getActions = false;                //Signifies if getting the contents of the loop or not
            loopActions = new string[100]; //actions performed in loop
            numActions = 0;                     //Numbers of actions in a loop
            place = 0;                          //Place in loop
            forLoop = false;
            greaterthan = false;              //Show whether for loop is greater than or less than
            startValue = 0;                    //Starting value for a for loop
            up = true;                        //Whether to increment up or down for a for loop
          
            resetGrid();
            tableLayoutPanel1.BackColor = Color.Green;
            await Task.Delay(700);
            
            buttonRun.Enabled = false;
            buttonReset.Enabled = false;
            textBox1.Enabled = false;

            foreach (string progItem in prog)
            {
               
                if (!error) //Error occurs if a move is not possible
                {
                   
                    if (progItem.Contains("while")) //While loop
                    {
                        label3.Text = "Running: " + progItem;
                        whileLoop = true;
                        getActions = true;
                        x = progItem.Split('(');

                        condition = x[1];

                    }
                    else if (progItem.Contains("if")) //if statement
                    {
                        label3.Text = "Running: " + progItem;
                        ifTrue = true;
                        x = progItem.Split('(');
                        condition = x[1];
                        getActions = true;
                    }
                    else if (progItem.Contains("for")) //for loop
                    {
                        label3.Text = "Running: " + progItem;
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
                        else
                        {
                            greaterthan = false;
                        }

                        if (!progItem.Contains('+'))
                        {
                            up = false;
                        }
                        else
                        {
                            up = true;
                        }

                    }  
                    else if (getActions) //While loop
                    {
                        label1.Text = "Getting Actions for Loop or Conditional";
                        if (progItem != "}\r")
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
                        if (condition.Contains("notblocked"))
                        {
                            while (moveIsValid())
                            {
                                for (int i = 0; i < numActions; i++)
                                {
                                    label3.Text = "Running: " + loopActions[i];
                                    checkAction(loopActions[i]);
                                    await Task.Delay(700);

                                }

                            }

                        }
                        else if (condition.Contains("ispainted"))
                        {
                            while (grid[activeRow, activeColumn].getPaint())
                            {
                                for (int i = 0; i < numActions; i++)
                                {
                                    label3.Text = "Running: " + loopActions[i];
                                    checkAction(loopActions[i]);
                                    await Task.Delay(700);

                                }

                            }

                        }
                        else if (condition.Contains("notpainted"))
                        {
                            while (!grid[activeRow, activeColumn].getPaint())
                            {
                                for (int i = 0; i < numActions; i++)
                                {
                                    label3.Text = "Running: " + loopActions[i];
                                    checkAction(loopActions[i]);
                                    await Task.Delay(700);

                                }

                            }

                        }
                        else if (condition.Contains("leftblocked"))
                        {
                            if (direction == 1)
                            {
                                while (grid[activeRow - 1, activeColumn].getBlocked())
                                {
                                    for (int i = 0; i < numActions; i++)
                                    {
                                        label3.Text = "Running: " + loopActions[i];
                                        checkAction(loopActions[i]);
                                        await Task.Delay(700);

                                    }

                                }
                            }
                            else if (direction == 2)
                            {
                                while (grid[activeRow, activeColumn - 1].getBlocked())
                                {
                                    for (int i = 0; i < numActions; i++)
                                    {
                                        label3.Text = "Running: " + loopActions[i];
                                        checkAction(loopActions[i]);
                                        await Task.Delay(700);

                                    }

                                }
                            }
                            else if (direction == 3)
                            {
                                while (grid[activeRow + 1, activeColumn].getBlocked())
                                {
                                    for (int i = 0; i < numActions; i++)
                                    {
                                        label3.Text = "Running: " + loopActions[i];
                                        checkAction(loopActions[i]);
                                        await Task.Delay(700);

                                    }

                                }
                            }
                            else
                            {
                                while (grid[activeRow, activeColumn + 1].getBlocked())
                                {
                                    for (int i = 0; i < numActions; i++)
                                    {
                                        label3.Text = "Running: " + loopActions[i];
                                        checkAction(loopActions[i]);
                                        await Task.Delay(700);

                                    }

                                }
                            }
                            whileLoop = false;
                            label3.Text = "Running: " + progItem;
                            checkAction(progItem);
                            await Task.Delay(700);

                        }

                        numActions = 0;
                        Array.Clear(loopActions, 0, loopActions.Length);
                    }
                    else if (ifTrue)
                    {
                        if (condition.Contains("notblocked"))
                        {
                            if (moveIsValid())
                            {
                                for (int i = 0; i < numActions; i++)
                                {
                                    label3.Text = "Running: " + loopActions[i];
                                    checkAction(loopActions[i]);
                                    await Task.Delay(700);

                                }

                            }
                            else {
                                runElse = true;
                            }

                        }
                        else if (condition.Contains("ispainted"))
                        {
                            if (grid[activeRow, activeColumn].getPaint())
                            {
                                for (int i = 0; i < numActions; i++)
                                {
                                    label3.Text = "Running: " + loopActions[i];
                                    checkAction(loopActions[i]);
                                    await Task.Delay(700);

                                }

                            }
                            else
                            {
                                runElse = true;
                            }

                        }
                        else if (condition.Contains("notpainted"))
                        {
                            if (!grid[activeRow, activeColumn].getPaint())
                            {
                                for (int i = 0; i < numActions; i++)
                                {
                                    label3.Text = "Running: " + loopActions[i];
                                    checkAction(loopActions[i]);
                                    await Task.Delay(700);

                                }

                            }
                            else
                            {
                                runElse = true;
                            }

                        }
                        else if (condition.Contains("leftblocked"))
                        {
                            if (direction == 1)
                            {
                                if (grid[activeRow - 1, activeColumn].getBlocked())
                                {
                                    for (int i = 0; i < numActions; i++)
                                    {
                                        label3.Text = "Running: " + loopActions[i];
                                        checkAction(loopActions[i]);
                                        await Task.Delay(700);

                                    }

                                }
                                else
                                {
                                    runElse = true;
                                }
                            }
                            else if (direction == 2)
                            {
                                if (grid[activeRow, activeColumn - 1].getBlocked())
                                {
                                    for (int i = 0; i < numActions; i++)
                                    {
                                        label3.Text = "Running: " + loopActions[i];
                                        checkAction(loopActions[i]);
                                        await Task.Delay(700);

                                    }

                                }
                                else
                                {
                                    runElse = true;
                                }
                            }
                            else if (direction == 3)
                            {
                                if (grid[activeRow + 1, activeColumn].getBlocked())
                                {
                                    for (int i = 0; i < numActions; i++)
                                    {
                                        label3.Text = "Running: " + loopActions[i];
                                        checkAction(loopActions[i]);
                                        await Task.Delay(700);

                                    }

                                }
                                else
                                {
                                    runElse = true;
                                }
                            }
                            else
                            {
                                if (grid[activeRow, activeColumn + 1].getBlocked())
                                {
                                    for (int i = 0; i < numActions; i++)
                                    {
                                        label3.Text = "Running: " + loopActions[i];
                                        checkAction(loopActions[i]);
                                        await Task.Delay(700);

                                    }

                                }
                                else
                                {
                                    runElse = true;
                                }
                            }
                        }
                        else if (condition.Contains("rightblocked"))
                        {
                            if (direction == 1)
                            {
                                if (grid[activeRow + 1, activeColumn].getBlocked())
                                {
                                    for (int i = 0; i < numActions; i++)
                                    {
                                        label3.Text = "Running: " + loopActions[i];
                                        checkAction(loopActions[i]);
                                        await Task.Delay(700);

                                    }

                                }
                                else
                                {
                                    runElse = true;
                                }
                            }
                            else if (direction == 2)
                            {
                                if (grid[activeRow, activeColumn + 1].getBlocked())
                                {
                                    for (int i = 0; i < numActions; i++)
                                    {
                                        label3.Text = "Running: " + loopActions[i];
                                        checkAction(loopActions[i]);
                                        await Task.Delay(700);

                                    }

                                }
                                else
                                {
                                    runElse = true;
                                }
                            }
                            else if (direction == 3)
                            {
                                if (grid[activeRow - 1, activeColumn].getBlocked())
                                {
                                    for (int i = 0; i < numActions; i++)
                                    {
                                        label3.Text = "Running: " + loopActions[i];
                                        checkAction(loopActions[i]);
                                        await Task.Delay(700);

                                    }

                                }
                                else
                                {
                                    runElse = true;
                                }
                            }
                            else
                            {
                                if (grid[activeRow, activeColumn - 1].getBlocked())
                                {
                                    for (int i = 0; i < numActions; i++)
                                    {
                                        label3.Text = "Running: " + loopActions[i];
                                        checkAction(loopActions[i]);
                                        await Task.Delay(700);

                                    }

                                }
                                else
                                {
                                    runElse = true;
                                }
                            }
                        }
                        else if (condition.Contains("behindblocked"))
                        {
                            if (direction == 1)
                            {
                                if (grid[activeRow, activeColumn - 1].getBlocked())
                                {
                                    for (int i = 0; i < numActions; i++)
                                    {
                                        label3.Text = "Running: " + loopActions[i];
                                        checkAction(loopActions[i]);
                                        await Task.Delay(700);

                                    }

                                }
                                else
                                {
                                    runElse = true;
                                }
                            }
                            else if (direction == 2)
                            {
                                if (grid[activeRow + 1, activeColumn].getBlocked())
                                {
                                    for (int i = 0; i < numActions; i++)
                                    {
                                        label3.Text = "Running: " + loopActions[i];
                                        checkAction(loopActions[i]);
                                        await Task.Delay(700);

                                    }

                                }
                                else
                                {
                                    runElse = true;
                                }
                            }
                            else if (direction == 3)
                            {
                                if (grid[activeRow, activeColumn + 1].getBlocked())
                                {
                                    for (int i = 0; i < numActions; i++)
                                    {
                                        label3.Text = "Running: " + loopActions[i];
                                        checkAction(loopActions[i]);
                                        await Task.Delay(700);

                                    }

                                }
                                else
                                {
                                    runElse = true;
                                }
                            }
                            else
                            {
                                if (grid[activeRow - 1, activeColumn].getBlocked())
                                {
                                    for (int i = 0; i < numActions; i++)
                                    {
                                        label3.Text = "Running: " + loopActions[i];
                                        checkAction(loopActions[i]);
                                        await Task.Delay(700);

                                    }

                                }
                                else
                                {
                                    runElse = true;
                                }
                            }
                        }
                        numActions = 0;
                        Array.Clear(loopActions, 0, loopActions.Length);
                        ifTrue = false;
                        if (progItem.Contains("else"))
                        {
                            getActions = true;
                        }
                    }
                    else if (forLoop)
                    {
                        
                        if (!greaterthan)
                        {
                            if (up)
                            {
                                for (int i = startValue; i < int.Parse(condition.Split('<')[1]); i++)
                                {
                                    for (int j = 0; j < numActions; j++)
                                    {
                                        label3.Text = "Running: " + loopActions[j];
                                        checkAction(loopActions[j]);
                                        await Task.Delay(700);
                                    }

                                }
                                forLoop = false;
                                label3.Text = "Running: " + progItem;
                                checkAction(progItem);
                                await Task.Delay(700);
                            }
                            else
                            {
                                for (int i = startValue; i < int.Parse(condition.Split('<')[1]); i--)
                                {
                                    for (int j = 0; j < numActions; j++)
                                    {
                                        label3.Text = "Running: " + loopActions[j];
                                        checkAction(loopActions[j]);
                                        await Task.Delay(700);
                                    }

                                }
                                forLoop = false;
                                label3.Text = "Running: " + progItem;
                                checkAction(progItem);
                                await Task.Delay(700);
                            }
                        }
                        else
                        {
                            if (up)
                            {
                                for (int i = startValue; i > int.Parse(condition.Split('<')[1]); i++)
                                {
                                    for (int j = 0; j < numActions; j++)
                                    {
                                        label3.Text = "Running: " + loopActions[j];
                                        checkAction(loopActions[j]);
                                        await Task.Delay(700);
                                    }

                                }
                                forLoop = false;
                                label3.Text = "Running: " + progItem;
                                checkAction(progItem);
                                await Task.Delay(700);
                            }
                            else
                            {
                                for (int i = startValue; i > int.Parse(condition.Split('<')[1]); i--)
                                {
                                    for (int j = 0; j < numActions; j++)
                                    {
                                        label3.Text = "Running: " + loopActions[j];
                                        checkAction(loopActions[j]);
                                        await Task.Delay(700);
                                    }

                                }
                                forLoop = false;
                                label3.Text = "Running: " + progItem;
                                checkAction(progItem);
                                await Task.Delay(700);
                            }
                        }
                        numActions = 0;
                        Array.Clear(loopActions, 0, loopActions.Length);
                    }
                    else if (runElse)
                    {
                        for (int i = 0; i < numActions; i++)
                        {
                            label3.Text = "Running: " + loopActions[i];
                            checkAction(loopActions[i]);
                            await Task.Delay(700);

                        }
                        runElse = false;
                        label3.Text = "Running: " + progItem;
                        checkAction(progItem);
                        await Task.Delay(700);
                    }
                    else
                    {
                        label3.Text = "Running: " + progItem;
                        checkAction(progItem);
                        await Task.Delay(700);
                    }
                }
                else
                {
                    label3.Text = "Stopped due to invalid move";
                }


            }

            tableLayoutPanel1.BackColor = Color.YellowGreen;
            buttonRun.Enabled = true;
            buttonReset.Enabled = true;
            textBox1.Enabled = true;
            label3.Text = "Done Running";



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

        private void buttonReset_Click(object sender, EventArgs e)
        {
           resetGrid();
        }

        public void resetGrid()
        {
            tableLayoutPanel1.BackColor = Color.DarkGray;
            error = false;
            foreach (box b in grid)
            {
                b.reset();
            }

            grid[6, 0].setImage(arrow);
            activeColumn = 0;
            activeRow = 6;
            while(direction != 1)
            {
                turn();
            }



        }

        //Movement functions

        public void checkAction(string action)
        {
            if (action == "move\r") { move(); }
            else if (action == "turnleft\r") { turn(); }
            else if (action == "turnright\r") { turnRight(); }
            else if (action == "turnaround\r") { turnAround(); }
            else if (action == "paint\r") { 
                if (!grid[activeRow, activeColumn].getPaint())
                {
                    grid[activeRow, activeColumn].setImage(paint);
                    grid[activeRow, activeColumn].setPaint();
                }
            }
            else if (action == "erase\r") { 
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
        public void turnRight()
        {
            turn();
            turn();
            turn();
        }
        public void turnAround()
        {
            turn();
            turn();
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
            else
            {
                error = true;
            }
         }

        public async void ifHelper (string progItem)
        {
            if (loopPlace < numActions)
            {
                label3.Text = "Running: " + progItem;
                checkAction(progItem);
                await Task.Delay(700);
                loopPlace++;
                if (loopPlace >= numActions)
                {
                    ifTrue = false;
                    progPlace++;
                    Array.Clear(loopActions, 0, loopActions.Length);
                    loopPlace = 0;
                }
            }
        }

        private async void buttonStep_Click(object sender, EventArgs e)
        {
            counter++;
            tableLayoutPanel1.BackColor = Color.Green;
            buttonRun.Enabled = false;
            buttonReset.Enabled = false;
            textBox1.Enabled = false;
            buttonStep.Enabled = false;
            prog = textBox1.Text.ToString().ToLower().Split('\n');
            string progItem;

            if (progDone)
            {
                resetGrid();
                progDone = false;
            }
            else
            {
                if (progPlace < prog.Length)
                {
                    if ((whileLoop || forLoop || ifTrue) && !getActions)
                    {
                        if (loopPlace >= numActions)
                        {
                            loopPlace = 0;
                        }
                        progItem = loopActions[loopPlace];
                    }
                    else
                    { progItem = prog[progPlace]; }

                    if (!error) //Error occurs if a move is not possible
                    {

                        if (progItem.Contains("while")) //While loop
                        {
                            label3.Text = "Running: " + progItem;
                            whileLoop = true;
                            getActions = true;
                            x = progItem.Split('(');

                            condition = x[1];
                            progPlace++;

                        }
                        else if (progItem.Contains("if")) //if statement
                        {
                            label3.Text = "Running: " + progItem;
                            ifTrue = true;
                            x = progItem.Split('(');
                            condition = x[1];
                            getActions = true;
                            progPlace++;
                        }
                        else if (progItem.Contains("for")) //for loop
                        {
                            label3.Text = "Running: " + progItem;
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
                            else
                            {
                                greaterthan = false;
                            }

                            if (!progItem.Contains('+'))
                            {
                                up = false;
                            }
                            else
                            {
                                up = true;
                            }
                            progPlace++;
                        }
                        else if (getActions) //While loop
                        {

                            label3.Text = "Getting Actions for Loop or Conditional";
                            string action = prog[progPlace];
                            int p = progPlace;
                            while (action != "}\r")
                            {
                                if (action != "\r")
                                {
                                    loopActions[place] = action;
                                    place++;
                                    numActions++;
                                }
                                p++;
                                action = prog[p];
                            }

                            getActions = false;
                            place = 0;

                        }
                        else if (whileLoop) //While loop
                        {

                            if (condition.Contains("notblocked"))
                            {
                                if (moveIsValid())
                                {
                                    if (loopPlace < numActions)
                                    {
                                        label3.Text = "Running: " + progItem;
                                        checkAction(progItem);
                                        await Task.Delay(700);
                                        loopPlace++;
                                    }
                                    else
                                    {
                                        loopPlace = 0;
                                    }

                                }
                                else { progPlace++; whileLoop = false; }

                            }
                            else if (condition.Contains("ispainted"))
                            {
                                if (grid[activeRow, activeColumn].getPaint())
                                {
                                    if (loopPlace < numActions)
                                    {
                                        label3.Text = "Running: " + progItem;
                                        checkAction(progItem);
                                        await Task.Delay(700);
                                        loopPlace++;
                                    }
                                    else
                                    {
                                        loopPlace = 0;
                                    }

                                }
                                else { progPlace++; whileLoop = false; }

                            }
                            else if (condition.Contains("notpainted"))
                            {
                                if (!grid[activeRow, activeColumn].getPaint())
                                {
                                    if (loopPlace < numActions)
                                    {
                                        label3.Text = "Running: " + progItem;
                                        checkAction(progItem);
                                        await Task.Delay(700);
                                        loopPlace++;
                                    }
                                    else
                                    {
                                        loopPlace = 0;
                                    }

                                }
                                else { progPlace++; whileLoop = false; }

                            }
                            else if (condition.Contains("leftblocked"))
                            {
                                if (direction == 1)
                                {
                                    if (grid[activeRow - 1, activeColumn].getBlocked())
                                    {
                                        if (loopPlace < numActions)
                                        {
                                            label3.Text = "Running: " + progItem;
                                            checkAction(progItem);
                                            await Task.Delay(700);
                                            loopPlace++;
                                        }
                                        else
                                        {
                                            loopPlace = 0;
                                        }

                                    }
                                    else { progPlace++; whileLoop = false; }
                                }
                                else if (direction == 2)
                                {
                                    if (grid[activeRow, activeColumn - 1].getBlocked())
                                    {
                                        if (loopPlace < numActions)
                                        {
                                            label3.Text = "Running: " + progItem;
                                            checkAction(progItem);
                                            await Task.Delay(700);
                                            loopPlace++;
                                        }
                                        else
                                        {
                                            loopPlace = 0;
                                        }

                                    }
                                    else { progPlace++; whileLoop = false; }
                                }
                                else if (direction == 3)
                                {
                                    if (grid[activeRow + 1, activeColumn].getBlocked())
                                    {
                                        if (loopPlace < numActions)
                                        {
                                            label3.Text = "Running: " + progItem;
                                            checkAction(progItem);
                                            await Task.Delay(700);
                                            loopPlace++;
                                        }
                                        else
                                        {
                                            loopPlace = 0;
                                        }

                                    }
                                    else { progPlace++; whileLoop = false; }
                                }
                                else
                                {
                                    if (grid[activeRow, activeColumn + 1].getBlocked())
                                    {
                                        if (loopPlace < numActions)
                                        {
                                            label3.Text = "Running: " + progItem;
                                            checkAction(progItem);
                                            await Task.Delay(700);
                                            loopPlace++;
                                        }
                                        else
                                        {
                                            loopPlace = 0;
                                        }

                                    }
                                    else { progPlace++; whileLoop = false; }
                                }


                            }
                        }
                        else if (ifTrue)
                        {
                            if (condition.Contains("notblocked"))
                            {
                                if (moveIsValid())
                                {
                                    ifHelper(progItem);


                                }
                                else
                                {
                                    progPlace++;
                                    runElse = true;
                                    getActions = true;
                                    ifTrue = false;
                                }

                            }
                            else if (condition.Contains("ispainted"))
                            {
                                if (grid[activeRow, activeColumn].getPaint())
                                {
                                    ifHelper(progItem);


                                }
                                else { progPlace++; ifTrue = false; }

                            }
                            else if (condition.Contains("notpainted"))
                            {
                                if (!grid[activeRow, activeColumn].getPaint())
                                {
                                    ifHelper(progItem);


                                }
                                else { progPlace++; ifTrue = false; }

                            }
                            else if (condition.Contains("leftblocked"))
                            {
                                if (direction == 1)
                                {
                                    if (grid[activeRow - 1, activeColumn].getBlocked())
                                    {
                                        ifHelper(progItem);


                                    }
                                    else { progPlace++; ifTrue = false; }
                                }
                                else if (direction == 2)
                                {
                                    if (grid[activeRow, activeColumn - 1].getBlocked())
                                    {
                                        ifHelper(progItem);


                                    }
                                    else { progPlace++; ifTrue = false; }
                                }
                                else if (direction == 3)
                                {
                                    if (grid[activeRow + 1, activeColumn].getBlocked())
                                    {
                                        ifHelper(progItem);


                                    }
                                    else { progPlace++; }
                                }
                                else
                                {
                                    if (grid[activeRow, activeColumn + 1].getBlocked())
                                    {
                                        ifHelper(progItem);


                                    }
                                    else { progPlace++; ifTrue = false; }
                                }
                            }
                            else if (condition.Contains("rightblocked"))
                            {
                                if (direction == 1)
                                {
                                    if (grid[activeRow + 1, activeColumn].getBlocked())
                                    {
                                        ifHelper(progItem);


                                    }
                                    else { progPlace++; ifTrue = false; }
                                }
                                else if (direction == 2)
                                {
                                    if (grid[activeRow, activeColumn + 1].getBlocked())
                                    {
                                        ifHelper(progItem);


                                    }
                                    else { progPlace++; ifTrue = false; }
                                }
                                else if (direction == 3)
                                {
                                    if (grid[activeRow - 1, activeColumn].getBlocked())
                                    {
                                        ifHelper(progItem);


                                    }
                                    else { progPlace++; ifTrue = false; }
                                }
                                else
                                {
                                    if (grid[activeRow, activeColumn - 1].getBlocked())
                                    {
                                        ifHelper(progItem);


                                    }
                                    else { progPlace++; ifTrue = false; }
                                }
                            }
                            else if (condition.Contains("behindblocked"))
                            {
                                if (direction == 1)
                                {
                                    if (grid[activeRow, activeColumn - 1].getBlocked())
                                    {
                                        ifHelper(progItem);


                                    }
                                    else { progPlace++; ifTrue = false; }
                                }
                                else if (direction == 2)
                                {
                                    if (grid[activeRow + 1, activeColumn].getBlocked())
                                    {
                                        ifHelper(progItem);


                                    }
                                    else { progPlace++; ifTrue = false; }
                                }
                                else if (direction == 3)
                                {
                                    if (grid[activeRow, activeColumn + 1].getBlocked())
                                    {
                                        ifHelper(progItem);


                                    }
                                    else { progPlace++; ifTrue = false; }
                                }
                                else
                                {
                                    if (grid[activeRow - 1, activeColumn].getBlocked())
                                    {
                                        ifHelper(progItem);


                                    }
                                    else { progPlace++; ifTrue = false; }
                                }
                            }
                            getActions = true;
                        }
                        else if (forLoop)
                        {

                            if (!greaterthan)
                            {
                                if (up)
                                {
                                    if (loopVariable < int.Parse(condition.Split('<')[1]))
                                    {
                                        if (loopPlace < numActions)
                                        {
                                            label3.Text = "Running: " + progItem;
                                            checkAction(progItem);
                                            await Task.Delay(700);
                                            if (loopPlace + 1 < numActions)
                                            { loopPlace++; }
                                            else
                                            {
                                                loopPlace = 0;
                                                loopVariable++;
                                                if (loopVariable >= int.Parse(condition.Split('<')[1]))
                                                {
                                                    progPlace++;
                                                    forLoop = false;
                                                }
                                            }

                                        }


                                    }

                                }
                                else
                                {
                                    if (loopVariable < int.Parse(condition.Split('<')[1]))
                                    {
                                        if (loopPlace < numActions)
                                        {
                                            label3.Text = "Running: " + progItem;
                                            checkAction(progItem);
                                            await Task.Delay(700);
                                            if (loopPlace + 1 < numActions)
                                            { loopPlace++; }
                                            else
                                            {
                                                loopPlace = 0;
                                                loopVariable--;
                                            }

                                        }
                                    }
                                    else { progPlace++; }
                                }
                            }
                            else
                            {
                                if (up)
                                {
                                    if (loopVariable > int.Parse(condition.Split('>')[1]))
                                    {
                                        if (loopPlace < numActions)
                                        {
                                            label3.Text = "Running: " + progItem;
                                            checkAction(progItem);
                                            await Task.Delay(700);
                                            if (loopPlace + 1 < numActions)
                                            { loopPlace++; }
                                            else
                                            {
                                                loopPlace = 0;
                                                loopVariable++;
                                            }

                                        }
                                    }
                                    else { progPlace++; }
                                }
                                else
                                {
                                    if (loopVariable > int.Parse(condition.Split('>')[1]))
                                    {
                                        if (loopPlace < numActions)
                                        {
                                            label3.Text = "Running: " + progItem;
                                            checkAction(progItem);
                                            await Task.Delay(700);
                                            if (loopPlace + 1 < numActions)
                                            { loopPlace++; }
                                            else
                                            {
                                                loopPlace = 0;
                                                loopVariable--;
                                            }

                                        }
                                    }
                                    else { progPlace++; }
                                }
                            }
                        }
                        else if (runElse)
                        {
                            ifHelper(progItem);
                        }
                        else
                        {
                            label3.Text = "Running: " + progItem;
                            checkAction(progItem);
                            await Task.Delay(700);
                            progPlace++;
                        }

                    }
                    else
                    {
                        label3.Text = "Stopped due to invalid move";
                    }
                }
                else
                {
                    tableLayoutPanel1.BackColor = Color.YellowGreen;
                    buttonRun.Enabled = true;
                    buttonReset.Enabled = true;
                    textBox1.Enabled = true;
                    loopPlace = 0;
                    loopVariable = 0;
                    progPlace = 0;
                    numActions = 0;
                    label3.Text = "Done Running" + counter;
                    Array.Clear(prog, 0, prog.Length);
                    Array.Clear(loopActions, 0, loopActions.Length);
                    progDone = true;

                    //end of stepping
                }
            }
            buttonStep.Enabled = true;
        }

        private void buttonLesson_Click(object sender, EventArgs e)
        {
            Lesson l = new Lesson(this);
            l.Show();

        }
    }
}
