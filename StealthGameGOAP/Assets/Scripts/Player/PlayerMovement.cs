using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputActionAsset inputActions;
    public float movementSpeed = 3f;
    public float movementSpeedCrouched = 1f;
    public float rotationSpeed = 10f;

    private InputAction movementAction, crouchAction;
    private CharacterController charController;

    public bool IsCrouching
    {
        get
        {
            return crouchAction.IsPressed();
            //return isCrouching;
        }
    }
    //private bool isCrouching;

    // Start is called before the first frame update
    void Awake()
    {
        movementAction = inputActions.FindActionMap("gameplay").FindAction("movement");
        crouchAction = inputActions.FindActionMap("gameplay").FindAction("crouch");
        charController = GetComponent<CharacterController>();
    }

    void OnEnable()
    {
        inputActions.FindActionMap("gameplay").Enable();
    }

    void OnDisable()
    {
        inputActions.FindActionMap("gameplay").Disable();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = movementAction.ReadValue<Vector2>();
        Vector3 moveVector = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveVector != Vector3.zero)
        {
            gameObject.transform.rotation = Quaternion.Lerp(Quaternion.LookRotation(gameObject.transform.forward), Quaternion.LookRotation(moveVector), Time.deltaTime * rotationSpeed);
        }

        float moveSpeed = IsCrouching ? movementSpeedCrouched : movementSpeed;
        charController.Move(moveSpeed * Time.deltaTime * moveVector);

        /*if (crouchAction.WasPerformedThisFrame())
        {
            isCrouching = !isCrouching;
        }*/
    }
}
