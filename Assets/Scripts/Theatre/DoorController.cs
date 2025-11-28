using UnityEngine;

public class DoorController : MonoBehaviour
{

    public float openAngle = 270f;   // Angle Y quand la porte est ouverte
    public float speed = 2f;         // Vitesse d'ouverture
    private bool isOpen = false;

    private Quaternion closedRot;
    private Quaternion openRot;

    private void Start()
    {
        // Rotation fermée = rotation initiale
        closedRot = transform.rotation;

        // Rotation ouverte = rotation initiale + 270° en Y
        openRot = Quaternion.Euler(
            closedRot.eulerAngles.x,
            closedRot.eulerAngles.y + openAngle,
            closedRot.eulerAngles.z
        );
    }

    private void Update()
    {
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            isOpen ? openRot : closedRot,
            Time.deltaTime * speed
        );
    }

    public void OpenDoor() => isOpen = true;
    public void CloseDoor() => isOpen = false;
    
}
