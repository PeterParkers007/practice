using System.Collections;
using System.Collections.Generic;
using Game.Interfaces;
using Game.Tools;
using UnityEngine;

public class CharacterStateMachine : MonoBehaviour, IStateMachine,IObserverUpdate
{
    private Character _character;
    private ICharacterState _currentState;
    public ICharacterState CurrentState => _currentState;
    private void Start()
    {
        Initialize();   
    }
    private void Initialize()
    {
        _character = ComponentTools.GetOrLogComponent<Character>(gameObject);
    }
    public void ChangeState(ICharacterState newState)
    {
        _currentState?.IExit(_character);
        _currentState = newState;
        _currentState.IEnter(_character);
    }

    public void ObserverUpdate()
    {
        _currentState?.IUpdate(_character);
    }
    private void OnEnable()
    {
        UpdateManager.RegisterObserverUpdate(this);
    }
    private void OnDisable()
    {
        UpdateManager.UnRegisterObserverUpdate(this);
    }
}
