using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleAction : GOAPAction
{
    public override bool CheckProceduralPrecondition(GameObject agent)
    {
        return true;
    }

    public override void Activate(GameObject agent)
    {
    }

    public override GOAPActionResult Perform(GameObject agent)
    {
        return GOAPActionResult.Completed;
    }

    void Start()
    {
        // Initialize preconditions and effects for chasing
        preconditions = new Dictionary<string, int>
        {
            //{ "seesTarget", 0 },
            // { "vigilance", 1 } // For example, we might require high vigilance here
        };

        effects = new Dictionary<string, int>
        {
            //{ "catchTarget", 1 }
        };
    }
}
