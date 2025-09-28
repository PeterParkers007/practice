using UnityEngine;
namespace Game.Tools
{
    public static class ComponentTools  // 建议改为静态类，工具类通常不需要实例化
    {
        // 传入一个GameObject，在该对象上查找组件
        public static T GetOrLogComponent<T>(GameObject targetObject) where T : Component
        {
            // 先判断对象是否为空
            if (targetObject == null)
            {
                Debug.LogError("目标对象为空，无法获取组件");
                return null;
            }

            T component = targetObject.GetComponent<T>();
            if (component == null)
            {
                component = targetObject.AddComponent<T>();
                Debug.Log($"在对象 {targetObject.name} 上未找到组件: {typeof(T).Name},"+$"已添加{typeof(T).Name}组件");
            }
            return component;
        }
    }
}
