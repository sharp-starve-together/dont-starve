using System;
using System.Drawing;

namespace tower_defense.bullets
{
    class Bullet2 : AbstractBullet
    {
        public override event Action<IMonster> Kill;
        Bullet2(IMonster target, Point location)
            : base(target, location)
        { }
    }
}
