using System;
using System.Collections.Generic;
using System.Drawing;
using Tower;

namespace TowerDefenseDomain
{
    public class Core
    {
        public List<Point> Path { get; set; }
        public GameBase Base { get; set; }
        public WaveMachine WaveGenerator { get; set; }
        public Storage Storage { get; set; }

        public int Score { get; set; }
        public int ScoreToFinish { get; set; }
        public int Money { get; set; }

        public Action<bool> Finished { get; set; }
        public Action UpdateMoney { get; set; }

        public Core(Action<bool> finished, Action updateMoney)
        {
            Score = 0;
            Money = 30;
            Path = new List<Point>() {
                new Point(0, 200),
                new Point(200, 200),
                new Point(400, 200),
                new Point(400, 600),
                new Point(600, 600)
            };
            Base = new GameBase(Path[Path.Count - 1], 1000);
            ScoreToFinish = 400;
            WaveGenerator = new WaveMachine(Path, Base);
            Storage = new Storage(Base, AddScore, AddMoney);
            Finished = finished;
            UpdateMoney = updateMoney;
        }

        public void AddMoney(int count)
        {
            Money += count;
            UpdateMoney();
        }

        public void AddScore(int count)
        {
            Score += count;
            if (Money >= ScoreToFinish)
                Finished(true);
        }

        public void Update(double deltaTime)
        {
            Storage.Update(deltaTime);

            var newEnemy = WaveGenerator.GetNewEnemy(deltaTime);
            if (newEnemy != null)
                Storage.Enemies.Add(newEnemy);

            if (!Base.IsAlive())
                Finished(false);
            Storage.GameObject.Add(Base);
        }

        public void AddTower(Type tower, object[] args)
        {
            var obj = Activator.CreateInstance(tower, args);
            Storage.Towers.Add((AbstractTower)obj);
        }
    }
}
