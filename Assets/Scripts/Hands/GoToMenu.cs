using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GoToMenu : MonoBehaviour
{
    public InputActionProperty menuButtonAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menuButtonAction.action.performed += LoadMenu;
    }

    private void LoadMenu(InputAction.CallbackContext context)
    {
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            SceneManager.LoadScene("Menu");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
