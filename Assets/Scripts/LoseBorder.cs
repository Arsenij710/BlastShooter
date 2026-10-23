using UnityEngine;

public class LoseBorder : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out var enemy))
        {
            GameMenuManager.Instance.ShowGameOver();
            enemy.ReleaseSelf();
        }
    }
}
