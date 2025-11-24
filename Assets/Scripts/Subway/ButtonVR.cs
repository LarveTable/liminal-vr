using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class ButtonVR : MonoBehaviour
{
    public GameObject button;
    public UnityEvent onPress;
    public UnityEvent onRelease;
    GameObject presser;

    bool isPressed = false;

    public GameObject door1;
    public GameObject door2;

    public float slideDistance = 0.2f; 
    public float duration = 2f;

    Vector3 d1Closed;
    Vector3 d2Closed;
    Vector3 d1Open;
    Vector3 d2Open;

    void Start()
    {
        // sauvegarde de la position initiale (fermée)
        d1Closed = door1.transform.position;
        d2Closed = door2.transform.position;

        // position ouverte
        d1Open = d1Closed + new Vector3(0, 0, slideDistance);
        d2Open = d2Closed + new Vector3(0, 0, -slideDistance);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed && !other.CompareTag("NoColision"))
        {
            presser = other.gameObject;
            isPressed = true;

            // animation bouton visuelle
            button.transform.localPosition = new Vector3(0, 0.003f, 0);

            onPress.Invoke();
            OpenDoors();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == presser)
        {
            presser = null;
            isPressed = false;

            button.transform.localPosition = new Vector3(0, 0.015f, 0);

            onRelease.Invoke();
            CloseDoors();
        }
    }

    public void OpenDoors()
    {
            StartCoroutine(MoveDoors(d1Open, d2Open));
    }

    public void CloseDoors()
    {
            StartCoroutine(MoveDoors(d1Closed, d2Closed));
    }

    private IEnumerator MoveDoors(Vector3 target1, Vector3 target2)
    {

        float elapsed = 0f;

        Vector3 start1 = door1.transform.position;
        Vector3 start2 = door2.transform.position;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            door1.transform.position = Vector3.Lerp(start1, target1, t);
            door2.transform.position = Vector3.Lerp(start2, target2, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        door1.transform.position = target1;
        door2.transform.position = target2;

    }
}
