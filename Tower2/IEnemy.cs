using System.Collections.Generic;
using System.Drawing;

namespace tower_defense_domain
{
    public interface IEnemy
    {
        int HP { get; }
        int Damage { get; }
        int Speed { get; }
        int Score { get; }
        int Money { get; }
        Point Location { get; }
        IEnumerator<Point> Path { get; }
        State Move();
        void TakeDamage(int damage);
    }
}
