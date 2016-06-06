using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tower2;

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
        public List<IGameObject> GameObject;

        public Core(Action<bool> finished, Action<int> updateMoney, Action<int> updateScore)
        {
            Score = 0;
            enemies = new List<IEnemy>();
            bullets = new List<IBullet>();
            towers = new List<ITower>();
            Path = new List<Point>() {
                new Point(50,50),
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
            GameObject = new List<IGameObject> { Base };
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
            if (towers != null)
                foreach (var tower in towers)
                {
                    var bullet = tower.TryShoot(enemies);
                    if (bullet != null)
                        bullets.Add(bullet);
                }
            if (bullets != null)
                for (var i = 0; i < bullets.Count(); i++)
                {
                    var action = bullets[i].Move();
                    if (action == State.die)
                        bullets.Remove(bullets[i]);
                }
            if (enemies != null)
                for (var i = 0; i < enemies.Count; i++ )
                {
                    var result = enemies[i].Move();
                    if (result != State.go)
                    {
                        if (result == State.die)
                        {
                            AddMoney(enemies[i].Money);
                        }
                        enemies.Remove(enemies[i]);
                    }
                }

            var newEnemy = WaveGenerator.GetNewEnemy(deltaTime);
            if (newEnemy != null)
                enemies.Add(newEnemy);

            if (!Base.IsAlive())
                Finished(false);
            GameObject.Clear();
            GameObject.AddRange(enemies);
            GameObject.AddRange(bullets);
            GameObject.AddRange(towers);
            GameObject.Add(Base);
        }

        public void AddTower(Type tower, object[] args)
        {
            var obj = Activator.CreateInstance(tower, args);
            towers.Add((ITower)obj);
        }
    }
}
