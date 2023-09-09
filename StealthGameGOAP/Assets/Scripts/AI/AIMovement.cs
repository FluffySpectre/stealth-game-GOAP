using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    private NavMeshAgent nma;
    private LevelInfo levelInfo;
    private Vector3 currentTargetPosition;

    // Start is called before the first frame update
    void Start()
    {
        nma = GetComponent<NavMeshAgent>();
        levelInfo = FindObjectOfType<LevelInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        PickRandomPositionOnMap();
    }

    public void GoToPosition(Vector3 position)
    {
        nma.SetDestination(position);
    }

    private Vector3 PickRandomPositionOnMap()
    {
        return new Vector3(Random.Range(0.5f, levelInfo.LevelWidth - 0.5f), 0f, Random.Range(0.5f, levelInfo.LevelHeight - 0.5f));
    }
}
