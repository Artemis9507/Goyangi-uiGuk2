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
            Debug.LogWarning("PauseMenu: pausePanel not set in inspector.");
        }
    }


    public void TogglePause()
    {
        if (isPaused) Unpause(); else Pause();
    }


    public void Pause()
    {
// Only pause during playable scenes (optional):
        if (SceneManager.GetActiveScene().buildIndex == 0) return; // don't pause main menu


        Time.timeScale = 0f;
        isPaused = true;
        if (pausePanel != null) pausePanel.SetActive(true);


        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;


// Make sure EventSystem exists and the panel is interactable (check in inspector)
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
// Restore time scale and cursor before loading menu
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;


        SceneManager.LoadScene(mainMenuBuildIndex);
    }
}
