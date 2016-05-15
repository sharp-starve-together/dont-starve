using System;
using System.Collections.Generic;
using System.Drawing;

namespace tower_defense_domain.enemies
{
    
    public abstract class AbstractEnemy : IEnemy
    {
        public int damage { get; private set; }
        public Point location { get; private set; }
        public int hp { get; set; }
        public int speed { get; set; }
        public IEnumerator<Point> path { get; private set; }
        private Point nextPosition { get; set; }
        private Base target;

        public AbstractEnemy(int hp, int damage, int speed, IEnumerable<Point> path, Base target)
        {
            this.target = target;
            this.damage = damage;
            this.hp = hp;
            this.speed = speed;
            this.path = path.GetEnumerator();
            this.path.MoveNext();
            location = new Point
            {
                X = this.path.Current.X + new Random().Next(-5, 5),
                Y = this.path.Current.Y + new Random().Next(-5, 5)
            };
            this.path.MoveNext();
            nextPosition = this.path.Current;
        }
        public State Move()
        {
            if (hp < 0)
                return State.die;
            if (CheckReachingNextPosition())
                if (path.MoveNext())
                    nextPosition = path.Current;
                else
                {
                    target.TakeDamage(damage);
                    return State.finish;
                }
            var vector = new Point(nextPosition.X - location.X, nextPosition.Y - location.Y);
            var angle = Math.Atan2(vector.Y, vector.X);
            location = new Point
            {
                X = location.X + (int)Math.Round(speed * Math.Cos(angle)),
                Y = location.Y + (int)Math.Round(speed * Math.Sin(angle))
            };
            return State.go;

        }

        private bool CheckReachingNextPosition()
        {
            return Math.Abs(nextPosition.X - location.X) <= 10 &&
                Math.Abs(nextPosition.Y - location.Y) <= 10;
        }

        public void TakeDamage(int damage)
        {
            hp -= damage;
        }
    }
}
