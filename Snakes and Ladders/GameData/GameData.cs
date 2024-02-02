using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using static SnakesAndLadders.Components;

namespace SnakesAndLadders
{
    class GameData
    {
        public static bool ResumeSelect = false;
        public static int PlayerLimit;
        public static string GameFile;
        public static string GameName;
        public static string SavedGamesFile = "!SAVED GAMES!.txt";
        public static string[] IncompleteGames = File.ReadAllLines(SavedGamesFile);

        public static List<string> SelectedColours = new List<string>();
        public static List<PictureBox> PlayerList = new List<PictureBox>();
        public static int playerCounter = 0;

        public static PictureBox[] DiceStorage = new PictureBox[]
        {
           PicBox("1.png", 0, 0),
           PicBox("2.png", 0, 0),
           PicBox("3.png", 0, 0),
           PicBox("4.png", 0, 0),
           PicBox("5.png", 0, 0),
           PicBox("6.png", 0, 0),
        };
    }
}
