using System;
using System.Drawing;
using tower_defense_domain.bullets;

namespace tower_defense_domain.towers
{
    public class ArcherTower : AbstractTower
    {
        public ArcherTower(Point location) 
            : base(location)
        {
            Range = 100;
            AtackSpeed = 1;
            Cost = 10;
            NameImage = "ArcherTower.png";
        }

        protected override IBullet СreateBullet(IEnemy enemy)
        {
            return new ArrowBullet(enemy, Location);
        }

        protected override void SetTimerReload()
        {
            TimerReload = 20;
        }
    }
}
