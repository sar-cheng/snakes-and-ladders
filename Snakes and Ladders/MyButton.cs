using System;
using System.Windows.Forms;
using System.Drawing;

namespace SnakesAndLadders
{
    class MyButton : Button
    {
        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            FlatAppearance.BorderSize = 0;
            FlatAppearance.MouseOverBackColor = Color.Transparent;
            TextAlign = ContentAlignment.MiddleCenter;
        }
        protected override void OnMouseEnter (EventArgs e)
        {
            base.OnMouseEnter(e);
            ForeColor = Color.White;
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            ForeColor = Color.Black;
        }
        protected override void OnMouseMove(MouseEventArgs mevent)
        {
            base.OnMouseMove(mevent);
            Cursor = Cursors.Hand;
        }
    }
}
