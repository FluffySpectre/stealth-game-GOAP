using System.Collections.Generic;
using UnityEngine;

public abstract class GOAPAction : MonoBehaviour
{
    public float cost = 1.0f; // Cost of performing the action
    public GameObject target; // A target GameObject, if needed

    // State conditions that must be met to execute this action.
    public Dictionary<string, int> preconditions;

    // The effects of the action on the world state.
    public Dictionary<string, int> effects;

    public GOAPAction()
    {
        preconditions = new Dictionary<string, int>();
        effects = new Dictionary<string, int>();
    }

    public virtual void ResetAction()
    {
        target = null;
    }

    // Check if the action can be performed in the current context.
    public abstract bool CheckProceduralPrecondition(GameObject agent);

    // Gets called once before the Perform-Step.
    public abstract void Activate(GameObject agent);

    // Execute the action.
    // Return true if the action has been completed.
    public abstract GOAPActionResult Perform(GameObject agent);

    // Check if the action is achievable under the given conditions.
    public bool IsAchievableGiven(Dictionary<string, int> conditions)
    {
        foreach (KeyValuePair<string, int> p in preconditions)
        {
            if (!conditions.ContainsKey(p.Key))
            {
                return false;
            }

            if (conditions[p.Key] < p.Value)
            {
                return false;
            }
        }
        return true;
    }

    public enum GOAPActionResult
    {
        Performing,
        Completed,
        Failed
    }
}
