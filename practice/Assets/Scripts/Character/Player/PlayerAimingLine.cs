using System.Collections;
using System.Collections.Generic;
using Game.Tools;
using UnityEngine;

public class PlayerAimingLine : CharacterAimingLine
{
    protected override void Initialize()
    {
        base.Initialize();
        _character = ComponentTools.GetOrLogComponent<Player>(gameObject);
        delayTime = 0.1f; // �����׼���ӳٽ�С��������
    }

    public override void ObserverUpdate()
    {
        // �ؼ���ÿһ֡���������λ�ã�ʵʱ��ȡ��
        _currentTargetPos = GetMouseWorldPosition();
        // ���ø����߼����л��ƣ�����ƽ���������ײ��⣩
        base.ObserverUpdate();
    }
}