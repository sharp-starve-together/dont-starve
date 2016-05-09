using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace tower_defense_domain.towers
{
    public abstract class AbstractTower : ITower
    {
        public int range { get; set; }
        public int atackSpeed { get; set; }
        public Point location { get; set; }
        //TODO reload

        public AbstractTower(Point location, int range, int atackSpeed) {
            this.location = location;
            this.atackSpeed = atackSpeed;
            this.range = range;
        }

        public IBullet Shoot(IEnumerable<IEnemy> enemies)
        {
            foreach (var enemy in enemies)
            {
                var vector = new Point(enemy.location.X - location.X, enemy.location.Y - location.Y);
                if (length(vector) < range)
                    return createBullet(enemy);
            }
            return null;
        }

        protected abstract IBullet createBullet(IEnemy enemy);

        private int length(Point vector)
        {
            return (int)Math.Round(Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y));
        }
    }
}
