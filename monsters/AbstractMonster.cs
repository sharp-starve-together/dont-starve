using System.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace tower_defense.monsters
{
    public abstract class AbstractMonster : IMonster
    {
        public int damage { get; private set; }
        public Point location { get; private set; }
        public int hp { get; set; }
        public int speed { get; set; }
        public IEnumerator<Point> path { get; private set; }
        private Point nextPosition{get; set;}

        public AbstractMonster(int hp, int damage,int speed, IEnumerable<Point> path)
        {
            this.damage = damage;
            this.hp = hp;
            this.speed = speed;
            this.path = path.GetEnumerator();
            location = new Point
            {
                X = this.path.Current.X + new Random().Next(-5, 5),
                Y = this.path.Current.Y + new Random().Next(-5, 5)
            };
            this.path.MoveNext();
            nextPosition = this.path.Current;
        }
        public void Move()
        {
            if (CheckReachingNextPosition())
                if (path.MoveNext())
                    nextPosition = path.Current;
                else
                {
                    // дошел до конца
                }
            var vector = new Point(nextPosition.X - location.X, nextPosition.Y - location.Y);
            var angle = Math.Atan2(vector.Y, vector.X);
            location = new Point
            {
                X = location.X + (int)Math.Round(speed * Math.Cos(angle)),
                Y = location.Y + (int)Math.Round(speed * Math.Sin(angle))
            };

        }

        private bool CheckReachingNextPosition()
        {
            return Math.Abs(nextPosition.X - location.X) <= 10 &&
                Math.Abs(nextPosition.Y - location.Y) <= 10;
        }
    }
}
