using System;
using System.Drawing;

namespace TowerDefenseDomain
{
    public class MagicBullet : AbstractBullet
    {
        public MagicBullet(BaseEnemy target, Point location)
            : base(target, location)
        {
            NameImage = "MagicBullet.png";
            Damage = 4;
        }
    }
}