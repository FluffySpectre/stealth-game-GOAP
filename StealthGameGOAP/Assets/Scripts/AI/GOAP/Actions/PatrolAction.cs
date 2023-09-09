using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolAction : GOAPAction
{
    public Transform[] waypoints;  // The waypoints for patrol
    private int nextWaypoint;      // Index of the next waypoint
    private NavMeshAgent agent;    // Reference to the NavMeshAgent
    private bool arrived = false;  // Has the agent arrived at the waypoint?

    public override void Reset()
    {
        base.Reset();
        arrived = false;
    }

    public override bool IsDone()
    {
        return arrived;
    }

    public override bool CheckProceduralPrecondition(GameObject agent)
    {
        // We can always patrol, assuming there are waypoints
        if (waypoints.Length > 0)
        {
            target = waypoints[nextWaypoint].gameObject;
            nextWaypoint = (nextWaypoint + 1) % waypoints.Length;
            return true;
        }
        return false;
    }

    public override bool Perform(GameObject agent)
    {
        // Initialize NavMeshAgent and set the destination to the next waypoint
        if (this.agent == null)
        {
            this.agent = agent.GetComponent<NavMeshAgent>();
        }

        if (this.agent == null)
        {
            Debug.LogError("No NavMeshAgent found");
            return false;
        }

        this.agent.SetDestination(target.transform.position);

        // Check if the agent has arrived at the waypoint
        if (!this.agent.pathPending && this.agent.remainingDistance <= this.agent.stoppingDistance)
        {
            arrived = true;
            // Set some effect, e.g., increased vigilance
            effects["vigilance"] = 1;
        }

        return true;
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
