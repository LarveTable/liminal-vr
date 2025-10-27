using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactors;


public class SwingingArmMotion : MonoBehaviour
{
    [Header("XR Components")]
    // Get the character controller
    public CharacterController characterController;
    // Get the left hand
    public GameObject leftHand, rightHand;
    // Get the XR Ray interactor
    public XRRayInteractor rightRayInteractor;

    [Header("Input")]
    // Get the A button input
    public InputActionReference activateSwingAction;

    // Useful positions
    Vector3 previousPosLeft, previousPosRight, direction;

    [Header("Settings")]
    // Movement speed
    public float speed = 4;

    Vector3 gravity = new Vector3(0, -9.81f, 0);
    bool swingingActive = false;

    void OnEnable()
    {
        activateSwingAction.action.performed += OnSwingActivated;
        activateSwingAction.action.canceled += OnSwingDeactivated;
        SetPreviousPos();
    }

    void OnDisable()
    {
        activateSwingAction.action.performed -= OnSwingActivated;
        activateSwingAction.action.canceled -= OnSwingDeactivated;
    }

    void OnSwingActivated(InputAction.CallbackContext ctx)
    {
        swingingActive = true;
        if (rightRayInteractor != null) rightRayInteractor.enabled = false;
        SetPreviousPos();
    }

    void OnSwingDeactivated(InputAction.CallbackContext ctx)
    {
        swingingActive = false;
        if (rightRayInteractor != null) rightRayInteractor.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (!swingingActive) return;

        // Calculate the velocity of the player hand movement
        Vector3 leftHandVelocity = leftHand.transform.position - previousPosLeft;
        Vector3 rightHandVelocity = rightHand.transform.position - previousPosRight;
        float totalVelocity = +leftHandVelocity.magnitude * 0.8f + rightHandVelocity.magnitude * 0.8f;

        if (totalVelocity >= 0.05f) // If player has swing their hand
        {
            // Getting the direction the player is facing
            direction = Camera.main.transform.forward;

            // Move the player
            characterController.Move(speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up));
        }

        characterController.Move(gravity * Time.deltaTime);
        SetPreviousPos();
    }
    
    // Set the previous positions
    void SetPreviousPos()
    {
        previousPosLeft = leftHand.transform.position;
        previousPosRight = rightHand.transform.position;
    }
}
