using System;
using System.Windows.Forms;
using System.Drawing;

namespace SnakesAndLadders
{
    class FormDimensions
    {
        public static int ScreenWidth = Screen.PrimaryScreen.WorkingArea.Width;
        public static int ScreenHeight = Screen.PrimaryScreen.WorkingArea.Height;
        public static Point FormCentre(Form form)
        {
            Point centre = new Point(form.Width/ 2, form.Height/ 2);
            return centre;
        }
    }
}
