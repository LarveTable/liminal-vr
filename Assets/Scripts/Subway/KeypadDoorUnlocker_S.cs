using UnityEngine;

public class KeypadDoorUnlocker_S : MonoBehaviour
{
    public GameObject door1; // Reference to the door GameObject
    public GameObject door2;
    public string correctCode = "9174"; // The correct code to unlock the door
    private string enteredCode = ""; // The code entered by the player
    public GameObject greenLight; // Reference to the green light GameObject
    public GameObject redLight; // Reference to the red light GameObject

    public void EnterDigit(string digit)
    {
        enteredCode += digit;
        Debug.Log("Entered Code: " + enteredCode);

        if (enteredCode.Length >= correctCode.Length)
        {
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
        StartCoroutine(MoveDoors());
    }

    // Coroutine to rotate the door
    /*
    private System.Collections.IEnumerator RotateDoor()
    {
         float duration = 2f;
        float elapsed = 0f;

        // Utiliser localRotation si (0,180,0) est la rotation locale dans l'inspector
        Quaternion initial1 = door1.transform.localRotation;
        Quaternion initial2 = door2.transform.localRotation;

        // Rotation relative : ajouter 90° autour du Z local
        Quaternion relative = Quaternion.Euler(0f, 0f, 90f);
        Quaternion target1 = initial1 * relative; // IMPORTANT : multiplication (initial puis relative)
        Quaternion target2 = initial2 * relative; // IMPORTANT : multiplication (initial puis relative)

        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            door1.transform.localRotation = Quaternion.Slerp(initial1, target1, t);
            door2.transform.localRotation = Quaternion.Slerp(initial2, target2, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Assurer la valeur finale exacte
        door1.transform.localRotation = target1;
        door2.transform.localRotation = target2;
    }
    */


    private System.Collections.IEnumerator MoveDoors()
{
    float duration = 2f;
    float elapsed = 0f;

    Vector3 startPos1 = door1.transform.localPosition;
    Vector3 startPos2 = door2.transform.localPosition;

    Vector3 targetPos1 = startPos1 + Vector3.left * 2f;  // porte 1 → gauche
    Vector3 targetPos2 = startPos2 + Vector3.right * 2f; // porte 2 → droite

    while (elapsed < duration)
    {
        float t = Mathf.Clamp01(elapsed / duration);
        door1.transform.localPosition = Vector3.Lerp(startPos1, targetPos1, t);
        door2.transform.localPosition = Vector3.Lerp(startPos2, targetPos2, t);
        elapsed += Time.deltaTime;
        yield return null;
    }

    door1.transform.localPosition = targetPos1;
    door2.transform.localPosition = targetPos2;
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
