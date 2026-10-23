using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyConfig", menuName = "Configs/Enemy Config")]
public class SOEnemyConfig : ScriptableObject
{
    public GameObject prefab;
    public float maxHealth = 5f;
    public float fallSpeed = 4f;
    public float damage = 10f;
}
