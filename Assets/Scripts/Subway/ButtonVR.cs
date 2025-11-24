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

    Coroutine currentRoutine;

    void Start()
    {
        d1Closed = door1.transform.position;
        d2Closed = door2.transform.position;

        d1Open = d1Closed + new Vector3(0, 0, slideDistance);
        d2Open = d2Closed + new Vector3(0, 0, -slideDistance);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed && !other.CompareTag("NoColision"))
        {
            presser = other.gameObject;
            isPressed = true;

            button.transform.localPosition = new Vector3(0, -0.02f, 0); //-0.01f

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
        // stop animation en cours
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(MoveDoors(d1Open, d2Open));
    }

    public void CloseDoors()
    {
        // stop animation en cours
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(MoveDoors(d1Closed, d2Closed));
    }

    private IEnumerator MoveDoors(Vector3 target1, Vector3 target2)
    {
        Vector3 start1 = door1.transform.position;
        Vector3 start2 = door2.transform.position;

        // Distance totale "théorique"
        float totalDistance = Vector3.Distance(d1Closed, d1Open);

        // Distance restante à parcourir
        float distanceRemaining = Vector3.Distance(start1, target1);

        // Durée proportionnelle
        float dynamicDuration = duration * (distanceRemaining / totalDistance);

        float elapsed = 0f;

        while (elapsed < dynamicDuration)
        {
            float t = elapsed / dynamicDuration;

            door1.transform.position = Vector3.Lerp(start1, target1, t);
            door2.transform.position = Vector3.Lerp(start2, target2, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        door1.transform.position = target1;
        door2.transform.position = target2;
    }
}
