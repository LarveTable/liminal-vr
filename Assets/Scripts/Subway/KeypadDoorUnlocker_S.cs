using System.Collections;
using UnityEngine;

public class KeypadDoorUnlocker_S : MonoBehaviour
{
    [Header("Porte et lumières")]
    public GameObject door1;
    public GameObject door2;
    public GameObject greenLight;
    public GameObject redLight;

    [Header("Code")]
    public string correctCode = "9174";
    private string enteredCode = "";

    [Header("Ouverture des portes")]
    public float slideDistance = 0.2f;  // distance à déplacer (en mètres)
    public float duration = 2f;       // durée de l'animation (secondes)

    // Appelé par le keypad
    public void EnterDigit(string digit)
    {
        enteredCode += digit;
        Debug.Log("Entered Code: " + enteredCode);

        if (enteredCode.Length >= correctCode.Length)
        {
            CheckCode();
        }
    }

    private void CheckCode()
    {
        if (enteredCode == correctCode)
        {
            greenLight.SetActive(true);
            UnlockDoor();
        }
        else
        {
            Debug.Log("Incorrect Code. Try Again.");
            redLight.SetActive(true);
            Invoke("DeactivateRedLight", 1f);
            enteredCode = "";
        }
    }

    private void DeactivateRedLight()
    {
        redLight.SetActive(false);
    }

    private void UnlockDoor()
    {
        Debug.Log("Door Unlocked!");
        StartCoroutine(MoveDoors());
    }

private IEnumerator MoveDoors()
{
    float elapsed = 0f;

    Vector3 startPos1 = door1.transform.position;
    Vector3 startPos2 = door2.transform.position;

    // Déplacement RELATIF sur Z local
    Vector3 targetPos1 = startPos1 + new Vector3(0, 0, slideDistance); // porte1 → -Z
    Vector3 targetPos2 = startPos2 + new Vector3(0, 0, -slideDistance);  // porte2 → +Z

    while (elapsed < duration)
    {
        float t = Mathf.Clamp01(elapsed / duration);
        door1.transform.position = Vector3.Lerp(startPos1, targetPos1, t);
        door2.transform.position = Vector3.Lerp(startPos2, targetPos2, t);

        elapsed += Time.deltaTime;
        yield return null;
    }

    // Assurer la position finale exacte
    door1.transform.position = targetPos1;
    door2.transform.position = targetPos2;
}
}
