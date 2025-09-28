using UnityEngine;
using System.Collections;
public class PooledObject : MonoBehaviour
{
    [System.NonSerialized] public GameObjectPoolSO PoolSO; // ����ʱ��ֵ�������л�

    // �����Լ������Լ�
    public void ReturnToPool()
    {
        if (PoolSO != null)
        {
            PoolManager.Instance.Release(PoolSO, gameObject);
        }
        else
        {
            Destroy(gameObject); // ��ѡ����
        }
    }

    // ��ѡ������ӳٻ��շ���
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