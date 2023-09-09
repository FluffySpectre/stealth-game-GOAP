using System.Collections.Generic;
using UnityEngine;

public class GOAPAgent : MonoBehaviour
{
    public HashSet<GOAPAction> availableActions;
    public Dictionary<string, int> worldState;
    public Dictionary<string, int> goalState;

    private GOAPPlanner planner;
    private Queue<GOAPAction> actionQueue;
    private GOAPAction currentAction;

    void Start()
    {
        planner = new GOAPPlanner();
        availableActions = new HashSet<GOAPAction>(GetComponents<GOAPAction>());
        worldState = new Dictionary<string, int>(); // Initialize with the current world state
        goalState = new Dictionary<string, int>(); // Initialize with the current goal state

        // Example world state and goal state
        worldState.Add("hasKey", 0);
        worldState.Add("doorLocked", 1);

        goalState.Add("doorLocked", 0);

        // Create a plan
        Plan();
    }

    void Update()
    {
        if (currentAction != null && currentAction.IsDone())
        {
            // Action is completed, update world state and plan again
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

        if (actionQueue != null && actionQueue.Count > 0 && currentAction == null)
        {
            // Execute the next action
            currentAction = actionQueue.Dequeue();
            if (!currentAction.Perform(gameObject))
            {
                // Action could not be performed, replan
                Plan();
            }
        }
    }

    private void Plan()
    {
        actionQueue = planner.Plan(gameObject, availableActions, new Dictionary<string, int>(worldState), goalState);

        if (actionQueue == null)
        {
            Debug.LogError("Failed to plan");
        }
    }
}
