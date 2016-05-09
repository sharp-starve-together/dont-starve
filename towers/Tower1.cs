using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tower_defense_domain.bullets;

namespace tower_defense_domain.towers
{
    class Tower1 : AbstractTower
    {
        protected override IBullet createBullet(IEnemy enemy)
        {
            return new Bullet1(enemy, location, 3);
        }

        Tower1(Point location, int range, int atackSpeed)
            :base(location,range,atackSpeed)
        { }
    }
}
