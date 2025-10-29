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

    // FixedUpdate is called at a fixed interval and is independent of frame rate
    void FixedUpdate()
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

        // Set the character controller center to match the camera height
        float headHeight = Mathf.Clamp(Camera.main.transform.localPosition.y, 0.5f, 2f);
        characterController.height = headHeight;
        Vector3 newCenter = Vector3.zero;
        newCenter.y = characterController.height / 2;
        newCenter.y += characterController.skinWidth;
        newCenter.x = Camera.main.transform.localPosition.x;
        newCenter.z = Camera.main.transform.localPosition.z;
        characterController.center = newCenter;

        // Apply gravity
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
