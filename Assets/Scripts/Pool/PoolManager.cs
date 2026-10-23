using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    private Dictionary<GameObject, IObjectPool<GameObject>> pools = new Dictionary<GameObject, IObjectPool<GameObject>>();
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    public GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        if (!pools.ContainsKey(prefab))
        {
            var pool = new ObjectPool<GameObject>(
                createFunc: () => {
                    GameObject obj = Instantiate(prefab, gameObject.transform);
                    var pooledObj = obj.GetComponent<PooledObject>();
                    if (pooledObj != null) pooledObj.prefabKey = prefab;
                    return obj;
                },
                actionOnGet: (obj) => obj.SetActive(true),
                actionOnRelease: (obj) => obj.SetActive(false),
                actionOnDestroy: (obj) => Destroy(obj),
                collectionCheck: true,
                defaultCapacity: 20,
                maxSize: 100
            );
            pools.Add(prefab, pool);
        }

        GameObject instance = pools[prefab].Get();
        instance.transform.position = position;
        instance.transform.rotation = rotation;

        if (instance.TryGetComponent<IPoolable>(out var poolable))
        {
            poolable.OnSpawn();
        }

        return instance;
    }
    public void Despawn(GameObject prefabKey, GameObject obj)
    {
        if (obj.TryGetComponent<IPoolable>(out var poolable))
        {
            poolable.OnDespawn();
        }

        if (pools.ContainsKey(prefabKey))
        {
            pools[prefabKey].Release(obj);
        }
        else
        {
            Destroy(obj);
        }
    }
}
