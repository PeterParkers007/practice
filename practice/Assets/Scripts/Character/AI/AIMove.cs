using System.Collections;
using System.Collections.Generic;
using Game.Tools;
using UnityEngine;

public class AIMove : CharacterMove
{

    public Vector3 targetPos;
    protected override void Initialize()
    {
        _character = ComponentTools.GetOrLogComponent<Character>(gameObject);
    }

    public void SetTarget(Vector3 targetPos)
    {
        this.targetPos = targetPos;
    }

    public override void Move()
    {
        MoveToTargetPos(targetPos);
    }

    
}
