using System;
using System.ComponentModel;
using System.Drawing;
using Tower;

namespace TowerDefenseDomain
{
    public abstract class AbstractBullet : IGameObject
    {
        public int Speed { get; set; }
        public int Damage { get; set; }
        public string NameImage { get; set; }
        public Point Location { get; set; }

        private readonly BaseEnemy target;

        public AbstractBullet(BaseEnemy target, Point location)
        {
            this.target = target;
            Location = location;
            NameImage = "Bullet.png";
            Speed = 50;
            Damage = 1;
        }

        public State Move()
        {
            var vector = new Point(target.Location.X - Location.X, target.Location.Y - Location.Y);
            var angle = Math.Atan2(vector.Y, vector.X);
            Location = new Point
            {
                X = Location.X + (int)Math.Round(Speed * Math.Cos(angle)),
                Y = Location.Y + (int)Math.Round(Speed * Math.Sin(angle))
            };
            if (NearEnemy(angle))
            {
                DealDamage();
                return State.Dead;
            }
            return State.Moved;
        }

        private bool NearEnemy(double angle)
        {
            return Math.Abs(Location.X - target.Location.X) <= Speed / 2 
                   && Math.Abs(Location.Y - target.Location.Y) <= Speed / 2;
        }

        public void DealDamage()
        {
            target.TakeDamage(Damage);
        }
    }
}
