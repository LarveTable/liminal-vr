using UnityEngine;

public class SecretBall : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If the ball hasn't moved
        if (GetComponent<Rigidbody>().linearVelocity.magnitude < 0.1f)
        {
            // Rotate the ball slowly around the Y axis
            transform.Rotate(Vector3.up, 30 * Time.deltaTime, Space.World);
        }
        else
        {
            // Disable this script
            this.enabled = false;
        }
    }
}
