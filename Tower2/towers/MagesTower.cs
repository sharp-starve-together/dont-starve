using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tower_defense_domain.bullets;

namespace tower_defense_domain.towers
{
    public class MagesTower : AbstractTower
    {
        public MagesTower(Point location)
            : base(location)
        {
            Range = 120;
            AtackSpeed = 1;
            Cost = 20;
            NameImage = "MagesTower.png";
        }

        protected override IBullet СreateBullet(IEnemy enemy)
        {
            return new MagicBullet(enemy, Location);
        }

        protected override void SetTimerReload()
        {
            TimerReload = 50;
        }
    }
}
