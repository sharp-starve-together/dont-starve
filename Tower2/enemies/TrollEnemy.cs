using System.Collections.Generic;
using System.Drawing;

namespace TowerDefenseDomain
{
    public class TrollEnemy : BaseEnemy
    {
        public TrollEnemy(IEnumerable<Point> path, GameBase target)
            : base(path, target)
        {
            HP = 5;
            Damage = 2;
            Speed = 1;
            Money = 10;
            Score = 5;
            NameImage = "TrollEnemy.png";
        }
    }
}