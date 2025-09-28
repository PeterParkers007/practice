namespace Game.Interfaces
{
    public interface IDamageable
    {
        void TakeDamage(float damage,Character character);
        float CurrentHealth { get; }
        event System.Action<float> OnHealthChanged;
        event System.Action OnDeath;
    }
}