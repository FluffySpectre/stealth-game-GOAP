using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayWorldState : MonoBehaviour
{
    public GOAPAgent goapAgent;
    private TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        text.text = "World State:\n" + GetWorldStateString() + "\n\n";
        text.text += "Goal State:\n" + GetGoalStateString();
    }

    private string GetWorldStateString()
    {
        string str = "";
        foreach (KeyValuePair<string, int> kvp in goapAgent.worldState)
        {
            str += $"{kvp.Key}={kvp.Value}\n";
        }
        return str;
    } 

    private string GetGoalStateString()
    {
        string str = "";
        foreach (KeyValuePair<string, int> kvp in goapAgent.goalState)
        {
            str += $"{kvp.Key}={kvp.Value}\n";
        }
        return str;
    }
}
