using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GOAPAgent))]
public class GuardAI : MonoBehaviour
{
    private GOAPAgent goapAgent;

    // Start is called before the first frame update
    void Awake()
    {
        goapAgent = GetComponent<GOAPAgent>();
        goapAgent.worldState.Add("seesTarget", 0);
        goapAgent.worldState.Add("hearsNoise", 0);
        goapAgent.worldState.Add("vigilance", 0);

        goapAgent.goalState.Add("catchTarget", 1);
        goapAgent.goalState.Add("vigilance", 0);
    }
}
