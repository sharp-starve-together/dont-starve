using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tower_defense.towers
{
    class Tower2 : AbstractTower
    {
        public override IBullet Shoot(IEnumerable<IMonster> monsters)
        {
            throw new NotImplementedException();
        }
        Tower2(Point location, int range, int atackSpeed)
            : base(location, range, atackSpeed)
        { }
    }
}
