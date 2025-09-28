using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Game.Interfaces
{
    public interface ICharacterState
    {
        void IEnter(Character character);
        void IUpdate(Character character);
        void IExit(Character character);
    }
}