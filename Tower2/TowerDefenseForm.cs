using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using tower_defense_domain;
using tower_defense_domain.towers;

namespace Tower2
{
    public partial class TowerDefenseForm : Form
    {
        public Core Core;
        private Timer timer;
        private string FontName = "TowerDefense";
        private Font font;
        private Tuple<int, int> SelectedTypeTower;
        private bool isStateBuilding = false;
        public Size WorldSize { get; set; }
        Dictionary<string, Bitmap> Bitmaps = new Dictionary<string, Bitmap>();

        public TowerDefenseForm(Core core, Size clientSize, Size imageSize)
        {
            font = new Font(FontName, 20);
            Core = core;
            ClientSize = clientSize;
            WorldSize = imageSize;
            DoubleBuffered = true;

            var imagesDirectory = new DirectoryInfo("images");
            foreach (var e in imagesDirectory.GetFiles("*"))
                if (e.Name.IndexOf(".db") > 0)
                    continue;
                else
                    Bitmaps[e.Name] = (Bitmap)Bitmap.FromFile(e.FullName);
            BackgroundImage = Bitmaps["Ground.png"];

            timer = new Timer();
            timer.Interval = 100;
            timer.Tick += TimerTick;
            timer.Start();
        }
        void TimerTick(object sender, EventArgs args)
        {
            Core.Update(1);
            Invalidate();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            var graphics = e.Graphics;

            //e.Graphics.FillRectangle(Brushes.Black, 0, 0, ClientSize.Width, ClientSize.Height);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            DrawImagesPath(graphics);
            DrawGameObjects(graphics);

            graphics.DrawString(Core.Score.ToString() + "Score", font, Brushes.Black, new Point(10, 5));
            graphics.DrawString(Core.Money.ToString() + "Money", font, Brushes.Black, new Point(10, 30));
            CreateMenu();
        }

        public void DrawGameObjects(Graphics graphics)
        {
            foreach(var obj in Core.GameObject)
            {
                var nameImageFile = obj.NameImage;
                var size = new Rectangle(obj.Location.X, obj.Location.Y, WorldSize.Width, WorldSize.Height);
                DrawImage(graphics, size, nameImageFile);
            }
        }

        public void DrawImagesPath(Graphics graphics)
        {
            foreach (Point point in Core.Path)
            {
                var nameImageFile = "Path.png";
                var size = new Rectangle(point.X, point.Y, WorldSize.Width, WorldSize.Height);
                DrawImage(graphics, size, nameImageFile);
            }
        }
        public void DrawImage(Graphics graphics, Rectangle size, string name)
        {
            var compressionSize = size;
            var image = Bitmaps[name];
            graphics.DrawImage(image, compressionSize);
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (isStateBuilding && SelectedTypeTower != null)
            {
                var args = new object[3]{e.Location, 100, 1};
                Core.AddTower(System.Type.GetType("tower_defense_domain.towers.Tower1"), args);
            }
        }
        public void CreateMenu()
        {
            var ButtonCreateTower = CreateButton(new Point(ClientSize.Width * 8 / 10, ClientSize.Height / 3), "Create", Font, "Create1.png", new Size(64, 32));
            ButtonCreateTower.Click += (sender, args) =>
            {
                isStateBuilding = !isStateBuilding;
            };
            var ButtonUpdateTower = CreateButton(new Point(ClientSize.Width * 9 / 10, ClientSize.Height / 3), "Update", Font, "Update.png", new Size(64, 32));
            ButtonUpdateTower.Click += (sender, args) =>
            {

            };
            var ButtonTower1 = CreateButton(new Point(ClientSize.Width * 8 / 10, ClientSize.Height / 5), "", Font, "Tower1.png", new Size(32, 32));
            ButtonTower1.Click += (sender, args) =>
                {
                    SelectedTypeTower = new Tuple<int,int>(1, 1);
                };
            Controls.Add(ButtonTower1);
            Controls.Add(ButtonCreateTower);
            Controls.Add(ButtonUpdateTower);
        }
        public Button CreateButton(Point position, string text, Font font, string image, Size buttonSize)
        {
            var button = new Button();
            button = (Button)SetStyle(button, position, text, font, image, Color.Goldenrod, buttonSize);
            button.FlatAppearance.MouseOverBackColor = Color.Gray;
            button.FlatAppearance.MouseDownBackColor = Color.DimGray;
            button.FlatAppearance.BorderSize = 0;
            button.FlatStyle = FlatStyle.Flat;
            button.TextAlign = ContentAlignment.BottomCenter;

            return button;
        }
        public Control SetStyle(Control control, Point position, string text, Font font, string image, Color backColor, Size controlSize)
        {
            control.BackColor = backColor;
            control.Text = text;
            control.Font = font;
            control.Size = controlSize;
            control.TabStop = false;
            control.ForeColor = Color.Azure;
            if (image != null)
                control.BackgroundImage = Bitmaps[image];

            var textSize = TextRenderer.MeasureText(text, font);
            control.Location = new Point(position.X - textSize.Width / 2, position.Y);

            return control;
        }
    }
}
