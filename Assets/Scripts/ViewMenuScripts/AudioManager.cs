using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource sfxAudioSource; // For button sound effects
    [SerializeField] private AudioClip buttonClickSound; // Sound for button clicks
    public void PlayButtonClickSound()
    {
        if (sfxAudioSource != null && buttonClickSound != null)
        {
            sfxAudioSource.PlayOneShot(buttonClickSound);
        }
    }
}