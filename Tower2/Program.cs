﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TowerDefenseDomain;

namespace Tower
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var borderSpace = new Size(1000, 700);
            var imageSize = new Size(32, 32);
            Application.Run(new TowerDefenseForm(borderSpace, imageSize));
        }
    }
}
