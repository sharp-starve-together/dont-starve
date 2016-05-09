using System.Collections.Generic;
using System.Drawing;

namespace tower_defense_domain.enemies
{
    public class Enemy2 : AbstractEnemy
    {
        public Enemy2(int hp, int damage, int speed, IEnumerable<Point> path)
            :base(hp, damage, speed, path)
        {

        }
    }
}