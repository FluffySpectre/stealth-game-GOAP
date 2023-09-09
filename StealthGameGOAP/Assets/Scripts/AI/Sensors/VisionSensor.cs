using UnityEngine;

public class VisionSensor : MonoBehaviour
{
    public float viewRadius = 5.0f;
    public LayerMask targetMask;

    public GameObject Target
    {
        get
        {
            return target;
        }
    }
    private GameObject target;

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
                // The agent sees a target and saves the spotted target id
                agent.worldState["seesTarget"] = 1;
                this.target = target.gameObject;
                return;
            }
        }

        // The agent does not see a target
        agent.worldState["seesTarget"] = 0;
        this.target = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.25f);
        Gizmos.DrawSphere(transform.position, viewRadius);
        //Gizmos.DrawFrustum(transform.position, 45f, viewRadius, 0.1f, 0.5f);
    }
}
