using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace tower_defense.towers
{
    public abstract class AbstractTower : ITower
    {
        public int range { get; set; }
        public int atackSpeed { get; set; }
        public Point location { get; set; }

        public AbstractTower(Point location, int range, int atackSpeed) {
            this.location = location;
            this.atackSpeed = atackSpeed;
            this.range = range;
        }
        
        public abstract IBullet Shoot(IEnumerable<IMonster> monsters);
        
    }
}
