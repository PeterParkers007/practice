using System.Collections;
using System.Collections.Generic;
using Game.Tools;
using UnityEngine;

public class Player : Character
{
    public Color DeathColor = Color.black;
    protected override void StaticInitialize()
    {
        base.StaticInitialize();
        characterMove = ComponentTools.GetOrLogComponent<PlayerMove>(gameObject);
        aimingLine = ComponentTools.GetOrLogComponent<PlayerAimingLine>(gameObject);
        characterDetector = ComponentTools.GetOrLogComponent<PlayerDetector>(gameObject);
    }
    protected override void Die()
    {
        base.Die();
        visualHandler.DeathVisual();
    }
    public override void ObserverUpdate()
    {
        base.ObserverUpdate();
        if(Input.GetKeyUp(KeyCode.Space))
        {
            
        }
    }
}
