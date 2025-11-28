using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public DoorController door;      // ta porte
    public string targetTag = "OpenDoor_Theatre";  // le tag de l’objet à détecter

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("OpenDoor_Theatre"))
        {
            Debug.Log("door_opening");
            door.OpenDoor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("OpenDoor_Theatre"))
        {
            door.CloseDoor();
        }
    }
}
