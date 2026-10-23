using UnityEngine;

[CreateAssetMenu(fileName = "NewBulletConfig", menuName = "Configs/BulletConfig")]
public class SOBulletConfig : ScriptableObject
{
    public GameObject prefab;
    public float speed = 10f;
    public float damage = 1f;
}
