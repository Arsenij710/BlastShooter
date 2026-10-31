using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class LowHPAudioEffect : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string exposedParameterName = "LowPassCutoff";

    [Header("Cutoff")]
    [SerializeField] private float normalCutoff = 22000f;
    [SerializeField] private float muffledCutoff = 800f;
    [SerializeField] private float transitionDuration = 0.5f;

    [Range(0.1f, 0.9f)]
    [SerializeField] private float lowHpThresholdPercent = 0.25f;

    private bool _isTriggered = false;
    private Coroutine _transitionCoroutine;

    private void Start()
    {
        audioMixer.SetFloat(exposedParameterName, normalCutoff);
    }
    private void OnEnable()
    {
        playerHealth.OnHealthChanged += HandleHealthChanged;
    }

    private void OnDisable()
    {
        playerHealth.OnHealthChanged -= HandleHealthChanged;
    }
    private void HandleHealthChanged(float currentHealth, float maxHealth)
    {
        if (_isTriggered) return;

        float healthPercent = currentHealth / maxHealth;
        bool isLowHp = healthPercent <= lowHpThresholdPercent && healthPercent > 0f;

        if (isLowHp)
        {
            _isTriggered = true;
            SetCutoffSmoothly(muffledCutoff);
        }
    }
    private void SetCutoffSmoothly(float targetValue)
    {
        if (_transitionCoroutine != null)
        {
            StopCoroutine(_transitionCoroutine);
        }
        _transitionCoroutine = StartCoroutine(TransitionRoutine(targetValue));
    }

    private IEnumerator TransitionRoutine(float targetValue)
    {
        audioMixer.GetFloat(exposedParameterName, out float startValue);
        float elapsed = 0f;
        while (elapsed < transitionDuration)
        {
            elapsed += Time.deltaTime;
            float current = Mathf.Lerp(startValue, targetValue, elapsed / transitionDuration);
            audioMixer.SetFloat(exposedParameterName, current);
            yield return null;
        }

        audioMixer.SetFloat(exposedParameterName, targetValue);
    }
}
