using System;
using System.Collections.Generic;
using System.Drawing;

namespace tower_defense_domain.enemies
{
    public abstract class AbstractEnemy : IEnemy
    {
        public int Damage { get; private set; }
        public Point Location { get; private set; }
        public int HP { get; private set; }
        public int Speed { get; private set; }
        public int Money { get; private set; }
        public int Score { get; private set; }
        public IEnumerator<Point> Path { get; private set; }
        private Point nextPosition { get; set; }
        private GameBase target;
        private const int RadiusToNextPoint = 10;

        public AbstractEnemy(int hp, int damage, int speed, int money, int score, IEnumerable<Point> path, GameBase target)
        {
            this.target = target;
            Damage = damage;
            HP = hp;
            Speed = speed;
            Money = money;
            Score = score;
            Path = path.GetEnumerator();
            Path.MoveNext();
            Location = new Point
            {
                X = Path.Current.X + new Random().Next(-5, 5),
                Y = Path.Current.Y + new Random().Next(-5, 5)
            };
            Path.MoveNext();
            nextPosition = Path.Current;
        }

        public State Move()
        {
            if (HP < 0)
                return State.die;
            if (CheckReachingNextPosition())
                if (Path.MoveNext())
                    nextPosition = Path.Current;
                else
                {
                    target.TakeDamage(Damage);
                    return State.finish;
                }
            var vector = new Point(nextPosition.X - Location.X, nextPosition.Y - Location.Y);
            var angle = Math.Atan2(vector.Y, vector.X);
            Location = new Point
            {
                X = Location.X + (int)Math.Round(Speed * Math.Cos(angle)),
                Y = Location.Y + (int)Math.Round(Speed * Math.Sin(angle))
            };
            return State.go;
        }

        private bool CheckReachingNextPosition()
        {
            return Math.Abs(nextPosition.X - Location.X) <= RadiusToNextPoint &&
                Math.Abs(nextPosition.Y - Location.Y) <= RadiusToNextPoint;
        }

        public void TakeDamage(int damage)
        {
            HP -= damage;
        }
    }
}
