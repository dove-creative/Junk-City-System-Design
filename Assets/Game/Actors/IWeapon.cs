namespace JunkCity.Game
{
    public interface IWeapon
    {
        float AttackPower { get; }
        void Attack();
    }
}
