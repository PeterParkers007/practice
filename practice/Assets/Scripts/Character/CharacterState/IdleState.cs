using System.Collections;
using System.Collections.Generic;
using Game.Interfaces;
using UnityEngine;

public class IdleState : MonoBehaviour, ICharacterState
{
    public void IEnter(Character character)
    {
        Debug.Log("进入闲游状态");
    }
    public void IUpdate(Character character)
    {
        character.IdleBehaviour();
    }
    public void IExit(Character character)
    {
        Debug.Log("退出闲游状态");
    }

    
}
