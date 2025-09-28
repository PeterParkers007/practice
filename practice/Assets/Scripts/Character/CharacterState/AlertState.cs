using System.Collections;
using System.Collections.Generic;
using Game.Interfaces;
using UnityEngine;

public class AlertState : ICharacterState
{
    List<Character> alertTargets;
    public void AddTargetToAlert(Character character)
    {
        alertTargets.Add(character);
    }
    public void RemoveTargetToAlert(Character character)
    {
        alertTargets.Remove(character);
    }
    public void IEnter(Character character)
    {
        //character.characterDetector = 
    }
    public void IUpdate(Character character)
    {

    }
    public void IExit(Character character)
    {
        alertTargets.Clear();
    }

    
}
