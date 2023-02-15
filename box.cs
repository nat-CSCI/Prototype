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
    public partial class box : Form
    {
        PictureBox p;
        Image image;
        int row, col;
        bool blocked;
        bool painted;
        Image paint = Image.FromFile("C:\\Users\\natdy\\OneDrive\\Desktop\\Prototype\\black.png");
        Image blank = Image.FromFile("C:\\Users\\natdy\\OneDrive\\Desktop\\Prototype\\white.png");
       public box()
        {
            image = blank;
            row = 0;
            col = 0;
            blocked = false;
            painted = false;

        }

       public box(PictureBox x, int r, int c)
        {
            p = x;
            image = blank;
            row = r;
            col = c;
            blocked = false;
            painted = false;

        }

        public void setPictureBox(PictureBox x) { p = x; }
        public PictureBox getPictureBox() { return p; }

        public void setImage(Image s)
        {
            p.Image = s;
        }

        public Image getImage()
        {
            return image;
        }

        public bool getPaint()
        {
            return painted;
        }

        public void setPaint()
        {
            if (painted)
            {
                painted = false;
            }
            else { painted = true; }
        }

        public int getRow() { return row; }
        public int getCol() { return col; }
        public void setRow(int r) { row = r; }
        public void setCol(int c) { col = c; }
        public void setBlocked()
        {
            if (blocked)
            {
                blocked = false;
            }
            else
            {
                blocked = true;
            }
        }
        public bool getBlocked() { return blocked; }
    }
}
