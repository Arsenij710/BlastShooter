using System;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private PlayerStats _stats;
    private float currentHealth;
    private SOPlayerConfig config => _stats.Config;
    private float MaxHealth => config.maxHealth;

    public event Action<float, float> OnHealthChanged;
    public event Action OnDeath;

    private CinemachineImpulseSource _impulseSource;
    private void Awake()
    {
        _stats = GetComponent<PlayerStats>();
        _impulseSource = GetComponent<CinemachineImpulseSource>();
        currentHealth = config.maxHealth;
    }
    private void Start()
    {
        OnHealthChanged?.Invoke(currentHealth, MaxHealth);
    }
    public void TakeDamage(float amount)
    {
        if (currentHealth <= 0f) return;

        _impulseSource.GenerateImpulse();

        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0f);

        OnHealthChanged?.Invoke(currentHealth, MaxHealth);

        if (currentHealth <= 0f)
        {
            Die();
        }
    }
    private void Die()
    {
        OnDeath?.Invoke();
    }
}
