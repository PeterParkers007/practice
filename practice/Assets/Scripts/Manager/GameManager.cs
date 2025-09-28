using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public struct Phase
{
    public float Duration;
    public List<Character> Enemys;
    //public Effect PhaseEffect;
}
[Serializable]
public struct GameData
{
    public Player player;
    public List<Phase> phases;
    //public GaPTemp gapTemp;
}
public class GameManager : Singleton<GameManager>
{
    private void Initialize()
    {
        PutPlayer();
    }
    private void PutPlayer()
    {

    }
    public static Character TargetCheck()//(Camp targetCamp)
    {
        return new Character();
    }
}
