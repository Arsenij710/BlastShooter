using UnityEngine;

public class Enemy : PooledObject, IDamageable
{
    [SerializeField] private SOEnemyConfig config;
    private Rigidbody _rb;
    private float _currentHealth;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        Vector3 targetPosition = _rb.position + Vector3.back * config.fallSpeed * Time.fixedDeltaTime;
        _rb.MovePosition(targetPosition);
    }
    public override void OnSpawn()
    {
        base.OnSpawn();
        _currentHealth = config.maxHealth;
    }
    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        if (_currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        EnemySpawner.Instance.OnEnemyDeath();
        ReleaseSelf();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageable>(out var playerDamageable))
        {
            playerDamageable.TakeDamage(config.damage);
        }
    }
}
