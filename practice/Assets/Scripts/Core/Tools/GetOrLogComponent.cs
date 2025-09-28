using UnityEngine;
namespace Game.Tools
{
    public static class ComponentTools  // �����Ϊ��̬�࣬������ͨ������Ҫʵ����
    {
        // ����һ��GameObject���ڸö����ϲ������
        public static T GetOrLogComponent<T>(GameObject targetObject) where T : Component
        {
            // ���ж϶����Ƿ�Ϊ��
            if (targetObject == null)
            {
                Debug.LogError("Ŀ�����Ϊ�գ��޷���ȡ���");
                return null;
            }

            T component = targetObject.GetComponent<T>();
            if (component == null)
            {
                component = targetObject.AddComponent<T>();
                Debug.Log($"�ڶ��� {targetObject.name} ��δ�ҵ����: {typeof(T).Name},"+$"�����{typeof(T).Name}���");
            }
            return component;
        }
    }
}
