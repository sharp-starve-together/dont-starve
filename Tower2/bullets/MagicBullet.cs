using System;
using System.Drawing;

namespace tower_defense_domain.bullets
{
    public class MagicBullet : AbstractBullet
    {
        public MagicBullet(IEnemy target, Point location)
            : base(target, location)
        {
            NameImage = "MagicBullet.png";
            Damage = 4;
        }
    }
}