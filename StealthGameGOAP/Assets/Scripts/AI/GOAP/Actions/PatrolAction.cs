using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolAction : GOAPAction
{
    public Transform[] waypoints;  // The waypoints for patrol
    private int nextWaypoint;      // Index of the next waypoint
    private NavMeshAgent agent;    // Reference to the NavMeshAgent

    public override void ResetAction()
    {
        base.ResetAction();
    }

    public override bool CheckProceduralPrecondition(GameObject agent)
    {
        // We can always patrol, assuming there are waypoints
        if (waypoints.Length > 0)
        {
            return true;
        }
        return false;
    }

    public override void Activate(GameObject agent)
    {
        // Initialize NavMeshAgent
        this.agent = agent.GetComponent<NavMeshAgent>();

        if (waypoints.Length > 0)
        {
            target = waypoints[nextWaypoint].gameObject;
            nextWaypoint = (nextWaypoint + 1) % waypoints.Length;
        }
        else
        {
            Debug.LogError("No waypoints set for patrolling.");
        }
    }

    public override GOAPActionResult Perform(GameObject agent)
    {
        if (this.agent == null)
        {
            Debug.LogError("No NavMeshAgent found");
            return GOAPActionResult.Failed;
        }

        this.agent.SetDestination(target.transform.position);

        // Check if the agent has arrived at the waypoint
        if (Vector3.Distance(agent.transform.position, target.transform.position) <= this.agent.stoppingDistance)
        {
            return GOAPActionResult.Completed;
        }

        return GOAPActionResult.Performing;
    }

    void Start()
    {
        // Initialize preconditions and effects for patrolling
        preconditions = new Dictionary<string, int>
        {
            { "seesTarget", 0 },
            { "hearsNoise", 0 }
        };

        effects = new Dictionary<string, int>
        {
            { "vigilance", 0 }
        };
    }
}
