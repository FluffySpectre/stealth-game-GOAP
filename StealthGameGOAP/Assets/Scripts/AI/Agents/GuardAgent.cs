using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAgent : GOAPAgent
{
    // Start is called before the first frame update
    void Awake()
    {
        worldState.Add("seesTarget", 0);
        worldState.Add("hearsNoise", 0);
        worldState.Add("vigilance", 0);
        worldState.Add("catchTarget", 0);
        worldState.Add("roomSafe", 0);

        goalState.Add("catchTarget", 1);
        goalState.Add("vigilance", 0);
        // goalState.Add("roomSafe", 1);
    }
}
