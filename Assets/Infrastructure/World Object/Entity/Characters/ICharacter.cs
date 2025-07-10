namespace JunkCity.Infrastructure
{
    public interface ICharacter
    {
        public string Name { get; }
        public int Health { get; }
        public int AttackPower { get; }
    }
}
