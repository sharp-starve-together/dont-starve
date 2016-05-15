using System;
using System.Drawing;

namespace tower_defense_domain.bullets
{
    public abstract class AbstractBullet : IBullet
    {
        private readonly IEnemy target;
        private Point location;
        public int Damage { get; set; }
        private int speed = 50;

        public AbstractBullet(IEnemy target, Point location, int damage)
        {
            this.target = target;
            this.location = location;
            Damage = damage;
        }

        public State Move()
        {
            var vector = new Point(target.Location.X - location.X, target.Location.Y - location.Y);
            var angle = Math.Atan2(vector.Y, vector.X);
            location = new Point
            {
                X = location.X + (int)Math.Round(speed * Math.Cos(angle)),
                Y = location.Y + (int)Math.Round(speed * Math.Sin(angle))
            };
            if (location.X == target.Location.X && location.Y == target.Location.Y)
            {
                DealDamage();
                return State.die;
            }
            return State.go;
        }

        public void DealDamage()
        {
            target.TakeDamage(Damage);
        }
    }
}
