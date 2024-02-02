using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using static SnakesAndLadders.Components;
using static SnakesAndLadders.GameData;
using static SnakesAndLadders.BoardClass;

namespace SnakesAndLadders
{
    public partial class NewGame : Form
    {
        public NewGame() => InitializeComponent();
        private void NewGame_Load(object sender, EventArgs e)
        {
            DefaultFormProperties(this);
            CenterToScreen();

            if (ResumeSelect == true)
            {
                SelectGame();
            }
            else
            {
                SelectPlayers();
            }
        }
        Control[] Setup(Label header)
        {
            var Exit = ExitButton(this);
            var Return = ReturnButton(this);
            var Minimise = MinimiseButton(this);
            Control[] setUp = new Control[]
            {
                Exit,
                Return,
                Minimise,
                header,
            };

            ToolTip toolTip = Tip();
            toolTip.SetToolTip(Exit, "Close");
            toolTip.SetToolTip(Return, "Return to start");
            toolTip.SetToolTip(Minimise, "Minimise");

            Controls.AddRange(setUp);
            return setUp;
        }
        void SelectGame()
        {
            Setup(Heading(this, "SAVED GAMES", Height / 4, 25));

            string[] SavedGames = File.ReadAllLines(SavedGamesFile);

            ListBox gamesBox = listbox(0, Height / 2, Width, Height / 4, 20);
            gamesBox.Items.AddRange(SavedGames);

            Button Resume = CustomButton("RESUME", 0, 0);
            Resume.Location = new Point(0, gamesBox.Location.Y - Resume.Height);
            Resume.Click += new EventHandler(Resume_Click);

            Button Delete = CustomButton("DELETE", 0, 0);
            Delete.Location = new Point(Resume.Width, Resume.Location.Y);
            Delete.Click += new EventHandler(Delete_Click);

            Controls.AddRange(new Control[]
            {
                Resume,
                gamesBox,
                Delete,
            });

            void Resume_Click (object sender, EventArgs e)
            {
                try
                {
                    GameFile = gamesBox.SelectedItem.ToString() + ".txt";
                    Hide();
                    Game game = new Game();
                    game.ShowDialog();
                }
                catch (NullReferenceException) { }
            }
            void Delete_Click (object sender, EventArgs e)
            {
                try
                {
                    //remove from textfile
                    var newLines = SavedGames.Where(line => !line.Contains(gamesBox.SelectedItem.ToString()));
                    File.WriteAllLines(SavedGamesFile, newLines);
                    File.Delete(gamesBox.SelectedItem.ToString() + ".txt");

                    gamesBox.Items.Remove(gamesBox.SelectedItem.ToString());
                }
                catch (NullReferenceException) { }
            }
        }
        void SelectPlayers()
        {
            Setup(Heading(this, "SELECT NUMBER OF PLAYERS", Height / 4, 25));

            //Create controls
            int position = FormCentre(this).X - Width / 10 - 1;
            int i = 0;
            Button[] PlayerNumber = new Button[6];
            while (i < 6)
            {
                PlayerNumber[i] = BigButton(this, Convert.ToString(i+2), position, 0, (sender2, e3) => { });
                Controls.Add(PlayerNumber[i]);
                position = position - Width / 5;
                i++;
            }

            //Press button to run the next module
            foreach (Button PN in PlayerNumber)
            {
                PN.Click += (sender, e) =>
                {
                    Controls.Clear();
                    ChooseColour(Convert.ToInt32(PN.Text));
                };
            }
        }
        void ChooseColour(int PlayerLimit)
        {
            SelectedColours.Clear(); //ensuring it starts from being empty
            Label Select = Heading(this, "SELECT PLAYER 1 COLOUR", Height / 4, 25);
            Setup(Select);

            PictureBox[] PlayerColourStorage = new PictureBox[6]
            {
                PicBox("Red.png", Width/8, Height/8),
                PicBox("Blue.png", Width/8, Height/8),
                PicBox("Green.png", Width/8, Height/8),
                PicBox("Orange.png", Width/8, Height/8),
                PicBox("Yellow.png", Width/8, Height/8),
                PicBox("Pink.png", Width/8, Height/8),
            };

            int StartingLimit = 0;
            int Position = 1;

            foreach (PictureBox PlayerColour in PlayerColourStorage)
            {
                PlayerColour.Location = new Point(Position, FormCentre(this).Y);
                Position = Position + Width / 6;
                PlayerColour.Click += (sender, e) =>
                {
                    SelectedColours.Add(PlayerColour.Name);

                    if (StartingLimit == PlayerLimit-1)
                    {
                        SelectBoardSize();
                    }

                    //highlight selected player
                    PlayerColour.BorderStyle = BorderStyle.FixedSingle;
                    StartingLimit++;

                    Select.Text = "SELECT PLAYER " + (Convert.ToString(StartingLimit + 1)) + " COLOUR";
                    Select.Refresh();
                };
                Controls.Add(PlayerColour);
            }
        }
        void SelectBoardSize()
        {
            Controls.Clear();
            Setup(Heading(this, "BOARD SIZE", Height / 4, 25));
            Controls.AddRange(new Control[]
            {
                BigButton(this,"10x10", 0, 0, (sender, e) =>
                {
                    SelectedBoard = "10x10";
                    SetGameName();
                }),
                BigButton(this, "7x7", Width/5, 0, (sender, e) =>
                {
                    SelectedBoard = "7x7";
                    SetGameName();
                }),
                BigButton(this, "15x15", -Width/5, 0, (sender, e) => 
                {
                    SelectedBoard = "15x15";
                    SetGameName();
                }),
            });
        }
        void SetGameName()
        {
            Controls.Clear();
            Setup(null);

            TextBox SetName = TxtBox("SET A NAME FOR THE GAME", 0, Height/4, Width, Height/4, 20);
            
            Controls.AddRange(new Control[]
            {
                SetName,
                BigButton(this, "CREATE GAME", 0, 0, (sender, e) =>
                {
                    string AllowedChar = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 ";
                    //invalid name (certain characters)
                    if (SetName.Text == "SET A NAME FOR THE GAME")
                    {
                        MessageBox.Show("PLEASE SET A NAME FOR THE GAME");
                    }
                    else if (!SetName.Text.All(AllowedChar.Contains))
                    {
                        MessageBox.Show("THE NAME CAN ONLY CONTAIN ALPHANUMERICAL CHARACTERS");
                    }
                    else
                    {
                        GameName = SetName.Text;
                        GameFile = SetName.Text + ".txt";
                        Hide();
                        Game game = new Game();
                        game.ShowDialog();
                    }
                }),
            });
        }
        
    }
}
