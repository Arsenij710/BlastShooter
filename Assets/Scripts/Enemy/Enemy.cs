using UnityEngine;

public class Enemy : PooledObject, IDamageable
{
    private EnemyHealthBar _activeHealthBar;

    [SerializeField] private SOEnemyConfig config;
    [SerializeField] private SoundPlayer _hitAudio;

    private EnemyFlash _flash;
    private Rigidbody _rb;
    private float _currentHealth;
    private Transform _worldCanvasTransform;
    public SOEnemyConfig Config => config;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _flash = GetComponent<EnemyFlash>();
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
    public void Initialize(Transform worldCanvasTransform)
    {
        _worldCanvasTransform = worldCanvasTransform;
    }
    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        _hitAudio.PlaySound();

        if (_currentHealth <= 0)
        {
            Die();
            return;
        }
        
        if (_activeHealthBar == null)
        {
            GameObject barObj = PoolManager.Instance.Spawn(config.healthBarPrefab, transform.position, Quaternion.identity);
            barObj.transform.SetParent(_worldCanvasTransform, false);

            _activeHealthBar = barObj.GetComponent<EnemyHealthBar>();
        }
        _activeHealthBar.Initialize(transform, _currentHealth, config.maxHealth, () => _activeHealthBar = null);
        _flash.Flash();
    }
    private void Die()
    {
        if (_activeHealthBar != null)
        {
            _activeHealthBar.ReleaseSelf();
            _activeHealthBar = null;
        }

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
