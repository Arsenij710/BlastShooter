using UnityEngine;

[System.Serializable]
public class ShootData
{
    public GameObject bulletPrefab;
    public float fireRate = 2f;
    public float bulletSpeed = 10f;
    public float attackRange = 10f;
}

[CreateAssetMenu(fileName = "NewEnemyConfig", menuName = "Configs/Enemy Config")]
public class SOEnemyConfig : ScriptableObject
{
    [Header("Settings")]
    public GameObject prefab;
    public float maxHealth = 5f;
    public float fallSpeed = 4f;
    public float damage = 10f;

    [Header("HP Bar Settings")]
    public GameObject healthBarPrefab;

    [Header("Shoot Settings")]
    public bool canShoot = false;
    public ShootData shootData;
}
