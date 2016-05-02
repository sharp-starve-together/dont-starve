using System;
using System.Collections.Generic;
using System.Text;

namespace tower_defense
{
    interface ITower
    {
        IBullet shoot(IEnumerable<IMonster> monsters);
    }
}
