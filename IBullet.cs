using System;
using System.Collections.Generic;
using System.Text;

namespace tower_defense
{
    public interface IBullet
    {
        void Move();
        event Action<IMonster> Kill;
    }
}
