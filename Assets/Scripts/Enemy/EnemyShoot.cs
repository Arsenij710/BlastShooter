using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private SoundPlayer _shootAudio;
    
    private Transform _playerTransform;
    private Enemy _enemy;
    private SOEnemyConfig config => _enemy.Config;
    private float fireTimer;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }
    private void Start()
    {
        _playerTransform = PlayerStats.Instance.transform;
    }
    private void Update()
    {
        if (config == null || _playerTransform == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, _playerTransform.position);

        if (distanceToPlayer <= config.shootData.attackRange)
        {
            fireTimer += Time.deltaTime;

            if (fireTimer >= config.shootData.fireRate)
            {
                fireTimer = 0f;
                Shoot();
            }
        }
    }
    private void Shoot()
    {
        if (config.shootData.bulletPrefab == null) return;

        Vector3 spawnPos = firePoint.position;

        Vector3 directionToPlayer = (_playerTransform.position - spawnPos).normalized;
        Quaternion spawnRotation = Quaternion.LookRotation(directionToPlayer);

        GameObject bulletObj = PoolManager.Instance.Spawn(config.shootData.bulletPrefab, spawnPos, spawnRotation);
        _shootAudio.PlaySound();
    }
}
