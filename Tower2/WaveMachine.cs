using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tower_defense_domain.enemies;

namespace tower_defense_domain
{
    public class WaveMachine
    {
        private List<Point> path;
        private int level = 1;
        private Wave wave;
        private GameBase target;
        private Random randomaizer;

        private const double TimerCooldownMax = 20;
        private double TimerCooldown = TimerCooldownMax;

        public WaveMachine(List<Point> path, GameBase target)
        {
            this.target = target;
            this.path = path;
            wave = new Wave(path, level, target);
        }

        public void CreateWave()
        {
            wave =  new Wave(path, level, target);
            level++;
        }

        public IEnemy GetNewEnemy(double deltaTime)
        {
            if (TimerCooldown > 0)
                TimerCooldown -= deltaTime;
            else
            {
                TimerCooldown = TimerCooldownMax;
                return wave.CreateEnemy();
            }
            return null;
        }
    }
}
