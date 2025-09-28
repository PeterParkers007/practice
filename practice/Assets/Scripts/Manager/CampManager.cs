using System;
using System.Collections;
using System.Collections.Generic;
using Game.Enums;
using UnityEngine;
[Serializable]
public class CampRelationshipMainCard
{
    public Camp campA;
    public Camp campB;

    [Header("相对立场方向")]
    public CampRelationshipDirection relationshipDirection;
    [Header("相对立场")]
    public Stance stance;
}
public class CampManager : Singleton<CampManager>
{
    public List<CampRelationshipMainCard> relationshipSettings;
    public Stance CheckCharacterStance(Camp inspector, Camp inspectee)
    {
        if (inspector == inspectee)
            return Stance.Friendly;

        foreach (var card in relationshipSettings)
        {
            if (inspector == card.campA && inspectee == card.campB)
            {
                return card.stance;
            }
            
            if (card.relationshipDirection == CampRelationshipDirection.Two_way &&
                inspector == card.campB && inspectee == card.campA)
            {
                return card.stance;
            }
        }
        return Stance.Neutral;
    }
}
