using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tower_defense.bullets
{
    class Bullet1 : AbstractBullet
    {
        public override event Action<IMonster> Kill;
        Bullet1(IMonster target, Point location)
            :base(target, location)
        { }
    }
}
