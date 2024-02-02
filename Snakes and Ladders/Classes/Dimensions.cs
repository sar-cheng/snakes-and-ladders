using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Snakes_and_Ladders
{
    class Dimensions
    {
        public static int ScreenWidth { get; set; } = Screen.PrimaryScreen.WorkingArea.Width;
        public static int ScreenHeight { get; set; } = Screen.PrimaryScreen.WorkingArea.Height;
        //public static int ScreenBorder { get; set; } = Screen.PrimaryScreen.Bounds.X;
        public static Point FormCentre(Form form)
        {
            Point centre = new Point(form.Width/ 2, form.Height/ 2);
            return centre;
        }
        public static int BoardSquare;
        public static PictureBox DefaultBoard(Form form) => new PictureBox
        {
            Size = new Size(form.Height, form.Height),
            SizeMode = PictureBoxSizeMode.Zoom,
            Location = new Point(FormCentre(form).X - form.Height / 2, 0)
        };
        
    }
}
