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
            switch (randomaizer.Next(1, 2))
            {
                case 1: return new Enemy1(1, 1, 3, 10, 5, path, target);
                case 2: return new Enemy2(5, 2, 1, 10, 5, path, target);
                default: return null;
            }
        }
    }
}