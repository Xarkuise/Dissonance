using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundAudioSource; // For background music
    [SerializeField] private AudioSource sfxAudioSource; // For button sound effects
    [SerializeField] private AudioClip buttonClickSound; // Sound for button clicks
    [SerializeField] private AudioClip backgroundMusic; // Background music clip

    private void Start()
    {
        // Play the background music at the start
        if (backgroundMusic != null && backgroundAudioSource != null)
        {
            backgroundAudioSource.clip = backgroundMusic;
            backgroundAudioSource.loop = true; // Loop the music
            backgroundAudioSource.Play();
        }
    }

    public void PlayButtonClickSound()
    {
        if (sfxAudioSource != null && buttonClickSound != null)
        {
            sfxAudioSource.PlayOneShot(buttonClickSound);
        }
    }
}