using System;
using System.Drawing;

namespace tower_defense.bullets
{
    public abstract class AbstractBullet : IBullet
    {
        private readonly IMonster target;
        private Point location;
        public int damage { get; set; }

        public AbstractBullet(IMonster target, Point location)
        {
            this.target = target;
            this.location = location;
        }

        public abstract event Action<IMonster> Kill;

        public void Move()
        {
            throw new NotImplementedException();
        }
    }
}
