using System;
using System.Drawing;

namespace TowerDefenseDomain
{
    public class ArrowBullet : AbstractBullet
    {
        public ArrowBullet(BaseEnemy target, Point location)
            : base(target, location)
        {
            NameImage = "ArrowBullet.png";
            Damage = 2;
        }
    }
}
