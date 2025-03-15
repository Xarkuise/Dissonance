using UnityEngine;

public class MusicBoxSpawner : MonoBehaviour
{
    public GameObject[] musicBoxes; // Assign multiple music boxes

    private GameObject activeMusicBox;

    void Start()
    {
        if (musicBoxes.Length == 0)
        {
            Debug.LogError("No Music Boxes assigned to the spawner!");
            return;
        }

        // Hide all music boxes at the start
        foreach (GameObject box in musicBoxes)
        {
            box.SetActive(false);
        }

        // Pick a random music box to activate
        int randIndex = Random.Range(0, musicBoxes.Length);
        activeMusicBox = musicBoxes[randIndex];
        activeMusicBox.SetActive(true);

        Debug.Log("Activated Music Box: " + activeMusicBox.name);

        // Ensure the music starts playing
        AudioSource music = activeMusicBox.GetComponent<AudioSource>();
        if (music != null)
        {
            music.Play();
            Debug.Log("Music started for: " + activeMusicBox.name);
        }
        else
        {
            Debug.LogWarning("No AudioSource found on: " + activeMusicBox.name);
        }
    }
}
