using System;
using System.Drawing;
using System.Windows.Forms;
using static SnakesAndLadders.Components;

namespace SnakesAndLadders
{
    public partial class StartMenu : BaseForm
    {
        public StartMenu() => InitializeComponent();

        PictureBox Header { get; set; }
        Button Help { get; set; }
        Button NewGame { get; set; }
        Button ViewGames { get; set; }
        Button HighScores { get; set; }
        Button Minimise { get; set; }
        Button Exit { get; set; }

        private void StartMenu_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            //Instantiation

            HowToPlay howtoplay = new HowToPlay();
            SelectPlayers selectplayers = new SelectPlayers();
            HighScores highscores = new HighScores();
            ViewGames viewgame = new ViewGames();

            
            Header = PicBox("heading.png", Height, 3*(Height/4));
            Header.Location = new Point(FormCentre(this).X - Header.Width / 2, 0);
            

            //Calculations

            int gap = Width / 20;
            int underheader = Header.Location.Y + Header.Height;


            //Buttons

            Help = BigButton(this, "How To Play", gap/2, underheader, (sender2, ee) =>
            {
                Hide();
                howtoplay.ShowDialog();
            });
            NewGame = BigButton(this, "New Game", Help.Location.X + Help.Width + gap, underheader, (sender2, ee) =>
            {
                Hide();
                selectplayers.ShowDialog() ;
            });
            ViewGames = BigButton(this, "View Saved Games", NewGame.Location.X + NewGame.Width + gap, underheader, (sender2, ee) =>
            {
                Hide();
                viewgame.ShowDialog();
            });
            HighScores = BigButton(this, "High Scores", ViewGames.Location.X + ViewGames.Width + gap, underheader, (sender2, ee) =>
            {
                Hide();
                highscores.ShowDialog();
            });

            Exit = ExitButton(this);
            Minimise = MinimiseButton(this);
            Minimise.Location = new Point(Width - 2 * Exit.Width);

            ToolTip toolTip = Tip();
            toolTip.SetToolTip(Exit, "Close");
            toolTip.SetToolTip(Minimise, "Minimise");

            Controls.AddRange(new Control[]
            {
                Header,
                Exit,
                Minimise,
                Help,
                NewGame,
                ViewGames,
                HighScores,
            });
        }

    }
}
