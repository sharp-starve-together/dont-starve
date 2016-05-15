using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tower_defense_domain
{
    public class Core
    {
        public List<Point> Path { get; set; }
        public GameBase Base { get; set; }
        public WaveMachine WaveGenerator { get; set; }

        public List<ITower> towers { get; set; }
        public List<IEnemy> enemies { get; set; }
        public List<IBullet> bullets { get; set; }

        public int Score { get; set; }
        public int ScoreToFinish { get; set; }
        public int Money { get; set; }

        public Action<bool> Finished;
        public Action<int> UpdateMoney;
        public Action<int> UpdateScore;

        public Core(Action<bool> finished, Action<int> updateMoney, Action<int> updateScore)
        {
            Path = new List<Point>() {
                new Point(0, 0),
                new Point(200, 200),
                new Point(400, 400),
                new Point(600, 600)
            };
            Base = new GameBase(Path[Path.Count - 1], 1000);
            ScoreToFinish = 400;
            WaveGenerator = new WaveMachine(Path, Base);
            Finished = finished;
            UpdateMoney = updateMoney;
            UpdateScore = updateScore;
        }

        public void AddMoney(int count)
        {
            Money += count;
            UpdateMoney(Money);
        }

        public void AddScore(int count)
        {
            Score += count;
            UpdateScore(Score);
            if (Money >= ScoreToFinish)
                Finished(true);
        }

        public void Update(double deltaTime)
        {
            foreach (var tower in towers)
            {
                var bullet = tower.TryShoot(enemies);
                if (bullet != null)
                    bullets.Add(bullet);
            }

            foreach (var bullet in bullets)
            {
                var action = bullet.Move();
                if (action == State.die)
                    bullets.Remove(bullet);
            }

            foreach (var enemy in enemies)
            {
                var result = enemy.Move();
                if (result != State.go)
                {
                    if (result == State.die)
                    {
                        AddMoney(enemy.Money);
                    }
                    enemies.Remove(enemy);
                }
            }

            var newEnemy = WaveGenerator.GetNewEnemy(deltaTime);
            if (newEnemy != null)
                enemies.Add(newEnemy);

            if (!Base.IsAlive())
                Finished(false);
        }

        public void AddTower(Type tower)
        {
            var obj = Activator.CreateInstance(tower);
            towers.Add((ITower)obj);
        }
    }
}
