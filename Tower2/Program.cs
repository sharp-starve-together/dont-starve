using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using tower_defense_domain;

namespace Tower2
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
            Application.Run(new TowerDefenseForm(new Core(x => x = false, y => y += 1, z => z += 10), borderSpace, imageSize));
        }
    }
}
