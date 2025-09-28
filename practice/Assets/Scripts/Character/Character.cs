using System;
using Game.Interfaces;
using Game.Structs;
using System.Collections.Generic;
using Game.Enums;
using UnityEngine;
using Game.Tools;
public class Character : MonoBehaviour, IObserverUpdate, IDamageable
{
    protected Dictionary<Camp, Stance> relationshipTable = new Dictionary<Camp, Stance>();

    public GameObjectPoolSO deathParticleEffectPool;
    public CharacterPropertyTemplate templateProperty;

    public SpriteRenderer spriteRenderer;
    public CharacterStateMachine stateMachine;
    public CharacterMove characterMove;
    public CharacterAimingLine aimingLine;
    public CharacterDetector characterDetector;
    public VisualHandler visualHandler;

    public Camp characterCamp;
    public Stance stance;

    [HideInInspector]
    public CharacterProperty characterProperty;

    private float _currentHealth;

    public event Action<float> OnHealthChanged;
    public event Action OnDeath;

    // 使用属性来封装字段
    public float CurrentHealth
    {
        get => _currentHealth;
        private set
        {
            if (_currentHealth != value)
            {
                _currentHealth = Mathf.Clamp(value, 0, characterProperty.MaxHealth);
                OnHealthChanged?.Invoke(_currentHealth); // 触发事件
                print(_currentHealth);
                if (_currentHealth <= 0)
                    OnDeath?.Invoke();
            }
        }
    }
    protected virtual void Awake()
    {
        StaticInitialize();
    }
    protected virtual void StaticInitialize()
    {
        characterProperty = CharacterPropertyTemplate.ApplyTemplate(templateProperty);
        characterMove = ComponentTools.GetOrLogComponent<CharacterMove>(gameObject);
        stateMachine = ComponentTools.GetOrLogComponent<CharacterStateMachine>(gameObject);
        spriteRenderer = ComponentTools.GetOrLogComponent<SpriteRenderer>(gameObject);
        visualHandler = ComponentTools.GetOrLogComponent<VisualHandler>(gameObject);
        aimingLine = ComponentTools.GetOrLogComponent<CharacterAimingLine>(gameObject);
    }
    protected virtual void DynamicInitialize()
    {
        _currentHealth = characterProperty.MaxHealth;
        OnDeath += Die;
    }
    public virtual void TakeDamage(float damage,Character culprit)
    {
        CurrentHealth -= damage;
        characterDetector.Hit(culprit);
    }
    protected virtual void Move()
    {
        characterMove.Move();
    }
    public virtual void IdleBehaviour() { }
    protected virtual void Die()
    {
        VisualParticleEffect visualParticleEffect = PoolManager.Instance.Get<VisualParticleEffect>(deathParticleEffectPool);
        visualParticleEffect.transform.position = transform.position;
        Events.PlayerEvent.TriggerPlayerDeath();
    }

    public virtual void ObserverUpdate()
    {
        Move();
    }
    private void OnEnable()
    {
        UpdateManager.RegisterObserverUpdate(this);
        DynamicInitialize();
    }
    private void OnDisable()
    {
        UpdateManager.UnRegisterObserverUpdate(this);
        OnDeath -= Die;
    }
    public void CheckCharacterCamp(Camp targetCamp)
    {
        if(relationshipTable.ContainsKey(targetCamp))
        {
            stance = relationshipTable[targetCamp];
        }
        else
        {
            Stance newStance = CampManager.Instance.CheckCharacterStance(characterCamp, targetCamp);
            stance = newStance;
            relationshipTable.TryAdd(targetCamp, newStance);
        }
    }
}
