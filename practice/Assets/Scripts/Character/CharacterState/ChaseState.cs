using System.Collections;
using System.Collections.Generic;
using Game.Interfaces;
using UnityEngine;

public class ChaseState : MonoBehaviour, ICharacterState
{
    public Character target;
    public void IEnter(Character character)
    {
        
    }
    public void IUpdate(Character character)
    {
        character.aimingLine.target = target;
        character.characterMove.MoveToCharacter(target);
    }
    public void IExit(Character character)
    {
        
    }
}
