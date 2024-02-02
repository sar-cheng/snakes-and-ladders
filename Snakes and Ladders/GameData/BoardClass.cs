using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using static SnakesAndLadders.Components;

namespace SnakesAndLadders
{
    class BoardClass
    {
        public static (int X, int Y) Finish = (0, 0);
        public static Point Square1;
        public static int BoardSquare;
        public static string SelectedBoard;
        public static PictureBox Board = new PictureBox();
        public static int CheckPosArray; //relative to positions in SnakesLadders List
        
        //Positional grid
        public static Point[,] BoardGrid(int size)
        {
            Point[,] Grid = new Point[size, size];
            int Row = 0;
            Point Position = Square1; //(0, 0)
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
                        Grid[Row, Col] = new Point(Position.X + Col * BoardSquare, Position.Y);
                        Col--;
                    }
                    Position = new Point(Position.X, Position.Y - BoardSquare);
                }
                Row++;
            }
            return Grid;
        }
        public static (int, int)[] ChangePos7 = new (int, int)[]
        {
            //ladders
            (0, 0),
            (1, 5),
            (2, 2),
            (5, 4),
            //snakes
            (6, 3),
            (5, 3),
            (4, 4),
            (2, 3),
        };
        public static (int, int)[] FinalPos7 = new (int, int)[]
        {
            //ladders
            (2, 1),
            (3, 5),
            (5, 1),
            (6, 5),
            //snakes
            (2, 0),
            (4, 3),
            (2, 4),
            (0, 6),
        };
        public static (int, int)[] ChangePos10 = new (int, int)[]
        {
            //ladders
            (0, 3),
            (2, 6),
            (3, 9),
            (4, 1),
            (5, 6),
            (8, 1),
            //snakes
            (3, 8),
            (4, 2),
            (4, 6),
            (7, 6),
            (7, 1),
            (9, 5),
        };
        public static (int, int)[] FinalPos10 = new (int, int)[]
        {
            //ladders
            (2, 4),
            (6, 7),
            (5, 8),
            (6, 2),
            (8, 5),
            (9, 0),
            //snakes
            (0, 7),
            (0, 4),
            (1, 8),
            (3, 5),
            (4, 0),
            (3, 3),
        };
        public static (int, int)[] ChangePos15 = new (int, int)[]
        {
            //ladders
            (0, 2),
            (1, 11),
            (2, 5),
            (4, 1),
            (6, 12),
            (7, 0),
            (10, 8),
            (11, 13),
            //snakes
            (14, 7),
            (12, 10),
            (10, 4),
            (7, 13),
            (6, 8),
            (4, 13),
            (2, 3),
        };
        public static (int, int)[] FinalPos15 = new (int, int)[]
        {
            //ladders
            (3, 1),
            (3, 12),
            (8, 4),
            (6, 2),
            (9, 5),
            (13, 1),
            (12, 8),
            (14, 13),
            //snakes
            (10, 6),
            (2, 9),
            (4, 2),
            (5, 13),
            (3, 6),
            (2, 13),
            (0, 6),
        };
        public static List<(int, int)[]> SnakesLadders = new List<(int, int)[]>
        {
            ChangePos7,
            FinalPos7,
            ChangePos10,
            FinalPos10,
            ChangePos15,
            FinalPos15,
        };
        public static PictureBox DefaultBoard(Form form) => new PictureBox
        {
            Size = new Size(form.Height, form.Height),
            SizeMode = PictureBoxSizeMode.Zoom,
            Location = new Point(FormCentre(form).X - form.Height / 2, 0),
            BackgroundImageLayout = ImageLayout.Zoom,
        };
    }
}
