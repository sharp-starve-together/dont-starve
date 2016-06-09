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
        private string fontName = "TowerDefense";
        private Font font;
        private Tuple<int, int> SelectedTypeTower;
        private bool isStateBuilding = false;
        public Size WorldSize { get; set; }
        Dictionary<string, Bitmap> Bitmaps = new Dictionary<string, Bitmap>();

        public TowerDefenseForm(Size clientSize, Size imageSize)
        {
            font = new Font(fontName, 20);
            Core = new Core(DrawFinishedWinOrDie, UpdateMoney);
            ClientSize = clientSize;
            WorldSize = imageSize;
            DoubleBuffered = true;

            var imagesDirectory = new DirectoryInfo("images");
            foreach (var e in imagesDirectory.GetFiles("*"))
                if (e.Name.IndexOf(".db") > 0)
                    continue;
                else
                    Bitmaps[e.Name] = (Bitmap)Bitmap.FromFile(e.FullName);

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

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            DrawImagesPath(graphics);
            DrawGameObjects(graphics);

            graphics.DrawString("Score: " + Core.Score.ToString(), font, Brushes.Black, new Point(200, 5));
            graphics.DrawString("Money: " + Core.Money.ToString(), font, Brushes.Black, new Point(200, 30));
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
                var args = new object[] { e.Location };
                var nameTower = GetNameTower(SelectedTypeTower.Item1);
                var fullName = "tower_defense_domain.towers." + nameTower;
                Core.AddTower(System.Type.GetType(fullName), args);
            }
            isStateBuilding = false;
        }

        private string GetNameTower(int number)
        {
            switch(number)
            {
                case 0:
                    return "ArcherTower";
                case 1:
                    return "MagesTower";
                default:
                    return "ArcherTower";
            }
        }

        public void DrawFinishedWinOrDie(bool isWin)
        {
            // соответствующий гуй для:
            // победы (isWin == true)
            // и поражения (isWin == false)
        }

        public void UpdateMoney()
        {
            SetEnabledOrDisenabledTowersColor();
        }

        private void SetEnabledOrDisenabledTowersColor()
        {
            foreach (Control control in Controls)
                if (control.GetType() == typeof(Button) && (string)control.Tag == "Tower")
                {
                    var button = (Button)control;
                    var cost = int.Parse(button.Text);
                    button.ForeColor = (Core.Money < cost) ? Color.Red : Color.Blue;
                }
        }

        public void CreateMenu()
        {
            var towerWidth = 64;
            var border = 20;
            var posWidth = ClientSize.Width - 2 * towerWidth - 2 * border;

            var ButtonUpdateTower = CreateButton(
                new Point(posWidth + 3 * border / 2 + towerWidth, towerWidth),
                "Update",
                Font,
                "Update.png",
                new Size(towerWidth, 32));
            ButtonUpdateTower.Click += (sender, args) =>
                {

                };
            Controls.Add(ButtonUpdateTower);

            for (int i = 0; i < 2; i++)
            {
                var tower = GetTmpTower(i);
                CreateTowerButton(
                    new Point(posWidth + i * (border + towerWidth), 2 * towerWidth),
                    tower.NameImage,
                    tower.Cost,
                    i,
                    towerWidth);
            }
        }

        public ITower GetTmpTower(int number)
        {
            var nameTower = GetNameTower(number);
            var fullName = "tower_defense_domain.towers." + nameTower;
            var pp = System.Type.GetType(fullName);
            var obj = Activator.CreateInstance(pp, new object[] { new Point(0, 0) });
            return (ITower)obj;
        }

        public void CreateTowerButton(Point pos, string name, int cost, int number, int width)
        {
            var tower = CreateButton(
                pos,
                cost.ToString(),
                Font,
                name,
                new Size(width, width + 50));
            tower.ForeColor = Color.Blue;
            tower.Font = new Font(Font.Name, 14, FontStyle.Bold);
            tower.BackgroundImageLayout = ImageLayout.Zoom;
            tower.Tag = "Tower";
            tower.Click += (sender, args) =>
            {
                SelectedTypeTower = new Tuple<int, int>(number, 1);
                isStateBuilding = false;
                var clicked = (Button)sender;
                var neededCost = int.Parse(clicked.Text);
                if (Core.Money < neededCost)
                    clicked.ForeColor = Color.Red;
                else
                {
                    clicked.ForeColor = Color.Blue;
                    Core.AddMoney(-neededCost);
                    isStateBuilding = true;
                }
            };
            Controls.Add(tower);
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
