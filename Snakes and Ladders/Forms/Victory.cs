using System;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System.IO;
using static SnakesAndLadders.Components;
using static SnakesAndLadders.GameData;
using static SnakesAndLadders.HighScoreClass;

namespace SnakesAndLadders
{
    public partial class Victory : BaseForm
    {
        public Victory() => InitializeComponent();

        PictureBox Trophy { get; set; }
        PictureBox RecordTrophy { get; set; }
        Label heading { get; set; }
        Button Exit { get; set; }
        Button Return { get; set; }

        private void Victory_Load(object sender, EventArgs e)
        {
            //Visuals 
            WindowState = FormWindowState.Normal;

            Trophy = PicBox("trophy.png", Height, 3 * (Height / 4));
            Trophy.Location = new Point(FormCentre(this).X - Trophy.Width / 2, 0);

            RecordTrophy = PicBox("trophy2.png", Height, 3 * (Height / 4));
            RecordTrophy.Location = new Point(FormCentre(this).X - RecordTrophy.Width / 2, 0);
            RecordTrophy.Visible = false;
            
            UserVictory = NumberOfTurns[playerCounter];
            CheckHighScore();

            heading = Heading(this, "You took " + UserVictory + " turns!", 0, 40);
            heading.Location = new Point(0, Trophy.Height);
            heading.BackColor = Color.Transparent;


            if (record == true)
            {
                RecordTrophy.Visible = true;
                RecordTrophy.BringToFront();
            }

            Exit = ExitButton(this);
            Exit.Click += new EventHandler(_DeleteGame);
            Return = ReturnButton(this);
            Return.Click += new EventHandler(_DeleteGame);

            ToolTip toolTip = Tip();
            toolTip.SetToolTip(Return, "Return to start");
            toolTip.SetToolTip(Exit, "Close");

            Controls.AddRange(new Control[]
            {
                Exit, 
                Return,
                heading,
                RecordTrophy,
                Trophy,
            });
        }
        void _DeleteGame (object sender, EventArgs e)
        {
            var newLines = IncompleteGames.Where(line => !line.Contains(GameName));
            File.WriteAllLines(SavedGamesFile, newLines);
            File.Delete(GameName + ".txt");
        }
    }
}
