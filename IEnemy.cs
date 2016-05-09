using System.Collections.Generic;
using System.Drawing;

namespace tower_defense_domain
{
    public interface IEnemy
    {
        int damage { get; }
        int speed { get; }
        int hp { get; }
        Point location { get; }
        IEnumerator<Point> path { get; }
        void Move();
    }
}
