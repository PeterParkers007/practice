using System.Collections;
using System.Collections.Generic;
using Game.Tools;
using UnityEngine;

public class Enemy : Character
{
    protected override void StaticInitialize()
    {
        base.StaticInitialize();
        characterMove = ComponentTools.GetOrLogComponent<AIMove>(gameObject);
    }
}
