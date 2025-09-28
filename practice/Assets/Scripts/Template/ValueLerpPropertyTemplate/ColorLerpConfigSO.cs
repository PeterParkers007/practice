using UnityEngine;
namespace Game.Structs
{
    [CreateAssetMenu(menuName = "Lerp Configs/Color Lerp Config")]
    public class ColorLerpConfigSO : LerpConfigSO
    {
        public Color targetColor = Color.white;
    }
}
