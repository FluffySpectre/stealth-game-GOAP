using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseAction : GOAPAction
{
    private NavMeshAgent agent;      // Reference to the NavMeshAgent

    public override void ResetAction()
    {
        base.ResetAction();
    }

    public override bool CheckProceduralPrecondition(GameObject agent)
    {
        // Only chase if a target is seen
        if (agent.GetComponent<GOAPAgent>().worldState["seesTarget"] == 1)
        {
            return true;
        }
        return false;
    }

    public override void Activate(GameObject agent)
    {
        // Initialize NavMeshAgent
        this.agent = agent.GetComponent<NavMeshAgent>();

        // TODO: Make this more generic
        target = agent.GetComponent<VisionSensor>().Target;
    }

    public override GOAPActionResult Perform(GameObject agent)
    {
        if (this.agent == null)
        {
            Debug.LogError("No NavMeshAgent found");
            return GOAPActionResult.Failed;
        }

        // Set destination
        this.agent.SetDestination(target.transform.position);

        // Check if the agent has caught the target
        if (Vector3.Distance(agent.transform.position, target.transform.position) <= this.agent.stoppingDistance)
        {
            return GOAPActionResult.Completed;
        }

        return GOAPActionResult.Performing;
    }

    void Start()
    {
        // Initialize preconditions and effects for chasing
        preconditions = new Dictionary<string, int>
        {
            { "seesTarget", 1 },
            { "catchTarget", 0 }
            // { "vigilance", 1 } // For example, we might require high vigilance here
        };

        effects = new Dictionary<string, int>
        {
            { "catchTarget", 1 }
        };
    }
}
