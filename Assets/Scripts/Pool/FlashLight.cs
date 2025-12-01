using UnityEngine;
using UnityEngine.InputSystem;

public class FlashLight : MonoBehaviour
{
    public InputActionProperty LeftTriggerAction;
    public InputActionProperty RightTriggerAction;
    public GameObject spotLight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LeftPlayerHand"))
        {
            LeftTriggerAction.action.performed += TurnOn;
        }
        else if (other.CompareTag("RightPlayerHand"))
        {
            RightTriggerAction.action.performed += TurnOn;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LeftPlayerHand"))
        {
            LeftTriggerAction.action.performed -= TurnOn;
        }
        else if (other.CompareTag("RightPlayerHand"))
        {
            RightTriggerAction.action.performed -= TurnOn;
        }
    }

    private void TurnOn(InputAction.CallbackContext context)
    {
        Debug.Log("Toggle Flashlight");
        spotLight.SetActive(!spotLight.activeSelf);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
