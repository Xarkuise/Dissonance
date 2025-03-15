using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public string sceneName; // Scene to load when winning
    public int levelNumber; // Level number for saving progress
    public GameObject hiddenDoor; // Assign the hidden door in the Inspector

    void Start()
    {
        // Ensure the hidden door starts inactive
        if (hiddenDoor != null)
        {
            hiddenDoor.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger.");

            // Show the hidden door when the player enters the area
            if (hiddenDoor != null)
            {
                hiddenDoor.SetActive(true);
                Debug.Log("Hidden door is now visible.");
            }
        }
    }

    public void OnDoorTrigger(Collider other)
    {
        if (other.CompareTag("Player") && hiddenDoor.activeSelf)
        {
            Debug.Log("Player reached the hidden door. Loading next level...");

            // Save progress and load the next level
            PlayerPrefs.SetInt("level", levelNumber);
            PlayerPrefs.Save();
            SceneManager.LoadScene(sceneName);
        }
    }
}
