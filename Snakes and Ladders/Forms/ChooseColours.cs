using System;
using System.Drawing;
using System.Windows.Forms;
using static SnakesAndLadders.Components;
using static SnakesAndLadders.GameData;

namespace SnakesAndLadders
{
    public partial class ChooseColours : BaseForm
    {
        public ChooseColours() => InitializeComponent();
        
        Label Header { get; set; }

        private void ChooseColours_Load(object sender, EventArgs e)
        {
            int W = Width / 6;
            int StartingLimit = 1;


            //Visuals
            
            WindowState = FormWindowState.Normal;

            Header = Heading(this, "SELECT PLAYER 1 COLOUR", Height / 4, 25);
            Setup(this, Header);

            PictureBox[] PlayerColourStorage = new PictureBox[6]
            {
                PicBox("Red.png", W, W),
                PicBox("Blue.png", W, W),
                PicBox("Green.png", W, W),
                PicBox("Orange.png", W, W),
                PicBox("Yellow.png", W, W),
                PicBox("Pink.png", W, W),
            };

            int i = 0;
            foreach (PictureBox PlayerColour in PlayerColourStorage)
            {
                //Position each PictureBox
                PlayerColour.Location = new Point(i * W, FormCentre(this).Y);
                i++;

                PlayerColour.Click += (sender2, ee) =>
                {
                    //Saves selected colour
                    SelectedColours.Add(PlayerColour.Name);

                    //Restricts user to select within the number of players chosen
                    if (StartingLimit == PlayerLimit)
                    {
                        Hide();
                        SelectBoard sb = new SelectBoard();
                        sb.ShowDialog();
                    }

                    //Highlights selected player
                    PlayerColour.BorderStyle = BorderStyle.FixedSingle;
                    PlayerColour.Enabled = false;
                    StartingLimit++;

                    //Changes header description
                    Header.Text = "SELECT PLAYER " + Convert.ToString(StartingLimit) + " COLOUR";
                };
                Controls.Add(PlayerColour);
            }
        }
    }
}
