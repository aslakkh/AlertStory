using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//singleton sceneloader class, used for loading scenes
//call LoadOnClick to load scene
public class SceneLoader : MonoBehaviour
{

    public int firstDaySceneIndex; //index of first day scene in build settings
    public int appSettingsSceneIndex; //index of app settings scene
    public int endSceneIndex; //index of end scene in build settings
    public int instructionsSceneIndex;

    public int currentDayIndex; //index of current day in build settings

    public static SceneLoader Instance { get; private set; } //static singleton

    private void Awake()
    {
        //destroy if conflicting instances
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        if(Instance == null)
        {
            Instance = GameObject.FindObjectOfType<SceneLoader>();
            if(Instance == null)
            {
                Instance = this; //save singleton instance
            }
        }
        

        DontDestroyOnLoad(gameObject); //persistent in all scenes
    }


    //loads scene with buildsetting index == int scene
    public void LoadOnClick(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    //load first day scene. Index of this scene must be specified by public field firstDaySceneIndex
    public void LoadFirstDayScene()
    {
        currentDayIndex = firstDaySceneIndex;
        SceneManager.LoadScene(firstDaySceneIndex);
    }

    //increments current day and loads scene at that index
    //assumes that every day scene is placed chronologically in build index
    public void LoadNextDay()
    {
        currentDayIndex++;
        SceneManager.LoadScene(currentDayIndex);
    }

    //load end scene. Index of this scene must be specified by public field endsceneindex
    public void LoadEndScene()
    {
        SceneManager.LoadScene(endSceneIndex);
    }

    //loads app settings scene. Index of this scene in build settings must be specified by public field appsettingssceneindex
    public void LoadAppSettingsScene()
    {
        SceneManager.LoadScene(appSettingsSceneIndex);
    }

    //loads instructions scene. INdex of this scene in build settings must be specified by public field instructionsScene
    public void LoadInstructionsScene()
    {
        SceneManager.LoadScene(instructionsSceneIndex);
    }

    //main menu should always be 0
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}