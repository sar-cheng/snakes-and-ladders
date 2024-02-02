using System.Drawing;

namespace SnakesAndLadders.Classes
{
    class Fonts
    {
        public static Font headingFont (int size) => new Font("Rockwell", size);
        public static Font buttonFont() => new Font("Calibri", 9, FontStyle.Bold);
        public static Font myFont(int size) => new Font("Calibri", size);
    }
}
