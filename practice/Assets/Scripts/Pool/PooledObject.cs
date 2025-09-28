using UnityEngine;
using System.Collections;
public class PooledObject : MonoBehaviour
{
    [System.NonSerialized] public GameObjectPoolSO PoolSO; // 运行时赋值，不序列化

    // 对象自己回收自己
    public void ReturnToPool()
    {
        if (PoolSO != null)
        {
            PoolManager.Instance.Release(PoolSO, gameObject);
        }
        else
        {
            Destroy(gameObject); // 备选方案
        }
    }

    // 可选：添加延迟回收方法
    public void ReturnToPoolAfter(float delay)
    {
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(DelayedReturn(delay));
        }
    }

    private IEnumerator DelayedReturn(float delay)
    {
        yield return new WaitForSeconds(delay);
        ReturnToPool();
    }
}