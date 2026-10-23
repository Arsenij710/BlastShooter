public interface IDamageable
{
    void TakeDamage(float amount);
}

public interface IPoolable
{
    void OnSpawn();
    void OnDespawn();
}
