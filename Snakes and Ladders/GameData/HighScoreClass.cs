using System;
using System.Collections.Generic;
using System.IO;
namespace SnakesAndLadders
{
    class HighScoreClass
    {
        public static string HighScore;
        public static bool record = false;
        public static int UserVictory;
        public static string ScoreFile;
        public static List<int> NumberOfTurns = new List<int>();

        public static string GetHighScore(string file) 
        {
            string HS;
            using (StreamReader sr = new StreamReader(file))
            {
                HS = sr.ReadLine();
                sr.Close();
            }
            return HS;
        }

        public static void CheckHighScore()
        {
            try
            {
                if (UserVictory < Convert.ToInt32(HighScore))
                {
                    HighScore = Convert.ToString(UserVictory);
                    using (StreamWriter sw = new StreamWriter(ScoreFile))
                    {
                        sw.WriteLine(HighScore);
                        sw.Close();
                    }
                    record = true;
                }
            }
            catch (FormatException) //if highscore cannot be converted into int ("N/A" by default)
            {
                HighScore = Convert.ToString(UserVictory);
                using (StreamWriter sw = new StreamWriter(ScoreFile))
                {
                    sw.WriteLine(HighScore);
                    sw.Close();
                }
                record = true;
            }
        }
    }
}
