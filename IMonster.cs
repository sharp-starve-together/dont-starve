using System;
using System.Collections.Generic;
using System.Text;

namespace tower_defense
{
    interface IMonster
    {
        int damage { get; }
        int hp { get; }
        void Move();
    }
}
