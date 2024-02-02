using System;
using System.Windows.Forms;
using static SnakesAndLadders.BoardClass;
using static SnakesAndLadders.Components;

namespace SnakesAndLadders
{
    public partial class SelectBoard : BaseForm
    {
        public SelectBoard() => InitializeComponent();
        private void SelectBoard_Load(object sender, EventArgs e)
        {
            SetGameName sgn = new SetGameName();
            int gap = Width / 10;


            //Visuals

            WindowState = FormWindowState.Normal;
            Setup(this, Heading(this, "BOARD SIZE", Height / 4, 25));
            Controls.AddRange(new Control[]
            {
                BigButton(this,"10x10", FormCentre(this).X - gap, FormCentre(this).Y, (sender2, ee) =>
                {
                    SelectedBoard = "10x10"; //Sets board size
                    Hide();
                    sgn.ShowDialog();
                }),
                BigButton(this, "7x7", gap, FormCentre(this).Y, (sender2, ee) =>
                {
                    SelectedBoard = "7x7";
                    Hide();
                    sgn.ShowDialog();
                }),
                BigButton(this, "15x15", Width - Width/5 - gap, FormCentre(this).Y, (sender2, ee) =>
                {
                    SelectedBoard = "15x15";
                    Hide();
                    sgn.ShowDialog();
                }),
            });
        }
    }
}
