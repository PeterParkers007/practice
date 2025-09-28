using UnityEngine.Pool;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pool", menuName = "Tools/Object Pool")]
public class GameObjectPoolSO : ScriptableObject
{
    public GameObject Prefab;
    public int DefaultCapacity = 10;
    public int MaxSize = 100;

    // �����鷽����Ϊ��չ�㣬��ʵ�ּ���
    protected virtual GameObject CreatePooledItem() => null;
    protected virtual void OnTakeFromPool(GameObject obj) { }
    protected virtual void OnReturnedToPool(GameObject obj) { }
    protected virtual void OnDestroyPoolObject(GameObject obj) { }
}