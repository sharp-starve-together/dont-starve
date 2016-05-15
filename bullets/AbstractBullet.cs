using System;
using System.Drawing;

namespace tower_defense_domain.bullets
{
    public abstract class AbstractBullet : IBullet
    {
        private readonly IEnemy target;
        private Point location;
        public int damage { get; set; }
        private int speed = 50;

        public AbstractBullet(IEnemy target, Point location, int damage)
        {
            this.target = target;
            this.location = location;
            this.damage = damage;
        }

        public State Move()
        {
            var vector = new Point(target.location.X - location.X, target.location.Y - location.Y);
            var angle = Math.Atan2(vector.Y, vector.X);
            location = new Point
            {
                X = location.X + (int)Math.Round(speed * Math.Cos(angle)),
                Y = location.Y + (int)Math.Round(speed * Math.Sin(angle))
            };
            if (location.X == target.location.X && location.Y == target.location.Y)
            {
                DealDamage();
                return State.die;
            }
            return State.go;
            
        }

        public void DealDamage()
        {
            target.TakeDamage(damage);
        }
    }
}
