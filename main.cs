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


        //************************************* 
        //Loop and if statment variables
        //************************************

        static int loopPlace = 0;                      //Shows current place in a loop
        static bool ifTrue = false;                    //If the program is running an if statement
        static bool whileLoop = false;                 //If the program is running a while loop
        static bool forLoop = false;                   //If the program is running a for loop
        static bool getActions = false;                //Signifies if getting the contents of the loop or not
        static string condition = "";                  //Condition that needs to be met for loop
        static string[] x;                             //While loop string
        static string[] loopActions = new string[100]; //actions performed in loop
        static int numActions = 0;                     //Numbers of actions in a loop
        static int place = 0;                          //Place in loop
        static bool greaterthan = false;  //Show whether for loop is greater than or less than
        static int startValue = 0;        //Starting value for a for loop
        static bool up = true;            //Whether to increment up or down for a for loop
        static int loopVariable = 0;      //The varible used to increment a for loop
        static bool runElse = false;      //Indicates whether or not the else needs to run

        //************************************* 
        // Variables to keep track of place in grid and program
        //************************************

        box[,] grid = new box[7, 7];
        static int activeRow = 6;
        static int activeColumn = 0;
        static int direction = 1;
        static int progPlace = 0;         //Shows current place in program
        static String s;
        public String[] prog;
        static bool running = false;
        int gridSize = 7;
        static bool progDone = false;     //Indicates whether the program has run completely or not
        static bool error = false;
        public bool progSet = false;

        //************************************* 
        // Variables for saving/opening files and opening lessons
        //************************************

        int lesson = 1;                   //Indicates what number lesson is selected              
        bool fileModified = false;
        string fileName = "Untitled";



        //************************************* 
        // Image variables
        //************************************
        Image arrow = Prototype.Properties.Resources.karela;
        Image blank = Prototype.Properties.Resources.white;
        Image block = Prototype.Properties.Resources.blocked;
        Image paint = Prototype.Properties.Resources.black;
        Image black = Prototype.Properties.Resources.painted;

        public main()
        {
            InitializeComponent();

            //Programmatically create pictureboxes in grid
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
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

            //Add pictureboxes to table layout
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    tableLayoutPanel1.Controls.Add(grid[i,j].getPictureBox(), j, i);
                }
            }

            //Set bottom corner as active image
            grid[6,0].setImage(arrow);
      
        }

        
        //Blocks a picturebox when clicked
        private void pictureBox_block(object sender, EventArgs e)
        {
            if (!running)
            {
                PictureBox pb = (PictureBox)sender;


                if (pb.Parent.Equals(tableLayoutPanel1))
                {
                    int r = tableLayoutPanel1.GetRow(pb);
                    int c = tableLayoutPanel1.GetColumn(pb);
                    if (grid[r, c].getBlocked())
                    {
                        pb.Image = blank;
                        grid[r, c].setBlocked();
                    }
                    else
                    {
                        pb.Image = block;
                        grid[r, c].setBlocked();
                    }

                }
            }

        }


        //************************************* 
        // Dragging events
        //************************************
        private void richTextBox1_DragEnter(object sender, DragEventArgs e)
        {
            label1.Text = "enter";

            richTextBox1.Focus();
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }
        private void richTextBox1_DragDrop(object sender,
        System.Windows.Forms.DragEventArgs e)
        {


            richTextBox1.Focus();
            int index = richTextBox1.GetCharIndexFromPosition(richTextBox1.PointToClient(Cursor.Position));
            richTextBox1.SelectionStart = index;
            richTextBox1.SelectionLength = 0;
            Point cp = richTextBox1.GetPositionFromCharIndex(index);
            char c = richTextBox1.GetCharFromPosition(cp);
            label1.Text = index.ToString();

            if(richTextBox1.GetCharFromPosition(cp) == '\0')
            {
                richTextBox1.Text = richTextBox1.Text.Insert(richTextBox1.SelectionStart, e.Data.GetData(DataFormats.Text).ToString()+"\n");
            
            }
            //else if (richTextBox1.GetCharFromPosition(cp) != '\n')
            //{
            //    richTextBox1.Text = richTextBox1.Text.Insert(richTextBox1.SelectionStart, "\n" + e.Data.GetData(DataFormats.Text).ToString());

            //}
            else
            {
                richTextBox1.Text = richTextBox1.Text.Insert(richTextBox1.SelectionStart, "\n" + e.Data.GetData(DataFormats.Text).ToString());
            }
            richTextBox1.Select(richTextBox1.Text.Length, 0);

        }
        private void pictureBox_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            
            PictureBox pb = (PictureBox)sender;
            if (pb.Name == "pictureBoxMove")
            {
                pb.DoDragDrop("Move;\n", DragDropEffects.All);
            }
            else if (pb.Name == "pictureBoxTurnLeft")
            {
                pb.DoDragDrop("TurnLeft;\n", DragDropEffects.All);
            }
            else if (pb.Name == "pictureBoxTurnRight")
            {
                pb.DoDragDrop("TurnRight;\n", DragDropEffects.All);
            }
            else if (pb.Name == "pictureBoxTurnAround")
            {
                pb.DoDragDrop("TurnAround;\n", DragDropEffects.All);
            }
            else if (pb.Name == "pictureBoxPaint")
            {
                pb.DoDragDrop("Paint;\n", DragDropEffects.All);
            }
            else if (pb.Name == "pictureBoxErase")
            {
                pb.DoDragDrop("Erase;\n", DragDropEffects.All);
            }
            else if (pb.Name == "pictureBoxWhile")
            {
                pb.DoDragDrop("while();{ \r};", DragDropEffects.All);
            }
            else if (pb.Name == "pictureBoxFor")
            {
                pb.DoDragDrop("for( : : );{\r};", DragDropEffects.All);
            }
            else if (pb.Name == "pictureBoxIf")
            {
                pb.DoDragDrop("if( );{\r};", DragDropEffects.All);
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

        //************************************* 
        // Running the user's program
        //************************************
        public void setProg()
        {
            s = richTextBox1.Text.ToString().ToLower();
            prog = s.Split(';');
        }

        public async void button1_Click(object sender, EventArgs e)
        {
            if (!progSet)
            {
                setProg();
            }
            ifTrue = false;
            whileLoop = false;
            getActions = false;                
            loopActions = new string[100];     
            numActions = 0;                    
            place = 0;                         
            forLoop = false;
            greaterthan = false;               
            startValue = 0;                    
            up = true;
            progPlace = 0;


            if (!running)
            {
                //Reset grid and disable buttons

                buttonRun.Enabled = false;
                buttonReset.Enabled = false;
                richTextBox1.Enabled = false;
                buttonStep.Enabled = false;
                progDone = false;
                running = true;

                resetGrid();
                tableLayoutPanel1.BackColor = Color.Green;
                await Task.Delay(700);
                int startIndex = 0;

                //Go through each item in the program
                foreach (string progItem in prog)
                {
                    
                        if (!error) //Error occurs if a move is not possible
                        {

                            if (progItem.Contains("while")) //While loop
                            {
                                startIndex = richTextBox1.GetFirstCharIndexFromLine(progPlace);
                                richTextBox1.Select(startIndex, richTextBox1.Lines[progPlace].Length);
                                richTextBox1.SelectionBackColor = System.Drawing.Color.Yellow;
                                label3.Text = "Running: " + progItem;
                                whileLoop = true;
                                getActions = true;
                                x = progItem.Split('(');

                                condition = x[1];
                            await Task.Delay(1000);

                            }
                            else if (progItem.Contains("if")) //if statement
                            {
                                startIndex = richTextBox1.GetFirstCharIndexFromLine(progPlace);
                                richTextBox1.Select(startIndex, richTextBox1.Lines[progPlace].Length);
                                richTextBox1.SelectionBackColor = System.Drawing.Color.Yellow;
                                label3.Text = "Running: " + progItem;
                                ifTrue = true;
                                x = progItem.Split('(');
                                condition = x[1];
                                getActions = true;
                            }
                            else if (progItem.Contains("for")) //for loop
                            {
                                startIndex = richTextBox1.GetFirstCharIndexFromLine(progPlace);
                                richTextBox1.Select(startIndex, richTextBox1.Lines[progPlace].Length);
                                richTextBox1.SelectionBackColor = System.Drawing.Color.Yellow;
                                label3.Text = "Running: " + progItem;
                                forLoop = true;
                                getActions = true;
                                x = progItem.Split(':');
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
                            else if (getActions) //Get actions for loop or if statement
                            {
                                label1.Text = "Getting Actions for Loop or Conditional";
                                if (!progItem.Contains('}'))
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
                            else if (whileLoop) //Run while loop
                            {
                                if (condition.Contains("notblocked"))
                                {
                                    while (moveIsValid())
                                    {
                                        for (int i = 0; i < numActions; i++)
                                        {
                                            runAction(loopActions[i], i);
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
                                            runAction(loopActions[i], i);
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
                                            runAction(loopActions[i], i);
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
                                                runAction(loopActions[i], i);
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
                                                runAction(loopActions[i], i);
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
                                                runAction(loopActions[i], i);
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
                                                runAction(loopActions[i], i);
                                                await Task.Delay(700);
                                            }

                                        }
                                    }

                                    
                                    startIndex = richTextBox1.GetFirstCharIndexFromLine(progPlace);
                                    richTextBox1.Select(startIndex, richTextBox1.Lines[progPlace].Length);
                                    richTextBox1.SelectionBackColor = System.Drawing.Color.Yellow;
                                    label3.Text = "Running: " + progItem;
                                    checkAction(progItem);
                                    await Task.Delay(700);
                 
                                    richTextBox1.SelectionBackColor = System.Drawing.Color.Transparent;
                                }
                                whileLoop = false;
                                numActions = 0;
                                Array.Clear(loopActions, 0, loopActions.Length);
                            }
                            else if (ifTrue) //Run if statement if condition is true
                            {
                                if (condition.Contains("notblocked"))
                                {
                                    if (moveIsValid())
                                    {
                                        for (int i = 0; i < numActions; i++)
                                        {
                                            runAction(loopActions[i], i);
                                            await Task.Delay(700);
                                        }

                                    }
                                    else
                                    {
                                        runElse = true;
                                    }

                                }
                                else if (condition.Contains("ispainted"))
                                {
                                    if (grid[activeRow, activeColumn].getPaint())
                                    {
                                        for (int i = 0; i < numActions; i++)
                                        {
                                            runAction(loopActions[i], i);
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
                                            runAction(loopActions[i], i);
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
                                                runAction(loopActions[i], i);
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
                                                runAction(loopActions[i], i);
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
                                                runAction(loopActions[i], i);
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
                                                runAction(loopActions[i], i);
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
                                                runAction(loopActions[i], i);
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
                                                runAction(loopActions[i], i);
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
                                                runAction(loopActions[i], i);
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
                                                runAction(loopActions[i], i);
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
                                                runAction(loopActions[i], i);
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
                                                runAction(loopActions[i], i);
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
                                                runAction(loopActions[i], i);
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
                                                runAction(loopActions[i], i);
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
                            else if (forLoop) //Run for loop
                            {

                                if (!greaterthan)
                                {
                                    if (up)
                                    {
                                        for (int i = startValue; i < int.Parse(condition.Split('<')[1]); i++)
                                        {
                                            for (int j = 0; j < numActions; j++)
                                            {
                                                runAction(loopActions[j], j);
                                                await Task.Delay(700);
                                            }

                                        }
                                        forLoop = false;
                                       
                                        startIndex = richTextBox1.GetFirstCharIndexFromLine(progPlace-1);
                                        richTextBox1.Select(startIndex, richTextBox1.Lines[progPlace-1].Length);
                                        richTextBox1.SelectionBackColor = System.Drawing.Color.Yellow;
                                        label3.Text = "Running: " + progItem;
                                        checkAction(progItem);
                                        await Task.Delay(700);
                                        richTextBox1.SelectionBackColor = System.Drawing.Color.Transparent;
                                    }
                                    else
                                    {
                                        for (int i = startValue; i < int.Parse(condition.Split('<')[1]); i--)
                                        {
                                            for (int j = 0; j < numActions; j++)
                                            {
                                                runAction(loopActions[j], j);
                                                await Task.Delay(700);
                                            }

                                        }
                                        forLoop = false;
                                        startIndex = richTextBox1.GetFirstCharIndexFromLine(progPlace - 1);
                                        richTextBox1.Select(startIndex, richTextBox1.Lines[progPlace - 1].Length);
                                        richTextBox1.SelectionBackColor = System.Drawing.Color.Yellow;
                                        label3.Text = "Running: " + progItem;
                                        checkAction(progItem);
                                        await Task.Delay(700);
                                        richTextBox1.SelectionBackColor = System.Drawing.Color.Transparent;
                                    }
                                }
                                else
                                {
                                    if (up)
                                    {
                                        for (int i = startValue; i > int.Parse(condition.Split('>')[1]); i++)
                                        {
                                            for (int j = 0; j < numActions; j++)
                                            {
                                                runAction(loopActions[j], j);
                                                await Task.Delay(700);
                                            }

                                        }
                                        forLoop = false;
                                        startIndex = richTextBox1.GetFirstCharIndexFromLine(progPlace - 1);
                                        richTextBox1.Select(startIndex, richTextBox1.Lines[progPlace - 1].Length);
                                        richTextBox1.SelectionBackColor = System.Drawing.Color.Yellow;
                                        label3.Text = "Running: " + progItem;
                                        checkAction(progItem);
                                        await Task.Delay(700);
                                        richTextBox1.SelectionBackColor = System.Drawing.Color.Transparent;
                                    }
                                    else
                                    {
                                        string[] b = condition.Split('>');
                                        for (int i = startValue; i > int.Parse(condition.Split('>')[1]); i--)
                                        {
                                            for (int j = 0; j < numActions; j++)
                                            {
                                                runAction(loopActions[j], j);
                                                await Task.Delay(700);
                                            }

                                        }

                                        startIndex = richTextBox1.GetFirstCharIndexFromLine(progPlace - 1);
                                        richTextBox1.Select(startIndex, richTextBox1.Lines[progPlace - 1].Length);
                                        richTextBox1.SelectionBackColor = System.Drawing.Color.Yellow;
                                        label3.Text = "Running: " + progItem;
                                        checkAction(progItem);
                                        await Task.Delay(700);
                                        richTextBox1.SelectionBackColor = System.Drawing.Color.Transparent;
                                    }
                                }
                                forLoop = false;
                                numActions = 0;
                                Array.Clear(loopActions, 0, loopActions.Length);
                            }
                            else if (runElse) //Run else statement
                            {
                                for (int i = 0; i < numActions; i++)
                                {
                                    runAction(loopActions[i], i);
                                    await Task.Delay(700);


                                }
                                runElse = false;
                                startIndex = richTextBox1.GetFirstCharIndexFromLine(progPlace - 1);
                                richTextBox1.Select(startIndex, richTextBox1.Lines[progPlace - 1].Length);
                                richTextBox1.SelectionBackColor = System.Drawing.Color.Yellow;
                                label3.Text = "Running: " + progItem;
                                checkAction(progItem);
                                await Task.Delay(700);
                                richTextBox1.SelectionBackColor = System.Drawing.Color.Transparent;
                            }
                            
                            else if(progItem != "" && progItem != " ")
                            {
                                startIndex = richTextBox1.GetFirstCharIndexFromLine(progPlace);
                                richTextBox1.Select(startIndex, richTextBox1.Lines[progPlace].Length);
                                richTextBox1.SelectionBackColor = System.Drawing.Color.Yellow;
                                label3.Text = "Running: " + progItem;
                                checkAction(progItem);
                                await Task.Delay(700);
                            }
                        }
                        else
                        {
                            label3.Text = "Stopped due to invalid move";
                        }

                        progPlace++;
                        richTextBox1.SelectionBackColor = System.Drawing.Color.Transparent;
                    
                }

                //Reenable buttons and change display

                tableLayoutPanel1.BackColor = Color.YellowGreen;
                buttonRun.Enabled = true;
                buttonReset.Enabled = true;
                buttonStep.Enabled = true;
                richTextBox1.Enabled = true;
                label3.Text = "Done Running";
                progDone = true;
                running = false;
                progSet = false;
            }


        }

        public async void buttonStep_Click(object sender, EventArgs e)
        {

            //Disable buttons and get program
            tableLayoutPanel1.BackColor = Color.Green;
            buttonRun.Enabled = false;
            buttonReset.Enabled = false;
            richTextBox1.Enabled = false;
            buttonStep.Enabled = false;
            if (!progSet)
            {
                setProg();
            }
            string progItem;

            if (progDone)  //If program is complete
            {
                resetGrid();
                progDone = false;
            }
            else
            {
                if (progPlace < prog.Length) //Check that program isn't complete
                {
                    if ((whileLoop || forLoop || ifTrue) && !getActions) //Check if in loop or if statement
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
                        else if (getActions) //Get actions for a loop or if statement
                        {

                            label3.Text = "Getting Actions for Loop or Conditional";
                            string action = prog[progPlace];
                            int p = progPlace;
                            while (!action.Contains('}'))
                            {
                                if (action != "")
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
                                        runAction(progItem, loopPlace+progPlace+numActions);
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
                                        runAction(progItem, loopPlace + progPlace + numActions);
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
                                        runAction(progItem, loopPlace + progPlace + numActions);
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
                                            runAction(progItem, loopPlace + progPlace + numActions);
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
                                            runAction(progItem, loopPlace + progPlace + numActions);
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
                                            runAction(progItem, loopPlace + progPlace + numActions);
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
                                            runAction(progItem, loopPlace + progPlace + numActions);
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
                        else if (ifTrue) //Run if
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
                        else if (forLoop) //Run for loop
                        {

                            if (!greaterthan)
                            {
                                if (up)
                                {
                                    if (loopVariable < int.Parse(condition.Split('<')[1]))
                                    {
                                        if (loopPlace < numActions)
                                        {

                                            runAction(progItem, 0);
                                            //label3.Text = "Running: " + progItem;
                                            //checkAction(progItem);
                                            //await Task.Delay(700);
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
                        else if (runElse) //run else
                        {
                            ifHelper(progItem);
                        }
                        else if(progItem != "" && progItem != " ")
                        {
                            int startIndex = richTextBox1.GetFirstCharIndexFromLine(progPlace);
                            richTextBox1.Select(startIndex, richTextBox1.Lines[progPlace].Length);
                            richTextBox1.SelectionBackColor = System.Drawing.Color.Yellow;

                            label3.Text = "Running: " + progItem;
                            checkAction(progItem);
                            await Task.Delay(700);
                            richTextBox1.SelectionBackColor = System.Drawing.Color.Transparent;
                            progPlace++;
                        }

                    }
                    else
                    {
                        label3.Text = "Stopped due to invalid move";
                        tableLayoutPanel1.BackColor = Color.YellowGreen;
                        buttonRun.Enabled = true;
                        buttonReset.Enabled = true;
                        richTextBox1.Enabled = true;
                        loopPlace = 0;
                        loopVariable = 0;
                        progPlace = 0;
                        numActions = 0;
                        label3.Text = "Done Running";
                        Array.Clear(prog, 0, prog.Length);
                        Array.Clear(loopActions, 0, loopActions.Length);
                        progDone = true;
                        progSet = false;
                    }
                }
                else
                {

                    //Enable buttons and reset variables

                    tableLayoutPanel1.BackColor = Color.YellowGreen;
                    buttonRun.Enabled = true;
                    buttonReset.Enabled = true;
                    richTextBox1.Enabled = true;
                    loopPlace = 0;
                    loopVariable = 0;
                    progPlace = 0;
                    numActions = 0;
                    label3.Text = "Done Running";
                    Array.Clear(prog, 0, prog.Length);
                    Array.Clear(loopActions, 0, loopActions.Length);
                    progDone = true;
                    progSet = false;
                    //end of stepping
                }
            }
            buttonStep.Enabled = true;
        }

        public void buttonReset_Click(object sender, EventArgs e)
        {
            resetGrid();
        }

        //************************************* 
        // Helper functions
        //************************************
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
            if((r <= gridSize-1 && r >=0) && (c <= gridSize - 1 && c >= 0)) {
                
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

        public async void ifHelper(string progItem)
        {
            if (loopPlace < numActions)
            {
                runAction(progItem, loopPlace + progPlace + numActions);
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
            while (direction != 1)
            {
                turn();
            }



        }

        public void checkAction(string action)
        {
            if (action.Contains("move")) { move(); }
            else if (action.Contains("turnleft")) { turn(); }
            else if (action.Contains("turnright")) { turnRight(); }
            else if (action.Contains("turnaround")) { turnAround(); }
            else if (action.Contains("paint"))
            {
                if (!grid[activeRow, activeColumn].getPaint())
                {
                    grid[activeRow, activeColumn].setImage(paint);
                    grid[activeRow, activeColumn].setPaint();
                }
            }
            else if (action.Contains("erase"))
            {
                grid[activeRow, activeColumn].setImage(arrow);
                grid[activeRow, activeColumn].setPaint();
            }
        }

        public async void runAction(string action, int place) {
            int p = progPlace + place - numActions;
            int startIndex = richTextBox1.GetFirstCharIndexFromLine(p - 1);
            richTextBox1.Select(startIndex, richTextBox1.Lines[p - 1].Length);
            richTextBox1.SelectionBackColor = System.Drawing.Color.Yellow;

            label3.Text = "Running: " + action;
            checkAction(action);
            await Task.Delay(1000);
            richTextBox1.SelectionBackColor = System.Drawing.Color.Transparent;
        }

       

        //************************************* 
        // Movement functions
        //************************************
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
        public void move()
        {


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
                else
                {
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

        //************************************* 
        // Lesson 
        //************************************
        private void buttonLesson_Click(object sender, EventArgs e)
        {
            Lesson l = new Lesson(this, lesson);
            l.Show();

        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           string selectedLesson = listBox1.SelectedItems[0].ToString();
            if(selectedLesson == "Introduction")
            {
                lesson = 0;
            }
            if(selectedLesson == "Lesson 1") {
                lesson = 1;
            }
            else if(selectedLesson == "Lesson 2")
            {
                lesson = 2;
            }
            else if (selectedLesson == "Lesson 3")
            {
                lesson = 3;
            }
            else if (selectedLesson == "Lesson 4")
            {
                lesson = 4;
            }
            else if (selectedLesson == "Lesson 5")
            {
                lesson = 5;
            }
            else if (selectedLesson == "Lesson 6")
            {
                lesson = 6;
            }
            else if (selectedLesson == "Lesson 7")
            {
                lesson = 7;
            }

        }

        //************************************* 
        // Saving and loading files 
        //************************************
        private void buttonSave_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName, richTextBox1.Text);
                fileName = saveFileDialog1.FileName;
            }
           
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            fileModified = true;

        }
        private void buttonLoad_Click(object sender, EventArgs e)
        {
            string filePath;
            if (!fileModified)
            {
                openFileDialog1.InitialDirectory = "c:\\";
                openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog1.FileName;
                    richTextBox1.Text = File.ReadAllText(filePath);

                }
            }
            else
            {
                DialogResult result = MessageBox.Show(fileName, "Do you wish to save?", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                    saveFileDialog1.FilterIndex = 2;
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllText(saveFileDialog1.FileName, richTextBox1.Text);
                        fileName = saveFileDialog1.FileName;
                    }

                    fileModified = false;
                }
                else
                {
                    openFileDialog1.InitialDirectory = "c:\\";
                    openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                    openFileDialog1.FilterIndex = 2;
                    openFileDialog1.RestoreDirectory = true;

                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        //Get the path of specified file
                        filePath = openFileDialog1.FileName;
                        richTextBox1.Text = File.ReadAllText(filePath);
                    }
                }
            }

        }

        //************************************* 
        // Enabling and disabling buttons on focus change
        //************************************
        private void main_Activate(object sender, EventArgs e)
        {
            buttonRun.Enabled = true;
            buttonReset.Enabled = true;
            buttonStep.Enabled = true;
            richTextBox1.Enabled = true;
        }
        private void main_Deactivate(object sender, EventArgs e)
        {
            buttonRun.Enabled = false;
            buttonReset.Enabled = false;
            buttonStep.Enabled = false;
            richTextBox1.Enabled = false;
        }

        

        
    }
}
