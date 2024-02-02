using System;
using System.Drawing;
using System.Windows.Forms;
using static SnakesAndLadders.Components;

namespace SnakesAndLadders
{
    public partial class BaseForm : Form
    {
        public BaseForm() => InitializeComponent();

        private void BaseForm_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            Icon = new Icon("icon.ico");
            Size = new Size(ScreenWidth / 2, ScreenHeight / 2);
            FormBorderStyle = FormBorderStyle.None;
            BackColor = Color.White;
            BackgroundImage = Image.FromFile("bg.png");
            BackgroundImageLayout = ImageLayout.Stretch;
            CenterToScreen();
        }
    }
}
