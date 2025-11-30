using UnityEngine;

public class NerfVoidRoom : MonoBehaviour
{
    public GameObject flashlight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Get the flashlight's spot light
            Light lightComponent = flashlight.GetComponentInChildren<Light>();
            if (lightComponent != null)
            {
                // Reduce the range to simulate darkness
                lightComponent.range = 4f; // Adjust this value as needed
                // Reduce intensity
                lightComponent.intensity = 1f; // Adjust this value as needed
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Get the flashlight's spot light
            Light lightComponent = flashlight.GetComponentInChildren<Light>();
            if (lightComponent != null)
            {
                // Restore the range
                lightComponent.range = 13f; // Original value
                // Restore intensity
                lightComponent.intensity = 3f; // Original value
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
