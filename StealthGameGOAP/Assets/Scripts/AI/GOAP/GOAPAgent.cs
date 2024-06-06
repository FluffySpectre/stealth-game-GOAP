using System.Collections.Generic;
using UnityEngine;

public class GOAPAgent : MonoBehaviour
{
    public HashSet<GOAPAction> availableActions;
    public Dictionary<string, int> worldState = new Dictionary<string, int>();
    public Dictionary<string, int> goalState = new Dictionary<string, int>();

    private GOAPPlanner planner;
    private Queue<GOAPAction> actionQueue;

    public GOAPAction CurrentAction
    {
        get
        {
            return currentAction;
        }
    }
    private GOAPAction currentAction;

    // Initialization
    void Start()
    {
        planner = new GOAPPlanner();
        availableActions = new HashSet<GOAPAction>(GetComponents<GOAPAction>());
    }

    // Update loop
    void Update()
    {
        // If there are no actions planned or in progress
        // plan again
        if (!HasActionsQueued() && currentAction == null)
        {
            Plan();
        }
        
        // If we have actions queued, but none currently executing
        if (HasActionsQueued() && currentAction == null)
        {
            ActivateNextAction();
        }

        // If we have an action in progress
        if (currentAction != null)
        {
            EvaluateCurrentAction();
        }
    }

    private void ActivateNextAction()
    {
        currentAction = actionQueue.Dequeue();
        currentAction.Activate(gameObject);
    }

    private void EvaluateCurrentAction()
    {
        GOAPAction.GOAPActionResult result = currentAction.Perform(gameObject);

        if (result == GOAPAction.GOAPActionResult.Completed)
        {
            UpdateWorldState();
            currentAction = null;
            Plan();
        }
        else if (result == GOAPAction.GOAPActionResult.Failed)
        {
            currentAction = null;
            Plan();
        }
    }

    private void UpdateWorldState()
    {
        foreach (var effect in currentAction.effects)
        {
            worldState[effect.Key] = effect.Value;
        }
    }

    private bool HasActionsQueued()
    {
        return actionQueue != null && actionQueue.Count > 0;
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
