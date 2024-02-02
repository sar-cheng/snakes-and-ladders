using System;
using System.Windows.Forms;
using static SnakesAndLadders.Components;
using static SnakesAndLadders.GameData;

namespace SnakesAndLadders
{
    public partial class SelectPlayers : BaseForm
    {
        public SelectPlayers() => InitializeComponent();
        Button[] Numbers { get; set; } = new Button[5];
        private void NewGame_Load(object sender, EventArgs e)
        {
            //Visuals

            WindowState = FormWindowState.Normal;
            Setup(this, Heading(this, "SELECT NUMBER OF PLAYERS", Height / 4, 25));

            int PositionX = Width / 5 + 1;
            int i = 0;
            while (i < 5)
            {
                Numbers[i] = BigButton(this, Convert.ToString(i + 2), i * PositionX, FormCentre(this).Y, null);
                Controls.Add(Numbers[i]);
                i++;
            }


            //Set button.click event

            foreach (Button number in Numbers)
            {
                number.Click += (sender2, ee) =>
                {
                    //Limits number of players for choosing colours
                    PlayerLimit = Convert.ToInt32(number.Text);
                    Hide();
                    ChooseColours cc = new ChooseColours();
                    cc.ShowDialog();
                };
            }
        }
    }
}
