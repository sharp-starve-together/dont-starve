using System.Drawing;

namespace Tower
{
    public interface IGameObject
    {
        Point Location { get; set; }
        string NameImage { get; set; }
    }
}
