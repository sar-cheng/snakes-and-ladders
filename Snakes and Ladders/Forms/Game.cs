using System; 
using System.Drawing; 
using System.Threading.Tasks; 
using System.Windows.Forms; 
using System.IO;
using System.Collections.Generic; 
using System.Linq;
using static SnakesAndLadders.Components;
using static SnakesAndLadders.GameData;
using static SnakesAndLadders.HighScoreClass;
using static SnakesAndLadders.BoardClass;

namespace SnakesAndLadders
{
    public partial class Game : BaseForm
    {
        public Game() => InitializeComponent();

        dynamic Grid { get; set; }
        string lineBreak { get; set; } = "";
        Button RollButton { get; set; }
        Button Exit { get; set; }
        Button Return { get; set; }
        Button Minimise { get; set; }
        Panel LeftBG { get; set; }
        Panel RightBG { get; set; }
        PictureBox PlayerArrow { get; set; }
        List<PictureBox> PlayerTurns { get; set; } = new List<PictureBox>();
        
        private void Game_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            WindowState = FormWindowState.Maximized;


            //Background design

            LeftBG = panel("bg.png", 0, 0, Width / 2 - Height / 2, Height);
            RightBG = panel("bg.png", FormCentre(this).X + Height/2, 0, Width / 2 - Height / 2, Height);


            //Game set-up

            if (ResumeSelect == true) //Chosen to resume a game
            {
                ResumeGame();
            }
            else //New game
            {
                LoadBoard();
                LoadPlayers();
                SetPlayerParent();
            }


            //Visuals

            PlayerArrow = PicBox("arrow.png", BoardSquare, BoardSquare);
            LeftBG.Controls.Add(PlayerArrow);


            //Game set-up continued
            
            LoadDice();
            IndicateTurn();
            HighScore = GetHighScore(ScoreFile); //from highscore text file


            //Buttons

            RollButton = CustomButton("Roll", DiceStorage[0].Location.X, DiceStorage[0].Location.Y - BoardSquare);
            RollButton.Width = DiceStorage[0].Width;
            RollButton.Click += new EventHandler(RollButton_Click);

            Exit = ExitButton(this);
            Exit.Location = new Point(RightBG.Width - Exit.Width, 0);
            Exit.Click += new EventHandler(_SaveGame);

            Return = ReturnButton(this);
            Return.Location = new Point(LeftBG.Width - 2 * Exit.Width, 0);
            Return.Click += new EventHandler(_SaveGame);

            Minimise = MinimiseButton(this);
            Minimise.Location = new Point(LeftBG.Width - 3 * Exit.Width, 0);

            ToolTip toolTip = Tip();
            toolTip.SetToolTip(Return, "Return and save");
            toolTip.SetToolTip(Exit, "Close and save");
            toolTip.SetToolTip(Minimise, "Minimise");

            RightBG.Controls.AddRange(new Control[]
            {
                Return, 
                Exit, 
                Minimise,
                RollButton,
            });

