using System;
using System.Collections.Generic;
using System.Drawing;
using tower_defense_domain.enemies;

namespace tower_defense_domain
{
    public class Wave
    {
        private readonly IEnumerable<Point> path;
        private int level;
        private Random randomaizer;
        private readonly GameBase target;

        public Wave(IEnumerable<Point> path, int level, GameBase target)
        {
            this.target = target;
            this.level = level;
            this.path = path;
            randomaizer = new Random();
        }

        public IEnemy CreateEnemy()
        {
            // как узнать количество наших классов с помощью рефлексии?

            // и да, в randomaizer верхняя граница не учитывается
            switch (randomaizer.Next(1, 3))
            {
                case 1: return new GhostEnemy(path, target);
                case 2: return new TrollEnemy(path, target);
                default: return null;
            }
        }
    }
}