using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DoNotDestroy : MonoBehaviour
{
    void Awake()
    {
        GameObject[] musicObjs = GameObject.FindGameObjectsWithTag("MenuMusic");
        if (musicObjs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainGameScene")
        {
            GameObject[] musicObjs = GameObject.FindGameObjectsWithTag("MenuMusic");
            foreach (GameObject musicObj in musicObjs)
            {
                AudioSource audioSource = musicObj.GetComponent<AudioSource>();
                if (audioSource != null)
                {
                    audioSource.Stop();
                }
            }
        }
    }
}
