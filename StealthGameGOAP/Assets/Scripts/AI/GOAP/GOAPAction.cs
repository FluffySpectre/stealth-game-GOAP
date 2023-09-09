using System.Collections.Generic;
using UnityEngine;

public abstract class GOAPAction : MonoBehaviour
{
    public float cost = 1.0f; // Cost of performing the action
    public GameObject target; // A target GameObject, if needed
    public float duration = 0; // How long does this action take?

    // State conditions that must be met to execute this action.
    public Dictionary<string, int> preconditions;

    // The effects of the action on the world state.
    public Dictionary<string, int> effects;

    public GOAPAction()
    {
        preconditions = new Dictionary<string, int>();
        effects = new Dictionary<string, int>();
    }

    public virtual void Reset()
    {
        target = null;
    }

    // Check if the action can be performed in the current context.
    public abstract bool CheckProceduralPrecondition(GameObject agent);

    // Execute the action.
    // Return true if the action has been completed.
    public abstract bool Perform(GameObject agent);

    // Check if the action is completed.
    public abstract bool IsDone();

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
}
