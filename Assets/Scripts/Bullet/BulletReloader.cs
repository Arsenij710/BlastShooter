using UnityEngine;

public class BulletReloader : MonoBehaviour
{
    [SerializeField] private PlayerAttack _playerAttack;
    private void OnTriggerEnter(Collider other)
    {
        Bullet bullet = other.GetComponentInParent<Bullet>();
        if (bullet != null)
        {
            bullet.TryCollect(gameObject, _playerAttack);
        }
    }
}
