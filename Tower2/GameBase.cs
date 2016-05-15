using System;
using System.Drawing;

namespace tower_defense_domain
{
    public class GameBase
    {
        public int HP { get; set; }
        public Point Location { get; set; }

        public GameBase(Point location, int hp = 100)
        {
            HP = hp;
            Location = location;
        }

        public bool IsAlive()
        {
            return HP > 0;
        }

        public void TakeDamage(int damage)
        {
            HP -= damage;
        }
    }
}
