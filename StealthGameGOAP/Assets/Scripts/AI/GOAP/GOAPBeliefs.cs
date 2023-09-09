using System.Collections.Generic;
using UnityEngine;

public class GOAPBeliefs : MonoBehaviour
{
    // Die Überzeugungen bzw. der aktuelle Zustand der Welt
    public Dictionary<string, int> states;

    // Mögliche Ziele für den Agenten
    public Dictionary<string, int> goals;

    private void Awake()
    {
        // Initialisiere Überzeugungen mit einigen Standardwerten
        states = new Dictionary<string, int>
        {
            { "hasEnergy", 5 },
            { "isAlert", 0 },
            { "heardSound", 0 }
        };

        // Initialisiere Ziele
        goals = new Dictionary<string, int>
        {
            { "capturePlayer", 1 }
        };
    }

    public void SetState(string key, int value)
    {
        if (states.ContainsKey(key))
        {
            states[key] = value;
        }
        else
        {
            states.Add(key, value);
        }
    }

    public int GetState(string key)
    {
        if (states.ContainsKey(key))
        {
            return states[key];
        }
        return 0;  // oder einen anderen Standardwert
    }

    public void SetGoal(string key, int value)
    {
        if (goals.ContainsKey(key))
        {
            goals[key] = value;
        }
        else
        {
            goals.Add(key, value);
        }
    }

    public int GetGoal(string key)
    {
        if (goals.ContainsKey(key))
        {
            return goals[key];
        }
        return 0;  // oder einen anderen Standardwert
    }
}
