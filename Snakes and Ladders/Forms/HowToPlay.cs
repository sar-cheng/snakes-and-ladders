using System;
using System.Windows.Forms;
using System.Drawing;
using static SnakesAndLadders.Components;

namespace SnakesAndLadders
{
    public partial class HowToPlay : BaseForm
    {
        public HowToPlay() => InitializeComponent();
        
        PictureBox Instructions { get; set; }

        private void HowToPlay_Load(object sender, EventArgs e)
        {
            //Visuals
            
            WindowState = FormWindowState.Normal;

            Setup(this, null);
            Instructions = PicBox("HowToPlay.png", Height, Height);
            Instructions.Location = new Point(FormCentre(this).X - Height / 2);

            Controls.Add(Instructions);
        }
    }
}
