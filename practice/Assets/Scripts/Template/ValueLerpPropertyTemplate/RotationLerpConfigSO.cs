using UnityEngine;
namespace Game.Structs
{
    [CreateAssetMenu(menuName = "Lerp Configs/Rotation Lerp Config")]
    public class RotationLerpConfigSO : LerpConfigSO
    {
        public Vector3 targetEulerAngles = Vector3.zero;
    }
}
