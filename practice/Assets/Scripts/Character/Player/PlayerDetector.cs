using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : CharacterDetector
{
    protected override void OnTargetDetection()
    {
        base.OnTargetDetection();
        print("²ì¾õ£¡");
    }
}
