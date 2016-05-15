using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tower_defense_domain
{
    class WaveMachine
    {
        private IEnumerable<Point> path;
        private int level = 1;
        private int countMonsters = 20;
        private Wave wave;
        private Base target;

        WaveMachine(IEnumerable<Point> path, Base target)
        {
            this.target = target;
            this.path = path;
        }

        void CreateWave()
        {
            wave =  new Wave(path, level, countMonsters, target);
            level++;
            countMonsters += 20;
        }
        IEnumerable<IEnemy> timer()
        {
            foreach (var monster in wave.CreateEnemy())
                yield return monster;
        }
    }
}
