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
        delayTime = 0.1f; // 玩家瞄准线延迟较小，更灵敏
    }

    public override void ObserverUpdate()
    {
        // 关键：每一帧都更新鼠标位置（实时获取）
        _currentTargetPos = GetMouseWorldPosition();
        // 调用父类逻辑进行绘制（包含平滑计算和碰撞检测）
        base.ObserverUpdate();
    }
}