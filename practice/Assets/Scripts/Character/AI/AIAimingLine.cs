using System.Collections;
using System.Collections.Generic;
using Game.Tools;
using UnityEngine;

public class AIAimingLine : CharacterAimingLine
{
    protected override void Initialize()
    {
        base.Initialize();
        _character = ComponentTools.GetOrLogComponent<AICharacter>(gameObject);
    }
    public override void ObserverUpdate()
    {
        _currentTargetPos = GetTargetPos();
        base.ObserverUpdate();
    }
    private Vector3 GetTargetPos()
    {
        if (target == null) return Vector3.zero;
        
        return target.gameObject.transform.position;
    }
    
}
