using System.Collections;
using UnityEngine;
using Game.Tools;
public class LifeTimer : MonoBehaviour
{
    [Header("生命周期设置")]
    [HideInInspector] public float lifeTime = 3f;
    [HideInInspector] public bool autoStart = true;
    [HideInInspector] public bool returnToPool = true;

    public PooledObject _pooledObject;
    private Coroutine _timerCoroutine;


    public event System.Action OnTimerEnd;

    private void OnEnable()
    {
        if (autoStart) StartTimer();
    }

    private void OnDisable()
    {
        StopTimer();
    }

    public void StartTimer()
    {
        StopTimer();
        _timerCoroutine = StartCoroutine(TimerRoutine());
    }

    public void StartTimerWithDuration(float duration)
    {
        lifeTime = duration;
        StartTimer();
    }

    public void RestartTimer() => StartTimer();

    public void StopTimer()
    {
        if (_timerCoroutine != null)
        {
            StopCoroutine(_timerCoroutine);
            _timerCoroutine = null;
        }
    }

    private IEnumerator TimerRoutine()
    {
        yield return new WaitForSeconds(lifeTime);

        OnTimerEnd?.Invoke();

        if (returnToPool && _pooledObject != null)
        {
            _pooledObject.ReturnToPool();
        }
        else
        {
            Destroy(gameObject);
        }

        _timerCoroutine = null;
    }
}