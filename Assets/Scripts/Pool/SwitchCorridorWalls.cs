using UnityEngine;

public class SwitchCorridorWalls : MonoBehaviour
{
    [SerializeField] private GameObject[] corridorWalls;

    private GameObject furthestWallOnEnter;

    private AudioSource playerAudioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            float distanceOne = Vector3.Distance(other.transform.position, corridorWalls[0].transform.position);
            float distanceTwo = Vector3.Distance(other.transform.position, corridorWalls[1].transform.position);

            if (distanceOne < distanceTwo)
            {
                corridorWalls[1].transform.Rotate(0f, 180f, 0f, Space.World);
                furthestWallOnEnter = corridorWalls[1];
            }
            else
            {
                corridorWalls[0].transform.Rotate(0f, 180f, 0f, Space.World);
                furthestWallOnEnter = corridorWalls[0];
            }

            // Turn off the player's audio source
            playerAudioSource = other.GetComponent<AudioSource>();
            if (playerAudioSource != null)
            {
                playerAudioSource.enabled = false;
            }
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            furthestWallOnEnter.transform.Rotate(0f, 180f, 0f, Space.World);
            // Turn the player's audio source back on
            if (playerAudioSource != null)
            {
                playerAudioSource.enabled = true;
            }
        }
    }
}
