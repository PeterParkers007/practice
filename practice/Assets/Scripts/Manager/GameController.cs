using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<GameObject> Managers;
    void Initialize()
    {
        foreach (GameObject manager in Managers)
        {
            Instantiate(manager);
        }
    }
    private void Awake()
    {
        Initialize();   
    }
}
