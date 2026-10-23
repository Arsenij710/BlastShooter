using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerConfig", menuName = "Configs/PlayerConfig")]
public class SOPlayerConfig : ScriptableObject
{
    [Header("Health")]
    public float maxHealth = 100f;

    [Header("Movement")]
    public float moveSpeed = 6f;
    public float rotationSpeed = 720;

    [Header("Attack")]
    public int maxBullets;
    public SOBulletConfig bulletConfig;
}
