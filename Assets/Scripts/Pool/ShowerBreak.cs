using UnityEngine;

public class ShowerBreak : MonoBehaviour
{

    public ParticleSystem shower1;
    public ParticleSystem shower2;
    public ParticleSystem shower3;
    public ParticleSystem shower4;
    public ParticleSystem shower5;
    public ParticleSystem shower6;
    public ParticleSystem leak1;
    public ParticleSystem leak2;
    public GameObject fakePipe;
    public GameObject key;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shower1.isPlaying == true &&
            shower2.isPlaying == true &&
            shower3.isPlaying == true &&
            shower4.isPlaying == true &&
            shower5.isPlaying == true &&
            shower6.isPlaying == true)
        {
            gameObject.SetActive(false);
            fakePipe.SetActive(true);
            leak1.Stop();
            leak2.Play();
            key.SetActive(true);
        }
    }
}
