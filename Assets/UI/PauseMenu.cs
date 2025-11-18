using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    public static bool isPaused { get; private set; }


    private void Awake()
    {
        if (pausePanel == null)
        {
            // Debug.LogWarning("PauseMenu: pausePanel not set in inspector.");
        }
    }


    public void TogglePause()
    {
        if (isPaused) Unpause(); else Pause();
    }


    public void Pause()
    {

        if (SceneManager.GetActiveScene().buildIndex == 0) return;


        Time.timeScale = 0f;
        isPaused = true;
        if (pausePanel != null) pausePanel.SetActive(true);


        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
    }


    public void Unpause()
    {
        Time.timeScale = 1f;
        isPaused = false;
        if (pausePanel != null) pausePanel.SetActive(false);


        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    public void ToMainMenu(int mainMenuBuildIndex = 0)
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;


        SceneManager.LoadScene(mainMenuBuildIndex);
    }
}
