using System.Collections.Generic;
using System.Drawing;
using Tower2;

namespace tower_defense_domain
{
    public interface IEnemy : IGameObject
    {
        int HP { get; }
        int Damage { get; }
        int Speed { get; }
        int Score { get; }
        int Money { get; }
        IEnumerator<Point> Path { get; }
        State Move();
        void TakeDamage(int damage);
    }
}
