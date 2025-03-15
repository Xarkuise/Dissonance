using UnityEngine;

public class MusicBoxProximity : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public AudioSource musicBoxAudio; // Reference to the AudioSource component on the music box
    public float maxVolume = 1.0f; // Maximum volume when far
    public float minVolume = 0.1f; // Minimum volume when close
    public float maxDistance = 20.0f; // Maximum distance at which sound is at maxVolume

    void Start()
    {
        // Try to find the player dynamically if not assigned
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }

        // Ensure AudioSource is assigned and playing
        if (musicBoxAudio == null)
        {
            musicBoxAudio = GetComponent<AudioSource>(); // Try to find it on the same GameObject
        }

        if (musicBoxAudio != null)
        {
            musicBoxAudio.spatialBlend = 1f; // Make sure it's 3D
            musicBoxAudio.rolloffMode = AudioRolloffMode.Linear; // Use linear rolloff for manual volume control
            musicBoxAudio.minDistance = 1f; // Prevents too much volume drop close up
            musicBoxAudio.maxDistance = maxDistance;

            if (!musicBoxAudio.isPlaying)
            {
                musicBoxAudio.Play(); // Ensure it starts playing
            }
        }
    }

    void Update()
    {
        if (player == null || musicBoxAudio == null)
            return;

        float distance = Vector3.Distance(transform.position, player.position);
        float volume = Mathf.Lerp(minVolume, maxVolume, distance / maxDistance);
        musicBoxAudio.volume = Mathf.Clamp(volume, minVolume, maxVolume);
    }
}
