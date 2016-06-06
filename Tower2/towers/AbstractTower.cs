using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Tower2;

namespace tower_defense_domain.towers
{
    public abstract class AbstractTower : ITower
    {
        public int Range { get; set; }
        public int AtackSpeed { get; set; }
        public Point Location { get; set; }
        protected int timerReload;
        public int Cost { get; set; }

        public string NameImage { get { return "Tower1.png"; } set { } }
        public void Upgreate()
        { }

        public AbstractTower(Point location, int range, int atackSpeed)
        {
            Location = location;
            AtackSpeed = atackSpeed;
            Range = range;
            timerReload = 0;
        }

        public IBullet TryShoot(IEnumerable<IEnemy> enemies)
        {
            if (timerReload > 0)
            {
                timerReload -= 1;
                return null;
            }
            foreach (var enemy in enemies)
            {
                var vector = new Point(enemy.Location.X - Location.X, enemy.Location.Y - Location.Y);
                if (length(vector) < Range)
                    SetTimerReload();
                    return createBullet(enemy);
            }
            return null;
        }

        protected abstract IBullet createBullet(IEnemy enemy);
        protected abstract void SetTimerReload();

        private int length(Point vector)
        {
            return (int)Math.Round(Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y));
        }
    }
}
