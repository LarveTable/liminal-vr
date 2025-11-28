using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LaunchScene : MonoBehaviour
{
    public String sceneToLoad;
    public InputActionProperty LeftTriggerAction;
    public InputActionProperty RightTriggerAction;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LeftPlayerHand"))
        {
            LeftTriggerAction.action.performed += LoadScene;
        }
        else if (other.CompareTag("RightPlayerHand"))
        {
            RightTriggerAction.action.performed += LoadScene;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LeftPlayerHand"))
        {
            LeftTriggerAction.action.performed -= LoadScene;
        }
        else if (other.CompareTag("RightPlayerHand"))
        {
            RightTriggerAction.action.performed -= LoadScene;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // 

    private void LoadScene(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
