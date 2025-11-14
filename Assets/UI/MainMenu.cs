using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    Button button1;
    [SerializeField]
    Button button2;
    [SerializeField]
    Button button3;

    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject settingsPanel;

    public void HideButtons()
    {
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
    }
    public void PlayGame()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }

    public void Settings()
    {
        menuPanel.SetActive(false);
        settingsPanel.SetActive(true);
        button3.animator.SetTrigger("Normal");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        button3.animator.SetTrigger("Normal");
        menuPanel.SetActive(true);
        settingsPanel.SetActive(false);
        
    }
}
