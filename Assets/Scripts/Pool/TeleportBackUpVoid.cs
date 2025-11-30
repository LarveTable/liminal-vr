using UnityEngine;

public class TeleportBackUpVoid : MonoBehaviour
{
    public GameObject startingPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = startingPoint.transform.position; // Teleport player to backup location
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
