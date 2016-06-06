using System;
using System.Collections.Generic;
using System.Text;
using Tower2;

namespace tower_defense_domain
{
    public interface ITower : IGameObject
    {
        IBullet TryShoot(IEnumerable<IEnemy> monsters);
        int Cost { get; set; }
    }
}
