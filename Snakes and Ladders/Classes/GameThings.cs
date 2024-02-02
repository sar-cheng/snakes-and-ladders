using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using static Snakes_and_Ladders.FormDimensions;
using static Snakes_and_Ladders.Components;

namespace Snakes_and_Ladders.Classes
{
    class GameThings
    {
        public static string SelectedBoard = string.Empty;
        public static List<string> SelectedColours = new List<string>();
        public static List<int> NumberOfTurns = new List<int>();
        //don't need to be stored
        public static (int X, int Y) Finish = (0, 0);
        public static Point Square1;
        public static int BoardSquare;
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

        public static Point[,] BoardGrid(int size)
        {
            Point[,] Grid = new Point[size, size];
            int Row = 0;
            Point Position = Square1;
            while (Row < Grid.GetLength(0))
            {
                if (Row % 2 == 0)
                {
                    //row number is even
                    int Col = 0;
                    while (Col < Grid.GetLength(1))
                    {
                        Grid[Row, Col] = new Point(Position.X + Col * BoardSquare, Position.Y);
                        Col++;
                    }
                    Position = new Point(Position.X, Position.Y - BoardSquare);
                }
                else
                {
                    int Col = Grid.GetLength(1) - 1;
                    while (Col >= 0)
                    {
                        Grid[Row, Col] = new Point(Position.X + Col*BoardSquare, Position.Y);
                        Col--;
                    }
                    Position = new Point(Position.X, Position.Y - BoardSquare);
                }
                Row++;
            }
            return Grid;
        }
        public static PictureBox DefaultBoard(Form form) => new PictureBox
        {
            Size = new Size(form.Height, form.Height),
            SizeMode = PictureBoxSizeMode.Zoom,
            Location = new Point(FormCentre(form).X - form.Height / 2, 0)
        };
    }
}
