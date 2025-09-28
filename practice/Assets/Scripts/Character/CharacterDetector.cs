using System;
using System.Collections;
using System.Collections.Generic;
using Game.Interfaces;
using Game.Tools;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CharacterDetector : MonoBehaviour,IObserverUpdate
{
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private LayerMask targetLayers;
    [SerializeField] private float detectionInterval = 0.2f;

    protected Character self;

    private float _lastDetectionTime;
    private Collider2D[] _results = new Collider2D[10]; // 预分配数组

    private void Start()
    {
        Initialize();
    }

    public void ObserverUpdate()
    {
        if (Time.time - _lastDetectionTime < detectionInterval) return;

        _lastDetectionTime = Time.time;
    }
    private void Initialize()
    {
        self = ComponentTools.GetOrLogComponent<Character>(gameObject);
    }
    private void PerformDetection()
    {
        // 使用非分配版本的OverlapCircle，避免GC
        int hitCount = Physics2D.OverlapCircleNonAlloc(
            transform.position,
            detectionRange,
            _results,
            targetLayers
        );

        for (int i = 0; i < hitCount; i++)
        {
            if (_results[i].TryGetComponent<Character>(out Character character))
            {
                Character target = character;
                if (target != self) // 排除自己
                {
                    
                    self.CheckCharacterCamp(target.characterCamp);
                    switch (self.stance)
                    {
                        case Game.Enums.Stance.Neutral:
                            
                            break;
                        case Game.Enums.Stance.Hostile:
                            self.stateMachine?.ChangeState(new ChaseState() { target = target });
                            break;
                        case Game.Enums.Stance.Friendly:

                            break;
                    }
                    OnTargetDetection();
                    Debug.Log("检测到角色:" + target.name);
                }
            }
        }
    }
    public void Hit(Character character)
    {
        self.CheckCharacterCamp(character.characterCamp);
        switch (self.stance)
        {
            case Game.Enums.Stance.Neutral:
                self.stateMachine?.ChangeState(new ChaseState() { target = character });
                break;
            case Game.Enums.Stance.Hostile:
                self.stateMachine?.ChangeState(new ChaseState() { target = character });
                break;
        }
    }
    protected virtual void OnTargetDetection() { }
    private void OnEnable()
    {
        UpdateManager.RegisterObserverUpdate(this);
    }
    private void OnDisable()
    {
        UpdateManager.UnRegisterObserverUpdate(this);
    }
}
