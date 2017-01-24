using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace IMS_System.Class.GUI
{
    class clsLines
    {
        public static void Draw_Horizontal_lines(PaintEventArgs e,Color PenColor,Label LabelName)
        {
            Graphics g;
            g = e.Graphics;
            Pen myPen = new Pen(PenColor);
            myPen.Width = 1;
            g.DrawLine(myPen, 5, LabelName.Height - 5, LabelName.Width-5, LabelName.Height - 5);
        }

        public static void Draw_Horizontal_full_lines(PaintEventArgs e, Color PenColor, Label LabelName)
        {
            Graphics g;
            g = e.Graphics;
            Pen myPen = new Pen(PenColor);
            myPen.Width = 1;
            g.DrawLine(myPen,0, 0, LabelName.Width+25, 0);
        }

        public static void Draw_Vertical_full_lines(PaintEventArgs e, Color PenColor,int left, int height)
        {
            Graphics g;
            g = e.Graphics;
            Pen myPen = new Pen(PenColor);
            myPen.Width = 1;
            g.DrawLine(myPen, left-1, height + 25, left-1, 0);
        }
    }
}
