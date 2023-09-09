using System.Collections.Generic;
using UnityEngine;

public class GOAPAgent : MonoBehaviour
{
    public HashSet<GOAPAction> availableActions;
    public Dictionary<string, int> worldState = new Dictionary<string, int>();
    public Dictionary<string, int> goalState = new Dictionary<string, int>();

    private GOAPPlanner planner;
    private Queue<GOAPAction> actionQueue;
    private GOAPAction currentAction;

    void Start()
    {
        planner = new GOAPPlanner();
        availableActions = new HashSet<GOAPAction>(GetComponents<GOAPAction>());
    }

    void Update()
    {
        if (actionQueue != null && actionQueue.Count > 0 && currentAction == null)
        {
            // Activate the next action.
            currentAction = actionQueue.Dequeue();
            currentAction.Activate(gameObject);
        }

        if (currentAction != null)
        {
            GOAPAction.GOAPActionResult result = currentAction.Perform(gameObject);

            if (result == GOAPAction.GOAPActionResult.Completed)
            {
                // Action is completed, update world state and plan again.
                foreach (var effect in currentAction.effects)
                {
                    if (!worldState.ContainsKey(effect.Key))
                    {
                        worldState.Add(effect.Key, effect.Value);
                    }
                    else
                    {
                        worldState[effect.Key] = effect.Value;
                    }
                }

                currentAction = null;
                Plan();
            }
            else if (result == GOAPAction.GOAPActionResult.Failed)
            {
                // Action has failed, plan again.
                Plan();
            }
        }

        // TODO: Fix me
        if ((actionQueue == null || actionQueue.Count == 0) && currentAction == null)
        {
            Plan();
        }
    }

    private void Plan()
    {
        actionQueue = planner.Plan(gameObject, availableActions, new Dictionary<string, int>(worldState), goalState);

        if (actionQueue == null)
        {
            // Debug.LogError("Failed to plan");
        }
        else
        {
            Debug.Log("Valid plan found!");
        }
    }
}
