using System;
using System.Drawing;

namespace tower_defense_domain.bullets
{
    class Bullet2 : AbstractBullet
    {
        public Bullet2(IEnemy target, Point location, int damage)
            : base(target, location, damage)
        { }
    }
}
