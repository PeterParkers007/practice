using Game.Structs;
using UnityEngine;
[CreateAssetMenu(menuName = "Template/Property",fileName ="CharacterPropertyTemplate")]
public class CharacterPropertyTemplate : ScriptableObject
{
    public CharacterProperty templateProperty;
    public static CharacterProperty ApplyTemplate(CharacterPropertyTemplate characterPropertyTemplate)
    {

        return characterPropertyTemplate.templateProperty;
    }
}
