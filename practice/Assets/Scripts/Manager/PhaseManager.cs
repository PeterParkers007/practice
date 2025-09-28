using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : Singleton<PhaseManager>
{
    public Phase currentPhase;
    public bool isPhaseCompleted;
}
