using System;
using System.Collections.Generic;
using System.Text;

namespace tower_defense
{
    public interface ITower
    {
        IBullet shoot(IEnumerable<IMonster> monsters);
        IBullet Shoot(IEnumerable<IMonster> monsters);
    }
}
