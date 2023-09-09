using UnityEngine;

public class NoiseSensor : MonoBehaviour
{
    public float hearingRadius = 7.0f;
    public LayerMask noiseMask;

    private GOAPAgent agent;

    void Start()
    {
        agent = GetComponent<GOAPAgent>();
    }

    void Update()
    {
        Collider[] noisesHeard = Physics.OverlapSphere(transform.position, hearingRadius, noiseMask);

        if (noisesHeard.Length > 0)
        {
            // The agent hears something
            agent.worldState["hearsNoise"] = 1;
        }
        else
        {
            // The agent does not hear anything
            agent.worldState["hearsNoise"] = 0;
        }
    }
}
