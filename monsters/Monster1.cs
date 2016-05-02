using System.Collections.Generic;
using System.Drawing;

namespace tower_defense.monsters
{
    public class Monster1 : AbstractMonster
    {
        public Monster1(int hp, int damage, int speed, IEnumerable<Point> path)
            :base(hp, damage, speed, path)
        {
            
        }
    }
}
