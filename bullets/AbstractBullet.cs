using System;
using System.Collections.Generic;
using System.Text;

namespace tower_defense.bullets
{
    public abstract class AbstractBullet : IBullet
    {
        public abstract event Action<IMonster> Kill;

        public void Move()
        {
            throw new NotImplementedException();
        }
    }
}
