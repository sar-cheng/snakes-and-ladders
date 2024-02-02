using System;
using System.Linq;
using System.Windows.Forms;
using static SnakesAndLadders.Components;
using static SnakesAndLadders.GameData;

namespace SnakesAndLadders
{
    public partial class SetGameName : BaseForm
    {
        public SetGameName() => InitializeComponent();

        TextBox SetName { get; set; }
        Button CreateGame { get; set; }
        
        private void SetGameName_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;

            Setup(this, null);

            SetName = TxtBox("SET A NAME FOR THE GAME", 0, Height / 4, Width, Height / 4, 20);
            CreateGame = BigButton(this, "CREATE GAME", FormCentre(this).X - Width / 10, FormCentre(this).Y, (sender2, ee) =>
            {
                string AllowedChar = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 ";
                
                try
                {
                    if (SetName.Text == "SET A NAME FOR THE GAME")
                    {
                        MessageBox.Show("PLEASE SET A NAME FOR THE GAME");
                    }
                    else if (!SetName.Text.All(AllowedChar.Contains)) //Invalid name => certain characters
                    {
                        MessageBox.Show("THE NAME CAN ONLY CONTAIN ALPHANUMERICAL CHARACTERS");
                    }
                    else
                    {
                        GameName = SetName.Text; //Sets game name
                        GameFile = SetName.Text + ".txt"; //Sets file name for this game's data
                        Hide();
                        Game game = new Game();
                        game.ShowDialog();
                    }
                }
                catch (System.IO.PathTooLongException)
                {
                    MessageBox.Show("The name is too long!");
                }
            });

            Controls.AddRange(new Control[]
            {
                SetName,
                CreateGame,
            });
        }
    }
}
