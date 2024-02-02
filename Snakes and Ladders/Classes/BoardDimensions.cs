using System;
using System.Drawing;
using System.Windows.Forms;
using static Snakes_and_Ladders.FormDimensions;
using static Snakes_and_Ladders.Components;

namespace Snakes_and_Ladders.Classes
{
    class BoardDimensions
    {
        public static Point StartingPosition;
        public static Point Grid1;
        public static Point[,] QGGrid = new Point[5, 5];
        public static Point[,] StandardGrid = new Point[10, 10];
        public static Point[,] LGGrid = new Point[15, 15];
        public static void BoardGrid(Point[,] Grid)
        {
            int Row = 0;
            Point Position = Grid1;
            while (Row < Grid.GetLength(0))
            {
                if (Row == 0 || Row % 2 == 0)
                {
                    //row number is even
                    int Col = 0;
                    while (Col < Grid.GetLength(1))
                    {
                        Grid[Row, 0] = new Point(Position.X + (Col * BoardSquare), Position.Y);
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
        }
        public static PictureBox DefaultBoard(Form form) => new PictureBox
        {
            Size = new Size(form.Height, form.Height),
            SizeMode = PictureBoxSizeMode.Zoom,
            Location = new Point(FormCentre(form).X - form.Height / 2, 0)
        };

        public static int[,] StandardPositions = new int[10, 10];
        void SetGrid(int X, int Y, int location)
        {
            StandardPositions[X, Y] = location;
            //have the location = to a point in a new array
        }
        void GridSetUp(Form form)
        {
            //100 = array[0,0]
            //99 = array[0,1]

        }

        public static int BoardSquare;
        



        int[,] board = new int[10,10];



    }
}
