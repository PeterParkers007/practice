using System.Collections;
using System.Collections.Generic;
using Game.Tools;
using UnityEngine;

public class AICharacter : Character
{
    public Character target;

    protected override void StaticInitialize()
    {
        base.StaticInitialize();
        aimingLine = ComponentTools.GetOrLogComponent<AIAimingLine>(gameObject);
        characterMove = ComponentTools.GetOrLogComponent<AIMove>(gameObject);
    }
    protected virtual void Aiming()
    {
        
    }
}
