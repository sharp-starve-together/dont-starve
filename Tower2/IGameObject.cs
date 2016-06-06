using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tower2
{
    public interface IGameObject
    {
        Point Location { get; set; }
        string NameImage { get; set; }
    }
}
