using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tower_defense_domain.bullets
{
    public class Bullet1 : AbstractBullet
    {
        public Bullet1(IEnemy target, Point location, int damage)
            : base(target, location, damage)
        { }
    }
}