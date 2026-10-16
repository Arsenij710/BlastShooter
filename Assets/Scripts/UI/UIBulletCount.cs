using TMPro;
using UnityEngine;

public class UIBulletCount : MonoBehaviour
{
    [SerializeField] private PlayerAttack _playerAttack;
    [SerializeField] private TextMeshProUGUI _bulletText;
    private void OnEnable()
    {
        PlayerAttack.OnAmmoChanged += UpdateAmmoDisplay;
    }

    private void OnDisable()
    {
        PlayerAttack.OnAmmoChanged -= UpdateAmmoDisplay;
    }
    private void UpdateAmmoDisplay(int current, int max)
    {
        _bulletText.text = $"{_playerAttack.CurrentBullets}/{_playerAttack.MaxBullets}";
    }
}
