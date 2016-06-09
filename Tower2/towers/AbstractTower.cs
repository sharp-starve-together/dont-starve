using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using Tower2;

namespace tower_defense_domain.towers
{
    public abstract class AbstractTower : ITower
    {
        public Point Location { get; set; }
        protected int TimerReload;
        public int Cost { get; set; }
        public int Range { get; set; }
        public int AtackSpeed { get; set; }
        public string NameImage { get; set; }

        protected abstract IBullet СreateBullet(IEnemy enemy);
        protected abstract void SetTimerReload();

        public AbstractTower(Point location)
        {
            Location = location;
            NameImage = "ArcherTower.png";
            AtackSpeed = 1;
            Range = 100;
            TimerReload = 0;
        }

        // сделать с out параметром !?
        public IBullet TryShoot(IEnumerable<IEnemy> enemies)
        {
            if (TimerReload > 0)
            {
                TimerReload -= 1;
                return null;
            }
            foreach (var enemy in enemies)
            {
                var vector = new Point(enemy.Location.X - Location.X, enemy.Location.Y - Location.Y);
                if (Length(vector) < Range)
                {
                    SetTimerReload();
                    return СreateBullet(enemy);
                }
            }
            return null;
        }

        public void Upgrade()
        {
            // реализовать улучшение зданий
        }

        private int Length(Point vector)
        {
            return (int)Math.Round(Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y));
        }
    }
}
