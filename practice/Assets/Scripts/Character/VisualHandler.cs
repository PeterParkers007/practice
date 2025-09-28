using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Tools;
using Game.Interfaces;
using Game.Structs;

public class VisualHandler : MonoBehaviour,IObserverUpdate
{
    public ColorLerpConfigSO deathColorLerpConfig;

    private Character _character;
    private void Start()
    {
        Initialize();
    }
    private void Initialize()
    {
        _character = ComponentTools.GetOrLogComponent<Character>(gameObject);
    }
    public void ObserverUpdate()
    {
        
    }
    public void DeathVisual()
    {
        SpriteColorLerp(_character.spriteRenderer, deathColorLerpConfig);
    }
    private void SpriteColorLerp(SpriteRenderer spriteRenderer,ColorLerpConfigSO configSO)
    {
        Lerps.ValueLerp(spriteRenderer.color,
            configSO.targetColor,
            configSO.changeSpeed,
            configSO.useUnscaledTime,
            this,
            configSO.curve,
            (color)=> spriteRenderer.color = color);
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
