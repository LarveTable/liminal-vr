using UnityEngine;
using UnityEngine.InputSystem;

public class TurnOnShower : MonoBehaviour
{
    public ParticleSystem particles;
    public InputActionProperty LeftTriggerAction;
    public InputActionProperty RightTriggerAction;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LeftPlayerHand"))
        {
            LeftTriggerAction.action.performed += TriggerPressed;
        }
        else if (other.CompareTag("RightPlayerHand"))
        {
            RightTriggerAction.action.performed += TriggerPressed;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LeftPlayerHand"))
        {
            LeftTriggerAction.action.performed -= TriggerPressed;
        }
        else if (other.CompareTag("RightPlayerHand"))
        {
            RightTriggerAction.action.performed -= TriggerPressed;
        }
    }

    private void TriggerPressed(InputAction.CallbackContext context)
    {
        if (!particles.isPlaying)
        {
            particles.Play();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

