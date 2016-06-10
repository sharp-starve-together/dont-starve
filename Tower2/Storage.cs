using System;
using System.Collections.Generic;
using System.Linq;
using TowerDefenseDomain;

namespace Tower
{
    public class Storage
    {
        public List<AbstractTower> Towers { get; set; }
        public List<BaseEnemy> Enemies { get; set; }
        public List<AbstractBullet> Bullets { get; set; }

        public List<IGameObject> GameObject;

        private Action<int> UpdateScore;
        private Action<int> UpdateMoney;

        public Storage(GameBase _base, Action<int> updateScore, Action<int> updateMoney)
        {
            GameObject = new List<IGameObject> { _base };
            UpdateMoney = updateMoney;
            UpdateScore = updateScore;
            Bullets = new List<AbstractBullet>();
            Towers = new List<AbstractTower>();
            Enemies = new List<BaseEnemy>();
        }

        public void Update(double deltaTime)
        {
            if (Towers != null)
                foreach (var tower in Towers)
                {
                    var bullet = tower.TryShoot(Enemies);
                    if (bullet != null)
                        Bullets.Add(bullet);
                }
            if (Bullets != null)
                for (var i = 0; i < Bullets.Count(); i++)
                {
                    var result = Bullets[i].Move();
                    if (result == TowerDefenseDomain.State.Dead)
                        Bullets.Remove(Bullets[i]);
                }
            if (Enemies != null)
                for (var i = 0; i < Enemies.Count; i++)
                {
                    var moved = Enemies[i].Move(UpdateScore, UpdateMoney);
                    if (moved)
                        continue;
                    Enemies.Remove(Enemies[i]);
                }

            GameObject.Clear();
            GameObject.AddRange(Enemies);
            GameObject.AddRange(Bullets);
            GameObject.AddRange(Towers);
        }
    }
}
