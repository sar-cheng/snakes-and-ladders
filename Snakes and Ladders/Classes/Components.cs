using System;
using System.Windows.Forms;
using System.Drawing;
using static SnakesAndLadders.Classes.Fonts;

namespace SnakesAndLadders
{
    class Components
    {
        public static int ScreenWidth = Screen.PrimaryScreen.WorkingArea.Width;
        public static int ScreenHeight = Screen.PrimaryScreen.WorkingArea.Height;
        public static ToolTip Tip() => new ToolTip
        {
            AutoPopDelay = 5000,
            InitialDelay = 1000,
            ReshowDelay = 500,
            ShowAlways = true
        };
        public static Point FormCentre(Form form)
        {
            Point centre = new Point(form.Width / 2, form.Height / 2);
            return centre;
        }
        public static Control[] Setup(Form form, Label header)
        {
            var Exit = ExitButton(form);
            var Return = ReturnButton(form);
            var Minimise = MinimiseButton(form);
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

            form.Controls.AddRange(setUp);
            return setUp;
        }
        public static Label Heading(Form form, string text, int Y, int FontSize) => new Label
        {
            Text = text,
            TextAlign = ContentAlignment.MiddleCenter,
            BackColor = Color.White,
            Font = headingFont(FontSize),
            Size = new Size(form.Width, form.Height / 5),
            Location = new Point(0, form.Height / 2 - form.Height / 10 - Y)
        };
        public static Label label(string text, int X, int Y, int FontSize) => new Label
        {
            Text = text,
            TextAlign = ContentAlignment.MiddleCenter,
            BackColor = Color.Transparent,
            Font = headingFont(FontSize),
            AutoSize = true,
            Location = new Point(X, Y),
        };
        public static MyButton ExitButton(Form form)
        {
            MyButton button = new MyButton
            {
                Text = "X",
                FlatStyle = FlatStyle.Flat,
                AutoSize = true,
                BackColor = Color.Transparent,
            };
            button.Click += (sender, e) => { Application.Exit(); };
            button.Location = new Point(form.Width - button.Width, 0);
            button.FlatAppearance.BorderSize = 0;
            return button;
        }
        public static MyButton ReturnButton(Form form)
        {
            MyButton button = new MyButton
            {
                Text = "<",
                FlatStyle = FlatStyle.Flat,
                AutoSize = true,
                BackColor = Color.Transparent,
            };
            button.Click += (sender, e) => { Application.Restart(); };
            button.Location = new Point(form.Width - 2*button.Width, 0);
            return button;
        }
        public static MyButton MinimiseButton(Form form)
        {
            MyButton button = new MyButton
            {
                Text = "—",
                FlatStyle = FlatStyle.Flat,
                AutoSize = true,
                BackColor = Color.Transparent,
            };
            button.Click += (sender, e) => { form.WindowState = FormWindowState.Minimized; };
            button.Location = new Point(form.Width - 3 * button.Width, 0);
            return button;
        }
        public static MyButton BigButton(Form form, string text, int X, int Y, EventHandler @event)
        {
            MyButton button = new MyButton
            {
                Text = text,
                Size = new Size(form.Width / 5, form.Height / 8),
                Location = new Point(X, Y),
                AutoSize = false,
                FlatStyle = FlatStyle.Flat,
                BackgroundImage = Image.FromFile("button.png"),
                BackgroundImageLayout = ImageLayout.Stretch,
                BackColor = Color.Transparent,
                Font = buttonFont(),
            };
            button.Click += @event;
            return button;
        }
        public static MyButton CustomButton(string text, int X, int Y) => new MyButton
        {
            Text = text,
            Location = new Point(X, Y),
            FlatStyle = FlatStyle.Flat,
            AutoSize = true,
            Font = buttonFont(),
            BackgroundImage = Image.FromFile("button.png"),
            BackgroundImageLayout = ImageLayout.Stretch,
            BackColor = Color.Transparent,
        };
        public static PictureBox PicBox(string file, int W, int H) => new PictureBox //wtf
        {
            Name = file,
            Image = Image.FromFile(file),
            SizeMode = PictureBoxSizeMode.Zoom,
            Size = new Size(W, H),
            BackColor = Color.Transparent,
        };
        public static TextBox TxtBox(string text, int X, int Y, int W, int H, int FontSize) 
        {
            TextBox textbox = new TextBox
            {
                Text = text,
                Location = new Point(X, Y),
                Size = new Size(W, H),
                Font = headingFont(FontSize),
                TextAlign = HorizontalAlignment.Center,
                BorderStyle = BorderStyle.FixedSingle,
            };
            textbox.Click += (sender, e) =>
            {
                textbox.Text = "";
            };
            return textbox;
        }
        public static ListBox listbox(int X, int Y, int W, int H) 
        {
            ListBox listBox = new ListBox
            {
                BorderStyle = BorderStyle.None,
                SelectionMode = SelectionMode.One,
                Bounds = new Rectangle(new Point(X, Y), new Size(W, H)),
                ScrollAlwaysVisible = true,
                DrawMode = DrawMode.OwnerDrawFixed,
                Font = myFont(10)
            };
            listBox.DrawItem += new DrawItemEventHandler(listBox_DrawItem);

            void listBox_DrawItem(object sender, DrawItemEventArgs e)
            {
                ListBox list = (ListBox)sender;
                if (e.Index >= 0)
                {
                    object item = list.Items[e.Index];
                    e.DrawBackground();
                    e.DrawFocusRectangle();
                    Brush brush = new SolidBrush(e.ForeColor);
                    SizeF size = e.Graphics.MeasureString(item.ToString(), e.Font);
                    e.Graphics.DrawString(item.ToString(), e.Font, brush, e.Bounds.Left + (e.Bounds.Width / 2 - size.Width / 2), e.Bounds.Top + (e.Bounds.Height / 2 - size.Height / 2));
                }
            }
            return listBox;
        }
        public static Panel panel(string file, int X, int Y, int W, int H) => new Panel
        {
            BackgroundImage = Image.FromFile(file),
            BackgroundImageLayout = ImageLayout.Stretch,
            Location = new Point(X, Y),
            Size = new Size(W, H),
            BackColor = Color.Transparent,
        };
    }
}
