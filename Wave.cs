using System;
using System.Collections.Generic;
using System.Drawing;
using tower_defense_domain.enemies;

namespace tower_defense_domain
{
    public class Wave
    {
        private readonly IEnumerable<Point> path;
        private int countMonsters;
        private int level;
        private Random randomaizer;

        public Wave(IEnumerable<Point> path, int level,int countMonsters)
        {
            this.level = level;
            this.path = path;
            this.countMonsters = countMonsters;
            randomaizer = new Random();

        }

        public IEnumerable<IEnemy> CreateEnemy()
        {
            countMonsters -= 1;
            //интервал между монстрами
            for (var i = 0; i < 18; i++)
                yield return null;
            if (countMonsters > 0)
                switch (randomaizer.Next(1,2))
                {
                    case 1: { yield return new Enemy1(1, 1, 3, path); break; }
                    case 2: { yield return new Enemy2(5, 2, 1, path); break; }
                }
            else
                yield return null;
        }
    }
}