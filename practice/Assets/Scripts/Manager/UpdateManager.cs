using System.Collections.Generic;
using UnityEngine;
using Game.Interfaces;


public class UpdateManager : MonoBehaviour
{
    private static List<IObserverUpdate> _observers = new List<IObserverUpdate>(); // 初始化静态列表
    private static List<IObserverUpdate> _pendingObservers = new List<IObserverUpdate>();

    private static List<IObserverFixedUpdate> _obseverFixeds = new List<IObserverFixedUpdate>();
    private static List<IObserverFixedUpdate> _pendingObserverFixeds = new List<IObserverFixedUpdate>();

    private static int _currentObserverIndex;
    private static int _currentObserverFixedIndex;
    private void Update()
    {
        for (_currentObserverIndex = _observers.Count - 1; _currentObserverIndex >= 0; _currentObserverIndex--)
        {
            _observers[_currentObserverIndex].ObserverUpdate();
        }
        _observers.AddRange(_pendingObservers);
        _pendingObservers.Clear();
    }
    private void FixedUpdate()
    {
        for (_currentObserverFixedIndex = _obseverFixeds.Count - 1; _currentObserverFixedIndex >= 0; _currentObserverFixedIndex--)
        {
            _obseverFixeds[_currentObserverFixedIndex].ObserverFixedUpdate();
        }
        _obseverFixeds.AddRange(_pendingObserverFixeds);
        _pendingObserverFixeds.Clear();
    }
    public static void RegisterObserverUpdate(IObserverUpdate observer)
    {
        _pendingObservers.Add(observer);
    }
    public static void RegisterObserverFixedUpdate(IObserverFixedUpdate obseverFixed)
    {
        _pendingObserverFixeds.Add(obseverFixed);
    }
    public static void UnRegisterObserverUpdate(IObserverUpdate observer)
    {
        _observers.Remove(observer);
        _currentObserverIndex--;
    }
    public static void UnRegisterObserverFixedUpdate(IObserverFixedUpdate obseverFixed)
    {
        _obseverFixeds.Remove(obseverFixed);
        _currentObserverFixedIndex--;

    }
}