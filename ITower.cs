using System;
using System.Collections.Generic;
using System.Text;

namespace tower_defense
{
    public interface ITower
    {
        IBullet Shoot(IEnumerable<IMonster> monsters);
    }
}
