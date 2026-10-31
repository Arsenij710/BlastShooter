using UnityEngine;

public class DeathAudio : MonoBehaviour
{
    public static DeathAudio Instance { get; private set; }

    [SerializeField] private PlayerHealth _playerHealth;

    [Header("Settings")]
    [SerializeField] private AudioSource deathAudioSource;
    [SerializeField] private AudioClip deathClip;

    [Header("Back Music")]
    [SerializeField] private AudioSource musicAudioSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void OnEnable()
    {
        _playerHealth.OnDeath += HandleDeath;
    }

    private void OnDisable()
    {
        _playerHealth.OnDeath -= HandleDeath;
    }

    public void HandleDeath()
    {
        if (musicAudioSource != null)
        {
            musicAudioSource.Stop();
        }

        if (deathAudioSource != null && deathClip != null)
        {
            deathAudioSource.PlayOneShot(deathClip);
        }
    }
}
