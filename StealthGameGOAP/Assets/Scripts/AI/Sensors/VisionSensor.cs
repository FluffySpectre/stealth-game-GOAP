using UnityEngine;

public class VisionSensor : MonoBehaviour
{
    public float viewRadius = 5.0f;
    public LayerMask targetMask;

    private GOAPAgent agent;

    void Start()
    {
        agent = GetComponent<GOAPAgent>();
    }

    void Update()
    {
        Collider[] targetsInView = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        foreach (Collider target in targetsInView)
        {
            Vector3 dirToTarget = (target.transform.position - transform.position).normalized;
            float angle = Vector3.Angle(transform.forward, dirToTarget);

            if (angle < 45.0f)
            {
                // The agent sees a target
                agent.worldState["seesTarget"] = 1;
                return;
            }
        }

        // The agent does not see a target
        agent.worldState["seesTarget"] = 0;
    }
}
