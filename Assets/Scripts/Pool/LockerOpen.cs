using UnityEngine;

public class LockerOpen : MonoBehaviour
{
    public MonoBehaviour grabScript;

    public GameObject number;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LockerKey"))
        {
            grabScript.enabled = true;
            number.SetActive(true);
            Destroy(other.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
