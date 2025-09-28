using System.Collections;
using UnityEngine;
using Game.Tools;
using Game.Structs;
using System;

public class VisualParticleEffect : MonoBehaviour
{
    
    public VisualParticleEffectSetting _setting;

    private PooledObject _pooledObject;
    private LifeTimer _lifeTimer;
    private ParticleSystem _particleEffect;

    private void Awake()
    {
        // 在Awake中初始化，确保OnEnable执行时已经有值
        Initialize();
    }
    protected void Initialize()
    {
        _pooledObject = ComponentTools.GetOrLogComponent<PooledObject>(gameObject);
        _lifeTimer = ComponentTools.GetOrLogComponent<LifeTimer>(gameObject);
        _particleEffect = ComponentTools.GetOrLogComponent<ParticleSystem>(gameObject);
        ApplySetting(_setting);
    }
    private void ApplySetting(VisualParticleEffectSetting setting)
    {
        _lifeTimer.lifeTime = setting.lifeTime;
        _lifeTimer.autoStart = setting.autoStart;
        _lifeTimer.returnToPool = setting.returnToPool;
        _lifeTimer._pooledObject = _pooledObject;
    }
}