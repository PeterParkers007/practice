using UnityEngine;

public abstract class LerpConfigSO : ScriptableObject
{
    public AnimationCurve curve;
    public float changeSpeed = 1f;
    public bool useUnscaledTime = false;
}