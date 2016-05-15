namespace tower_defense_domain
{
    public interface IBullet
    {
        State Move();
        void DealDamage();
        int Damage { get; }
    }
}
