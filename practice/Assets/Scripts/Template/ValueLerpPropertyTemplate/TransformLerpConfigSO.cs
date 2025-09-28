using UnityEngine;
namespace Game.Structs
{
    [CreateAssetMenu(menuName = "Lerp Configs/Transform Lerp Config")]
    public class TransformLerpConfigSO : LerpConfigSO
    {
        public Vector3 targetPosition = Vector3.zero;
    }
}
