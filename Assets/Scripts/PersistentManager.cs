using UnityEngine;
using UnityEngine.SceneManagement;


public class PersistentManager : MonoBehaviour
{
    public static PersistentManager Instance { get; private set; }


    [Tooltip("Build index of your Main Menu scene (destroy on this scene).")]
    public int mainMenuBuildIndex = 0;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }


        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        if (Instance == this) Instance = null;
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == mainMenuBuildIndex)
        {
            Destroy(gameObject);
        }
    }
}