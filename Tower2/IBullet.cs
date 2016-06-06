using System.Drawing;
using Tower2;
namespace tower_defense_domain
{
    public interface IBullet : IGameObject
    {
        State Move();
        void DealDamage();
        int Damage { get; }
    }
}
