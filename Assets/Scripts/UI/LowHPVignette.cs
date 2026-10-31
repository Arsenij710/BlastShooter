using System.Collections;
using UnityEngine;

public class LowHPVignette : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private PlayerHealth playerHealth;

    [Header("Settings")]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField, Range(0f, 1f)] private float hpThreshold = 0.25f;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float maxAlpha = 0.7f;

    [Header("Pulse")]
    [SerializeField] private float pulseSpeed = 5f;
    [SerializeField] private float pulseAmount = 0.15f;

    private Coroutine _fadeCoroutine;
    private bool _isTriggered = false;
    private void Awake()
    {
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 0f;
    }
    private void OnEnable()
    {
        playerHealth.OnHealthChanged += UpdateHealth;
    }
    private void OnDisable()
    {
        playerHealth.OnHealthChanged -= UpdateHealth;
    }
    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        if (_isTriggered) return;

        float hpPercent = currentHealth / maxHealth;

        if (hpPercent <= hpThreshold)
        {
            _isTriggered = true;

            if (_fadeCoroutine != null)
            {
                StopCoroutine(_fadeCoroutine);
            }
            _fadeCoroutine = StartCoroutine(TriggerRoutine());
        }
    }
    private IEnumerator TriggerRoutine()
    {
        float elapsedTime = 0f;
        float startAlpha = canvasGroup.alpha;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, maxAlpha, elapsedTime / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = maxAlpha;

        while (true)
        {
            float pulse = Mathf.Sin(Time.unscaledTime * pulseSpeed) * pulseAmount;
            canvasGroup.alpha = Mathf.Clamp(maxAlpha + pulse, 0f, 1f);
            yield return null;
        }
        
    }
}
