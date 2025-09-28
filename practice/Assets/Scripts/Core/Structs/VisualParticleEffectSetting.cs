using System;

namespace Game.Structs
{
    [Serializable]
    public struct VisualParticleEffectSetting
    {
        public float lifeTime;
        public bool autoStart;
        public bool returnToPool;
    }
}