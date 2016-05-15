using System;
using System.Collections.Generic;
using System.Text;

namespace tower_defense_domain
{
    public interface ITower
    {
        IBullet TryShoot(IEnumerable<IEnemy> monsters);
    }
}
