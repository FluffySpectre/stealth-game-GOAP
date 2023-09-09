using System.Collections.Generic;
using UnityEngine;

public class GOAPPlanner
{
    public Queue<GOAPAction> Plan(GameObject agent, HashSet<GOAPAction> availableActions, Dictionary<string, int> worldState, Dictionary<string, int> goal)
    {
        // Reset all actions
        foreach (GOAPAction a in availableActions)
        {
            a.ResetAction();
        }

        // Check which actions can run using their CheckProceduralPrecondition
        HashSet<GOAPAction> usableActions = new HashSet<GOAPAction>();
        foreach (GOAPAction a in availableActions)
        {
            if (a.CheckProceduralPrecondition(agent))
            {
                usableActions.Add(a);
            }
        }

        // Create leaves and set up the starting node
        List<Node> leaves = new List<Node>();
        Node start = new Node(null, 0, worldState, null);

        // Start planning
        bool success = BuildGraph(start, leaves, usableActions, goal);

        if (!success)
        {
            // Debug.LogError("NO PLAN");
            return null;
        }

        // Get the best leaf and build the action sequence
        Node bestLeaf = null;
        foreach (Node leaf in leaves)
        {
            if (bestLeaf == null || leaf.cost < bestLeaf.cost)
            {
                bestLeaf = leaf;
            }
        }

        List<GOAPAction> result = new List<GOAPAction>();
        Node n = bestLeaf;
        while (n != null)
        {
            if (n.action != null)
            {
                result.Insert(0, n.action); // insert the action in the front
            }
            n = n.parent;
        }

        // Convert the result to a queue and return
        Queue<GOAPAction> actionQueue = new Queue<GOAPAction>();
        foreach (GOAPAction a in result)
        {
            actionQueue.Enqueue(a);
        }

        return actionQueue;
    }

    private bool BuildGraph(Node parent, List<Node> leaves, HashSet<GOAPAction> usableActions, Dictionary<string, int> goal)
    {
        bool foundOne = false;

        foreach (GOAPAction action in usableActions)
        {
            if (action.IsAchievableGiven(parent.state))
            {
                Dictionary<string, int> currentState = new Dictionary<string, int>(parent.state);
                foreach (KeyValuePair<string, int> effect in action.effects)
                {
                    if (!currentState.ContainsKey(effect.Key))
                    {
                        currentState.Add(effect.Key, effect.Value);
                    }
                }

                Node node = new Node(parent, parent.cost + action.cost, currentState, action);

                if (IsGoalMet(goal, currentState))
                {
                    leaves.Add(node);
                    foundOne = true;
                }
                else
                {
                    HashSet<GOAPAction> subset = ActionSubset(usableActions, action);
                    bool found = BuildGraph(node, leaves, subset, goal);
                    if (found)
                        foundOne = true;
                }
            }
        }

        return foundOne;
    }

    private bool IsGoalMet(Dictionary<string, int> goal, Dictionary<string, int> state)
    {
        foreach (KeyValuePair<string, int> g in goal)
        {
            if (!state.ContainsKey(g.Key) || state[g.Key] < g.Value)
            {
                return false;
            }
        }

        return true;
    }

    private HashSet<GOAPAction> ActionSubset(HashSet<GOAPAction> actions, GOAPAction removeMe)
    {
        HashSet<GOAPAction> subset = new HashSet<GOAPAction>();
        foreach (GOAPAction a in actions)
        {
            if (!a.Equals(removeMe))
            {
                subset.Add(a);
            }
        }

        return subset;
    }

    private class Node
    {
        public Node parent;
        public float cost;
        public Dictionary<string, int> state;
        public GOAPAction action;

        public Node(Node parent, float cost, Dictionary<string, int> state, GOAPAction action)
        {
            this.parent = parent;
            this.cost = cost;
            this.state = new Dictionary<string, int>(state);
            this.action = action;
        }
    }
}
