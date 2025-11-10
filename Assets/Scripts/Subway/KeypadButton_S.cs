using UnityEngine;
using UnityEngine.InputSystem;

public class KeypadButton_S : MonoBehaviour
{
    public KeypadDoorUnlocker_S keypad; // assigné dans l'inspecteur
    private string digit;
    public InputActionProperty LeftTriggerAction;
    public InputActionProperty RightTriggerAction;

    void HighlightButton()
    {
        var renderer = GetComponent<Renderer>();
        var color = renderer.material.color;
        color.a = 0.15f; // Set alpha to 15%
        renderer.material.color = color;
    }

    void UnhighlightButton()
    {
        var renderer = GetComponent<Renderer>();
        var color = renderer.material.color;
        color.a = 0f; // Set alpha to 0%
        renderer.material.color = color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LeftKeypadCollider"))
        {
            HighlightButton();
            LeftTriggerAction.action.performed += TriggerPressed;
        }
        else if (other.CompareTag("RightKeypadCollider"))
        {
            HighlightButton();
            RightTriggerAction.action.performed += TriggerPressed;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LeftKeypadCollider"))
        {
            UnhighlightButton();
            LeftTriggerAction.action.performed -= TriggerPressed;
        }
        else if (other.CompareTag("RightKeypadCollider"))
        {
            UnhighlightButton();
            RightTriggerAction.action.performed -= TriggerPressed;
        }
    }

    private void TriggerPressed(InputAction.CallbackContext context)
    {
        keypad.EnterDigit(digit);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        digit = gameObject.name; // Because the button's name is the digit it represents
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
