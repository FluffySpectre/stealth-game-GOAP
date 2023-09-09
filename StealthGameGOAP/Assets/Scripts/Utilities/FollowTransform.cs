using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    public Transform target;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.position;
    }
}
