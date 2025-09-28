using System.Collections;
using System.Collections.Generic;
using Game.Interfaces;
using UnityEngine;

public class IdleState : MonoBehaviour, ICharacterState
{
    public void IEnter(Character character)
    {
        Debug.Log("��������״̬");
    }
    public void IUpdate(Character character)
    {
        character.IdleBehaviour();
    }
    public void IExit(Character character)
    {
        Debug.Log("�˳�����״̬");
    }

    
}
