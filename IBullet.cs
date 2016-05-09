using System;
using System.Collections.Generic;
using System.Text;

namespace tower_defense_domain
{
    public interface IBullet
    {
        void Move();
        event Action<IEnemy> Kill;
        int damage { get; }
    }
}
