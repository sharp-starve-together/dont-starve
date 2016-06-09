using System;
using System.Drawing;

namespace tower_defense_domain.bullets
{
    public class ArrowBullet : AbstractBullet
    {
        public ArrowBullet(IEnemy target, Point location)
            : base(target, location)
        {
            NameImage = "ArrowBullet.png";
            Damage = 2;
        }
    }
}
