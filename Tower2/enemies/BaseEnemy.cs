using System;
using System.Collections.Generic;
using System.Drawing;
using Tower;

namespace TowerDefenseDomain
{
    public class BaseEnemy : IGameObject
    {
        public int Damage { get; set; }
        public Point Location { get; set; }
        public int HP { get; set; }
        public int Speed { get; set; }
        public int Money { get; set; }
        public int Score { get; set; }
        public IEnumerator<Point> Path { get; private set; }
        private Point nextPosition { get; set; }
        private GameBase target;
        private const int radiusToNextPoint = 10;

        public string NameImage { get; set; }

        public BaseEnemy(IEnumerable<Point> path, GameBase target)
        {
            this.target = target;
            Path = path.GetEnumerator();
            Path.MoveNext();
            Location = new Point
            {
                X = Path.Current.X + new Random().Next(-5, 5),
                Y = Path.Current.Y + new Random().Next(-5, 5)
            };
            Path.MoveNext();
            nextPosition = Path.Current;
            NameImage = "Enemy.png";
            Damage = 1;
            HP = 1;
            Speed = 1;
            Money = 10;
            Score = 5;
        }

        public bool Move(Action<int> AddScore, Action<int> AddMoney)
        {
            if (HP < 0)
            {
                AddMoney(Money);
                AddScore(Score);
                return false;
            }
            if (CheckReachingNextPosition())
            {
                if (Path.MoveNext())
                    nextPosition = Path.Current;
                else
                {
                    target.TakeDamage(Damage);
                    return false;
                }
            }
            var vector = new Point(nextPosition.X - Location.X, nextPosition.Y - Location.Y);
            var angle = Math.Atan2(vector.Y, vector.X);
            Location = new Point
            {
                X = Location.X + (int)Math.Round(Speed * Math.Cos(angle)),
                Y = Location.Y + (int)Math.Round(Speed * Math.Sin(angle))
            };
            return true;
        }

        private bool CheckReachingNextPosition()
        {
            return Math.Abs(nextPosition.X - Location.X) <= radiusToNextPoint 
                   && Math.Abs(nextPosition.Y - Location.Y) <= radiusToNextPoint;
        }

        public void TakeDamage(int damage)
        {
            HP -= damage;
        }
    }
}
