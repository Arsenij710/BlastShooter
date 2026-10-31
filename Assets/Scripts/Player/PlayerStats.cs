using UnityEngine;
public class PlayerStats : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private SOPlayerConfig _config;

    public static PlayerStats Instance { get; private set; }
    public SOPlayerConfig Config => _config;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
}
