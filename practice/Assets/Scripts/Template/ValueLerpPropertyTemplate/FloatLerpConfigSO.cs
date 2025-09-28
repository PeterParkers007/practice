using UnityEngine;
namespace Game.Structs
{
    [CreateAssetMenu(menuName = "Lerp Configs/Float Lerp Config")]
    public class FloatLerpConfigSO : LerpConfigSO
    {
        public float targetValue = 1f;
    }
}
