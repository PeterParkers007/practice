using System.Collections;
using System.Collections.Generic;
using Game.Tools;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    public Character _character;
    private void Start()
    {
        Initialize();
    }
    protected virtual void Initialize()
    {
        _character = ComponentTools.GetOrLogComponent<Character>(gameObject);
    }
    protected virtual void Attack()
    {

    }
}
