using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Tools
{
    public class CoroutineManager : Singleton<CoroutineManager>
    {
        // �ؼ�������Э�̳����ߣ�MonoBehaviour�����洢��Ӧ�ĵ�ǰЭ��
        // ���ⲻͬ�����Э�̻�����ţ�����A����Ĳ�ֵ����Ӱ��B����Ĳ�ֵ��
        private Dictionary<MonoBehaviour, Coroutine> _objCoroutineMap = new Dictionary<MonoBehaviour, Coroutine>();

        // �ⲿ���ã���ָ��MonoBehaviour������Э�̣��Զ�ֹͣ�ö���ľ�Э��
        public void StartNewCoroutine(MonoBehaviour owner, IEnumerator coroutine)
        {
            // ���ö������������е�Э�̣���ֹͣ
            if (_objCoroutineMap.ContainsKey(owner) && _objCoroutineMap[owner] != null)
            {
                StopCoroutine(_objCoroutineMap[owner]);
            }

            // ������Э�̲�����ӳ���
            Coroutine newCoroutine = StartCoroutine(coroutine);
            _objCoroutineMap[owner] = newCoroutine;
        }

        // ��ѡ���ֶ�ָֹͣ�������Э�̣����糡���л�ǰ����
        public void StopOwnerCoroutine(MonoBehaviour owner)
        {
            if (_objCoroutineMap.ContainsKey(owner) && _objCoroutineMap[owner] != null)
            {
                StopCoroutine(_objCoroutineMap[owner]);
                _objCoroutineMap[owner] = null;
            }
        }
    }
}

