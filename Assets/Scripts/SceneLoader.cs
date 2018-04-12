using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    public static SceneLoader Instance { get; private set; } //static singleton

    private void Awake()
    {
        //destroy if conflicting instances
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this; //save singleton instance

        DontDestroyOnLoad(gameObject); //persistent in all scenes
    }

    public void LoadOnClick(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
