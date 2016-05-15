using System.Collections.Generic;
using System.Drawing;

namespace tower_defense_domain.enemies
{
    public class Enemy1 : AbstractEnemy
    {
        public Enemy1(int hp, int damage, int speed, int money, int score, IEnumerable<Point> path, GameBase target)
            : base(hp, damage, speed, money, score, path, target)
        { }
    }
}
