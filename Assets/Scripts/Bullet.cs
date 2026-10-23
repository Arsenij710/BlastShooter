using System.Collections;
using UnityEngine;

public class Bullet : PooledObject
{
    [SerializeField] private SOBulletConfig config;
    private Rigidbody _rb;

    [SerializeField] private float _canCollectTimer = 0.2f;
    private bool canBeCollected = false;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    void OnEnable()
    {
        canBeCollected = false;
        StartCoroutine(EnableCollectionRoutine());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    public override void OnSpawn()
    {
        base.OnSpawn();

        if (_rb != null)
        {
            _rb.linearVelocity = transform.forward * config.speed;
        }
    }
    private IEnumerator EnableCollectionRoutine()
    {
        yield return new WaitForSeconds(_canCollectTimer);
        canBeCollected = true;
    }
    public void TryCollect(GameObject collector, PlayerAttack attack)
    {
        if (collector.TryGetComponent<PlayerAttack>(out _))
        {
            if (!canBeCollected) return;
        }
        attack.RestoreBullet();
        ReleaseSelf();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.TakeDamage(config.damage);
        }
    }
}
