using System.Collections.Generic;
using System.Drawing;

namespace tower_defense_domain.enemies
{
    public class GhostEnemy : AbstractEnemy
    {
        public GhostEnemy(IEnumerable<Point> path, GameBase target)
            : base(path, target)
        {
            HP = 1;
            Damage = 1;
            Speed = 3;
            Money = 10;
            Score = 5;
            NameImage = "GhostEnemy.png";
        }
    }
}