            Controls.AddRange(new Control[]
            {
                LeftBG,
                RightBG,   
            });
        }
        void LoadDice()
        {
            foreach (PictureBox DiceImage in DiceStorage)
            {
                DiceImage.Size = new Size(DefaultBoard(this).Location.X / 2, DefaultBoard(this).Location.X / 2);
                DiceImage.Visible = false;
                DiceImage.Location = new Point(BoardSquare, Height - BoardSquare - DiceImage.Height);
            };
            RightBG.Controls.AddRange(DiceStorage);
        }
        void LoadBoard()
        {
            switch (SelectedBoard)
            {
                case "7x7":
                    BackgroundImage = Image.FromFile("7x7.png");
                    BackgroundImageLayout = ImageLayout.Zoom; 
                    BoardSquare = Height / 7; //Width of one square on the board
                    Square1 = new Point(DefaultBoard(this).Location.X, Height - BoardSquare);
                    ScoreFile = "!HIGH SCORE 7x7!.txt"; 
                    Grid = BoardGrid(7); //Creates 2D array of positions for board
                    Finish = (6, 6); //Finishing square
                    CheckPosArray = 0; //Array index for accessing the board's snakesandladders array
                    break;
                case "10x10":
                    BackgroundImage = Image.FromFile("10x10.png");
                    BackgroundImageLayout = ImageLayout.Zoom;
                    BoardSquare = Height / 10;
                    Square1 = new Point(DefaultBoard(this).Location.X, Height - BoardSquare);
                    ScoreFile = "!HIGH SCORE 10x10!.txt";
                    Grid = BoardGrid(10);
                    Finish = (9, 0);
                    CheckPosArray = 2;
                    break;
                case "15x15":
                    BackgroundImage = Image.FromFile("15x15.png");
                    BackgroundImageLayout = ImageLayout.Zoom;
                    BoardSquare = Height / 15;
                    Square1 = new Point(DefaultBoard(this).Location.X, Height - BoardSquare);
                    ScoreFile = "!HIGH SCORE 15x15!.txt";
                    Grid = BoardGrid(15);
                    Finish = (14, 14);
                    CheckPosArray = 4;
                    break;
            }
        }
        void LoadPlayers()
        {
            int i = 0;
            while (i < SelectedColours.Count)
            {
                //Load players beside the board
                PlayerList.Add(PicBox(SelectedColours[i], BoardSquare / 2, BoardSquare / 2));
                PlayerList[i].Location = new Point(DefaultBoard(this).Location.X - BoardSquare - i * BoardSquare / 2, Height - BoardSquare);
                //List of players down the side
                PlayerTurns.Add(PicBox(SelectedColours[i], BoardSquare, BoardSquare));
                PlayerTurns[i].Location = new Point(0, i * BoardSquare);
                Controls.Add(PlayerList[i]);
                LeftBG.Controls.Add(PlayerTurns[i]);
                NumberOfTurns.Add(0);
                i++;
            }
        }
        async void RollButton_Click(object sender, EventArgs e)
        {
            RollButton.Enabled = false;
            Random random = new Random();
            int Roll = random.Next(1, 7);
            for (int i = 0; i < 6; i++)
            {
                DiceStorage[i].Visible = true;
                await Task.Delay(80);
                DiceStorage[i].Visible = false;
            }
            DiceStorage[Roll - 1].Visible = true;
            MovePlayer(Roll);
        }
        void SetPlayerParent() //Set parent of each player PictureBox; for visuals
        {
            foreach (PictureBox player in PlayerList)
            {
                (int X, int Y) LocIndex = GetPlayerPosition();
                playerCounter++;
                if (LocIndex == (0, -1)) //If player is outside the board (at start)
                {
                    player.Parent = LeftBG;
                }
                else
                {
                    player.Parent = this;
                }
            }
            //reset
            playerCounter = 0;
        }
        void IndicateTurn()
        {
            if (playerCounter == PlayerList.Count) //If the last player in the line just had a go
            {
                playerCounter = 0;
            }
            PlayerArrow.Location = new Point(BoardSquare, PlayerTurns[playerCounter].Location.Y);
        }
        (int X, int Y) GetPlayerPosition()
        {
            (int X, int Y) LocIndex = (0, -1);

            for (int i = 0; i < Grid.GetLength(0); i++)
            {
                for (int j = 0; j < Grid.GetLength(1); j++)
                {
                    if (PlayerList[playerCounter].Location == Grid[i, j])
                    {
                        LocIndex = (i, j);
                    }
                }
            }
            return LocIndex;
        }
        async void MovePlayer(int Roll)
        {
            int LimitX = Grid.GetLength(0) - 1;
            int LimitY = Grid.GetLength(1) - 1;
            (int X, int Y) LocIndex = GetPlayerPosition();

            PlayerList[playerCounter].Parent = this;

            while (Roll > 0)
            {
                if (LocIndex.X % 2 == 0) //row number is even
                {
                    if (LocIndex.X == LimitX && LocIndex.Y == LimitY)
                    {
                        int exceedTotal = LocIndex.Y + Roll;
                        LocIndex.Y = LimitY - (exceedTotal - LimitY);
                        Roll = 0;
                    }
                    else if (LocIndex.Y == LimitY) 
                    {
                        LocIndex.X++; //go up by 1
                        LocIndex.Y = LocIndex.Y - Roll + 1; //go to the left by remaining number, to the right (subtracted from going up) by 1
                        Roll = 0; //breaks out of loop
                    }
                    else
                    {
                        LocIndex.Y++;
                        Roll--;
                    }
                }
                else
                {
                    if (LocIndex.X == LimitX && LocIndex.Y == 0)
                    {
                        int exceedTotal = LocIndex.Y - Roll;
                        LocIndex.Y = -exceedTotal;
                        Roll = 0;
                    }
                    else if (LocIndex.Y == 0) 
                    {
                        LocIndex.X++;
                        LocIndex.Y = Roll - 1;
                        Roll = 0;
                    }
                    else
                    {
                        LocIndex.Y--;
                        Roll--;
                    }
                }
            }
            
            PlayerList[playerCounter].Location = Grid[LocIndex.X, LocIndex.Y];
            PlayerList[playerCounter].BringToFront();

            await CheckSnakesLadders();
            
            //Increases the number of turns the player has had
            NumberOfTurns[playerCounter]++;

            if (LocIndex == Finish) //Win
            {
                await Win();
            }
            else //Continue
            {
                playerCounter++;
                RollButton.Enabled = true;
                IndicateTurn();
            }
        }
        async Task CheckSnakesLadders()
        {
            (int X, int Y) LocIndex = GetPlayerPosition();
            (int, int)[] ChangePos = SnakesLadders[CheckPosArray];
            (int, int)[] FinishPos = SnakesLadders[CheckPosArray + 1];

            for (int i = 0; i < ChangePos.Length; i++)
            {
                if (LocIndex == ChangePos[i])
                {
                    LocIndex = FinishPos[i];
                    await Task.Delay(1000);
                    PlayerList[playerCounter].Location = Grid[LocIndex.X, LocIndex.Y];
                }
            }
        }
        async Task Win()
        {
            await Task.Delay(1000);
            Victory victory = new Victory();
            victory.ShowDialog();
        }
        void _SaveGame(object sender, EventArgs e)
        {
            using (StreamWriter sw = new StreamWriter(GameFile))
            {
                sw.WriteLine(SelectedBoard);
                sw.WriteLine(lineBreak);
                foreach (string colour in SelectedColours)
                {
                    sw.WriteLine(colour);
                }
                sw.WriteLine(lineBreak);
                foreach (PictureBox player in PlayerList)
                {
                    sw.WriteLine(player.Location.X);
                    sw.WriteLine(player.Location.Y);
                }
                sw.WriteLine(lineBreak);
                foreach (int turns in NumberOfTurns)
                {
                    sw.WriteLine(turns);
                }
                sw.WriteLine(lineBreak);
                sw.WriteLine(playerCounter);
            }
            using (StreamWriter sw = new StreamWriter(SavedGamesFile, true))
            {
                sw.WriteLine(GameName);
            }

            //Remove duplicates
            string[] lines = File.ReadAllLines(SavedGamesFile);
            File.WriteAllLines(SavedGamesFile, lines.Distinct().ToArray());
        }
        void ResumeGame()
        {
            string line;
            StreamReader sr = new StreamReader(GameFile);

            //get board
            SelectedBoard = sr.ReadLine();
            LoadBoard();
            sr.ReadLine(); //consumes the linebreak

            //set players
            while ((line = sr.ReadLine()) != lineBreak)
            {
                SelectedColours.Add(line);
            }
            LoadPlayers();

            //set player positions
            int i = 0;
            string coord = "x";
            while ((line = sr.ReadLine()) != lineBreak)
            {
                switch (coord)
                {
                    case "x":
                        PlayerList[i].Left = Convert.ToInt32(line);
                        coord = "y";
                        break;
                    case "y":
                        PlayerList[i].Top = Convert.ToInt32(line);
                        coord = "x";
                        i++;
                        break;
                }
            }
            SetPlayerParent();

            //set number of turns - used for high score
            NumberOfTurns.Clear();
            while ((line = sr.ReadLine()) != lineBreak)
            {
                NumberOfTurns.Add(Convert.ToInt32(line));
            }

            //set player-counter
            playerCounter = Convert.ToInt32(sr.ReadLine());

            sr.Close();
        }
    }
}
