using System;
using System.Drawing;

namespace TowerDefenseDomain
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

        protected override AbstractBullet СreateBullet(BaseEnemy enemy)
        {
            return new ArrowBullet(enemy, Location);
        }

        protected override void SetTimerReload()
        {
            TimerReload = 20;
        }
    }
}
