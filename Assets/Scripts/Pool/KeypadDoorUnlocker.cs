using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeypadDoorUnlocker : MonoBehaviour
{
    public GameObject door; // Reference to the door GameObject
    public string correctCode = "1234"; // The correct code to unlock the door
    private string enteredCode = ""; // The code entered by the player
    public GameObject greenLight; // Reference to the green light GameObject
    public GameObject redLight; // Reference to the red light GameObject
    public TextMeshPro codeDisplay; // Reference to the UI text displaying the entered code
    public string baseDisplayText;

    public void EnterDigit(string digit)
    {
        enteredCode += digit;
        Debug.Log("Entered Code: " + enteredCode);

        // Append the digit to the code display
        codeDisplay.text += digit;

        if (enteredCode.Length >= correctCode.Length)
        {
            codeDisplay.text = baseDisplayText; // Reset display text
            CheckCode();
        }
    }

    // Check if the entered code is correct
    private void CheckCode()
    {
        if (enteredCode == correctCode)
        {
            greenLight.SetActive(true); // Activate green light
            UnlockDoor();
        }
        else
        {
            Debug.Log("Incorrect Code. Try Again.");
            // Activate red light for 1 second
            redLight.SetActive(true);
            Invoke("DeactivateRedLight", 1f);
            enteredCode = ""; // Reset entered code
        }
    }

    // Deactivate the red light
    private void DeactivateRedLight()
    {
        redLight.SetActive(false);
    }


    // Unlock the door
    private void UnlockDoor()
    {
        Debug.Log("Door Unlocked!");
        // Rotate the door smoothly on the Z axis
        StartCoroutine(RotateDoor());
    }

    // Coroutine to rotate the door
    private System.Collections.IEnumerator RotateDoor()
    {
         float duration = 2f;
        float elapsed = 0f;

        // Utiliser localRotation si (0,180,0) est la rotation locale dans l'inspector
        Quaternion initial = door.transform.localRotation;

        // Rotation relative : ajouter 90° autour du Z local
        Quaternion relative = Quaternion.Euler(0f, 0f, 90f);
        Quaternion target = initial * relative; // IMPORTANT : multiplication (initial puis relative)

        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            door.transform.localRotation = Quaternion.Slerp(initial, target, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Assurer la valeur finale exacte
        door.transform.localRotation = target;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        baseDisplayText = codeDisplay.text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
