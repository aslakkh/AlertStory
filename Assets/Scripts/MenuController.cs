using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

    private SceneLoader sceneLoader;

	// Use this for initialization
	void Start () {
        sceneLoader = GameObject.FindObjectOfType<SceneLoader>();
	}
	
	public void LoadAppSettingsScene()
    {
        sceneLoader.LoadAppSettingsScene();
    }

    public void LoadMainMenuScene()
    {
        sceneLoader.LoadMainMenu();
    }

    public void Quit()
    {
        sceneLoader.Quit();
    }

    public void LoadInstructionsScene()
    {
        sceneLoader.LoadInstructionsScene();
    }
}
