using System;
using System.Drawing;

namespace tower_defense_domain
{
    public class Base: IDisposable
    {
        int hp;
        Point location;
        public Base(Point location, int hp = 100)
        {
            this.hp = hp;
            this.location = location;
        }

        public void Dispose()
        {
            GC.Collect();
        }

        public void TakeDamage(int damage)
        {
            hp -= damage;
            if (hp <= 0)
            {
                Dispose();
            }
        }
    }
}
