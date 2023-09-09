using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThrowManager : MonoBehaviour
{
    public Transform target;
    public Transform initTransform;
    public GameObject projectilePrefab;

    public float launchSpeed;
    public float launchAngle = 45f;

    public InputActionAsset inputActions;

    [Header("Trajectory")]
    public LineRenderer lineRenderer;
    public int lineSegmentCount = 20;

    private InputAction throwAction;

    // Start is called before the first frame update
    void Start()
    {
        throwAction = inputActions.FindActionMap("gameplay").FindAction("throw");

        CalculateLaunchSpeed();
    }

    void Update()
    {
        if (throwAction.WasPressedThisFrame())
        {
            Launch();
        }
    }

    void CalculateLaunchSpeed()
    {
        // Distanz zum Ziel berechnen
        Vector3 toTarget = target.position - initTransform.position;
        Vector3 toTargetXZ = toTarget;
        toTargetXZ.y = 0;

        float y = toTarget.y;
        float xz = toTargetXZ.magnitude;

        float g = Physics.gravity.magnitude; // Gravitation
        float angle = launchAngle * Mathf.Deg2Rad; // Winkel in Radiant

        // Formel zur Berechnung der Anfangsgeschwindigkeit
        launchSpeed = Mathf.Sqrt((g * xz * xz) / (2 * (y - xz * Mathf.Tan(angle)) * Mathf.Pow(Mathf.Cos(angle), 2)));
    }

    void Launch()
    {
        // Projektil instanziieren
        GameObject projectile = Instantiate(projectilePrefab, initTransform.position, Quaternion.identity);

        // Geschwindigkeit und Richtung berechnen
        float angle = Mathf.Deg2Rad * launchAngle;
        //Vector3 launchDirection = new Vector3(Mathf.Cos(angle) * target.position.x, Mathf.Sin(angle), Mathf.Cos(angle) * target.position.z);
        Vector3 launchDirection = initTransform.forward;
        launchDirection.Normalize();

        // Geschwindigkeit setzen
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = launchSpeed * launchDirection;
    }
}
