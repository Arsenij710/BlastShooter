using UnityEditor.EditorTools;
using UnityEngine;

public class PooledObject : MonoBehaviour, IPoolable
{
    [HideInInspector] public GameObject prefabKey;
    private bool isReleased = false;

    public virtual void OnSpawn() 
    {
        isReleased = false;
    }
    public virtual void OnDespawn() { }

    public void ReleaseSelf()
    {
        if (isReleased) return;
        isReleased = true;

        if (PoolManager.Instance != null)
        {
            PoolManager.Instance.Despawn(prefabKey, gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
