using System.Collections.Generic;
using System.Drawing;

namespace tower_defense_domain.enemies
{
    public class Enemy1 : AbstractEnemy
    {
        public Enemy1(int hp, int damage, int speed, IEnumerable<Point> path)
            :base(hp, damage, speed, path)
        {
            
        }
    }
}
