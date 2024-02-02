using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using static SnakesAndLadders.Components;
using static SnakesAndLadders.GameData;

namespace SnakesAndLadders
{
    public partial class ViewGames : BaseForm
    {
        public ViewGames() => InitializeComponent();

        ListBox Incomplete { get; set; }
        Button ResumeButton { get; set; }
        Button DeleteButton { get; set; }

        private void ViewGame_Load(object sender, EventArgs e)
        {
            //Visuals
            
            WindowState = FormWindowState.Normal;
            Setup(this, Heading(this, "GAMES IN PROGRESS", Height / 4, 25));

            Incomplete = listbox(0, Height / 2, Width, Height / 4);
            Incomplete.Items.AddRange(IncompleteGames);

            ResumeButton = CustomButton("RESUME", 0, 0);
            ResumeButton.Location = new Point(0, Incomplete.Location.Y - ResumeButton.Height - ResumeButton.Height/2);
            ResumeButton.Click += new EventHandler(Resume_Click);

            DeleteButton = CustomButton("DELETE", ResumeButton.Width, ResumeButton.Location.Y);
            DeleteButton.Click += new EventHandler(Delete_Click);

            Controls.AddRange(new Control[]
            {
                ResumeButton,
                DeleteButton,
                Incomplete,
            });

        }
        void Resume_Click(object sender, EventArgs e)
        {
            try
            {
                GameFile = Incomplete.SelectedItem.ToString() + ".txt";
                GameName = Incomplete.SelectedItem.ToString();
                ResumeSelect = true;
                Hide();
                Game game = new Game();
                game.ShowDialog();
            }
            catch (NullReferenceException) { }
        }
        void Delete_Click(object sender, EventArgs e) //CHECK - only deletes once? slow?
        {
            try
            {
                //Remove from textfile
                var newLines = IncompleteGames.Where(line => !line.Contains(Incomplete.SelectedItem.ToString()));
                File.WriteAllLines(SavedGamesFile, newLines);
                File.Delete(Incomplete.SelectedItem.ToString() + ".txt");
                
                //Then remove from ListBox
                Incomplete.Items.Remove(Incomplete.SelectedItem.ToString());
            }
            catch (NullReferenceException) { }
        }
    }
}
