using System;
using System.Drawing;
using System.Windows.Forms;
using static SnakesAndLadders.HighScoreClass;
using static SnakesAndLadders.Components;

namespace SnakesAndLadders
{
    public partial class HighScores : BaseForm
    {
        public HighScores() => InitializeComponent();
        
        Panel Panel7 { get; set; }
        Panel Panel10 { get; set; }
        Panel Panel15 { get; set; }
        string Score7 { get; set; } = GetHighScore("!HIGH SCORE 7x7!.txt");
        string Score10 { get; set; } = GetHighScore("!HIGH SCORE 10x10!.txt");
        string Score15 { get; set; } = GetHighScore("!HIGH SCORE 15x15!.txt");
        Label Label7 { get; set; }
        Label Label10 { get; set; }
        Label Label15 { get; set; }
        Label heading { get; set; }
        private void HighScores_Load(object sender, EventArgs e)
        {
            //Visuals
            
            WindowState = FormWindowState.Normal;

            Setup(this, null);

            Panel7 = panel("hs7.png", 0, Height / 4, Width / 3, Width / 3);
            Panel10 = panel("hs10.png", Panel7.Width, Panel7.Location.Y, Width / 3, Width / 3);
            Panel15 = panel("hs15.png", Panel10.Location.X + Panel10.Width, Panel10.Location.Y, Width / 3, Width / 3);

            Label7 = label(Score7, Panel7.Width / 3, Panel7.Height / 2 + Panel7.Height/4, 20);
            Label10 = label(Score10, Panel7.Width / 3, Panel7.Height / 2 + Panel7.Height / 4, 20);
            Label15 = label(Score15, Panel7.Width / 3, Panel7.Height / 2 + Panel7.Height / 4, 20);

            Panel7.Controls.Add(Label7);
            Panel10.Controls.Add(Label10);
            Panel15.Controls.Add(Label15);

            heading = Heading(this, "HIGH SCORES", Height / 2 - Height / 10, 20);
            heading.BackColor = Color.Transparent;

            ToolTip toolTip = Tip();
            toolTip.SetToolTip(heading, "Least number of turns taken to complete the game");
            
            Controls.AddRange(new Control[]
            {
                heading,
                Panel7,
                Panel10,
                Panel15,
            });
        }
    }
}
