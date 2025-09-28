using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : Singleton<PoolManager>
{
    private Dictionary<GameObjectPoolSO, IObjectPool<GameObject>> _pools = new();

    public T Get<T>(GameObjectPoolSO poolSO) where T : Component
    {
        GameObject obj = Get(poolSO);
        return obj.GetComponent<T>();
    }

    public GameObject Get(GameObjectPoolSO poolSO)
    {
        if (!_pools.TryGetValue(poolSO, out var pool))
        {
            // 🎯 这里调用CreatePooledItem创建新池！
            pool = CreateNewPool(poolSO);
            _pools[poolSO] = pool;
        }
        return pool.Get(); // 这里会触发池子的createFunc，也就是调用CreatePooledItem
    }

    public void Release(GameObjectPoolSO poolSO, GameObject obj)
    {
        if (_pools.TryGetValue(poolSO, out var pool))
        {
            pool.Release(obj);
        }
        else
        {
            Debug.LogWarning($"尝试释放未初始化的池子: {poolSO.name}");
            Destroy(obj);
        }
    }
    private GameObject CreatePooledItem(GameObjectPoolSO poolSO)
    {
        GameObject obj = Instantiate(poolSO.Prefab,transform);
        obj.SetActive(false);

        // 获取或添加PooledObject组件，并注入PoolSO引用
        PooledObject pooledObject = obj.GetComponent<PooledObject>();
        if (pooledObject == null)
        {
            pooledObject = obj.AddComponent<PooledObject>();
        }
        pooledObject.PoolSO = poolSO; // 关键：让对象知道自己属于哪个池

        return obj;
    }

    // 创建新对象池 - 内部方法
    private IObjectPool<GameObject> CreateNewPool(GameObjectPoolSO poolSO)
    {
        return new ObjectPool<GameObject>(
            createFunc: () => CreatePooledItem(poolSO),
            actionOnGet: (obj) => obj.SetActive(true),  
            actionOnRelease: (obj) => obj.SetActive(false), 
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: poolSO.DefaultCapacity,
            maxSize: poolSO.MaxSize
        );
    }
}