namespace JunkCity.World
{
    public interface IWeapon
    {
        float AttackPower { get; }
        void Attack();
    }
}
