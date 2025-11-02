using System;
using UnityEngine;

public class FootStepAudioController : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Le CharacterController du joueur.")]
    public CharacterController characterController;

    [Header("Footstep Settings")]
    public AudioClip[] footstepClips;
    [Tooltip("Distance minimale parcourue entre deux pas.")]
    public float stepDistance = 0.4f; // en mètres
    [Tooltip("Seuil minimal de vitesse pour déclencher des pas.")]
    public float minSpeedThreshold = 0.01f; 

    [Header("Audio Settings")]
    public float volume = 0.8f;
    public bool randomizePitch = true;
    public Vector2 pitchRange = new Vector2(0.9f, 1.1f);

    private AudioSource audioSource;
    private float distanceCounter = 0f;
    private Vector3 lastPosition;

    // small timer to avoid resetting counter on tiny/noisy frame-to-frame deltas
    private float stopTimer = 0f;
    public float stopResetDelay = 0.15f; // seconds to wait before resetting distanceCounter

    void Start()
    {
        if (characterController == null)
            characterController = FindAnyObjectByType<CharacterController>();

        // Création automatique d’un objet AudioSource placé aux pieds
        GameObject footAudio = new GameObject("FootAudioSource");
        footAudio.transform.SetParent(transform);
        footAudio.transform.localPosition = new Vector3(0, -characterController.height / 2f, 0);

        // Configuration de la source audio
        audioSource = footAudio.AddComponent<AudioSource>();
        audioSource.spatialBlend = 1f; // 3D Sound
        audioSource.playOnAwake = false;
        audioSource.rolloffMode = AudioRolloffMode.Linear;
        audioSource.maxDistance = 5f;
        audioSource.loop = false;

        lastPosition = characterController.transform.position;
    }

    void Update()
    {
        // Calcul de la distance horizontale parcourue depuis la dernière frame
        Vector3 currentPosition = characterController.transform.position;
        Vector3 delta = currentPosition - lastPosition;
        delta.y = 0f; // ignore la hauteur

        float horizontalDistance = delta.magnitude;
        lastPosition = currentPosition;

        distanceCounter += horizontalDistance;

        // Vitesse approximative
        float currentSpeed = horizontalDistance / Time.deltaTime;

        if (currentSpeed > minSpeedThreshold && characterController.isGrounded)
        {
            stopTimer = 0f; // reset timer si on bouge
            // Jouer un pas quand on dépasse la distance minimale
            if (distanceCounter >= stepDistance)
            {
                PlayFootstep();
                distanceCounter = 0f;
            }
        }
        else
        {
            // not moving fast enough: wait a short time before resetting the counter
            stopTimer += Time.deltaTime;
            if (stopTimer >= stopResetDelay)
            {
                distanceCounter = 0f;
            }
        }
    }

    void PlayFootstep()
    {
        if (footstepClips.Length == 0) return;

        AudioClip clip = footstepClips[UnityEngine.Random.Range(0, footstepClips.Length)];

        if (randomizePitch)
            audioSource.pitch = UnityEngine.Random.Range(pitchRange.x, pitchRange.y);

        audioSource.PlayOneShot(clip, volume);
    }
}