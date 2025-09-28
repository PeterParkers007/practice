using System;

namespace Game.Structs
{
    [Serializable]
    public struct CharacterProperty
    {
        public float MaxHealth;
        public float Armor;
        public float MovementSpeed;
        public float AttackDamage;
        public float AttackSpeed;
        public float AttackRange;
        public float DetectionRange;
    }
}