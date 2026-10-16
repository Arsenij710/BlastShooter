using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _canCollectTimer = 0.2f;
    private bool canBeCollected = false;
    void Start()
    {
        StartCoroutine(EnableCollectionRoutine());
    }
    private IEnumerator EnableCollectionRoutine()
    {
        yield return new WaitForSeconds(_canCollectTimer);

        canBeCollected = true;
    }
    public void TryCollect(GameObject collector, PlayerAttack attack)
    {
        if (collector.TryGetComponent<PlayerAttack>(out _))
        {
            if (!canBeCollected) return;
        }

        attack.RestoreBullet();
        Destroy(gameObject);
    }
}
