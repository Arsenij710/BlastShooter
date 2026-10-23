using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHealth : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth; 
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI healthText;
    private void OnEnable()
    {
        playerHealth.OnHealthChanged += UpdateHealthSlider;
    }

    private void OnDisable()
    {
        playerHealth.OnHealthChanged -= UpdateHealthSlider;
    }
    private void UpdateHealthSlider(float current, float max)
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = max;
            healthSlider.value = current;
        }
        if (healthText != null)
        {
            healthText.text = $"{current}/{max}";
        }
    }
}
