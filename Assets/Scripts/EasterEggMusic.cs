using UnityEngine;
using System.Collections;

public class EasterEggMusic : MonoBehaviour
{
    public AudioSource backgroundMusic; // Assign your background music AudioSource
    private AudioSource iconMusic; // The icon music (Hehehe)

    void Start()
    {
        iconMusic = GetComponent<AudioSource>(); // Get the AudioSource on the EasterEgg
    }

    public void PlayIconMusic()
    {
        if (backgroundMusic.isPlaying)
        {
            backgroundMusic.Pause(); // Pause background music
        }

        iconMusic.Play(); // Play the "Hehehe" sound
        StartCoroutine(WaitForIconMusicToEnd());
    }

    private IEnumerator WaitForIconMusicToEnd()
    {
        yield return new WaitUntil(() => !iconMusic.isPlaying); // Wait until "Hehehe" finishes playing
        backgroundMusic.Play(); // Resume background music
    }
}
