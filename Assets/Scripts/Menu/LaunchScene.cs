using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LaunchScene : MonoBehaviour
{
    public String sceneToLoad;
    public InputActionProperty LeftTriggerAction;
    public InputActionProperty RightTriggerAction;
    private bool isPlayerInTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LeftPlayerHand"))
        {
            LeftTriggerAction.action.performed += LoadScene;
            isPlayerInTrigger = true;
        }
        else if (other.CompareTag("RightPlayerHand"))
        {
            RightTriggerAction.action.performed += LoadScene;
            isPlayerInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LeftPlayerHand"))
        {
            LeftTriggerAction.action.performed -= LoadScene;
            isPlayerInTrigger = false;
        }
        else if (other.CompareTag("RightPlayerHand"))
        {
            RightTriggerAction.action.performed -= LoadScene;
            isPlayerInTrigger = false;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // 

    private void LoadScene(InputAction.CallbackContext context)
    {
        // Disable further input to prevent multiple scene loads
        LeftTriggerAction.action.performed -= LoadScene;
        RightTriggerAction.action.performed -= LoadScene;
        SceneManager.LoadScene(sceneToLoad);
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the object for visual effect around the global Y axis
        if(!isPlayerInTrigger)
        {
            transform.Rotate(Vector3.up, 30 * Time.deltaTime, Space.World);
        }
    }
}
