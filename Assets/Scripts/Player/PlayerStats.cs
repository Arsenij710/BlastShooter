using UnityEngine;
using static UnityEngine.Rendering.STP;

public class PlayerStats : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private SOPlayerConfig _config;

    public SOPlayerConfig Config => _config;
}
