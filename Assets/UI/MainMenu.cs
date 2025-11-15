using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject settingsPanel;


    [Header("Buttons")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Animator[] buttonAnimators;
    
    public int firstLevelBuildIndex = 1;

    public void Awake()
    {
        if (menuPanel) menuPanel.SetActive(true);
        if (settingsPanel) settingsPanel.SetActive(false);
    }

   /* public void HideButtons()
    {
        quitButton.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
    }*/
    
    public void PlayGame()
    {
        
        SceneManager.LoadScene(firstLevelBuildIndex);
        
    }

    public void Settings()
    {
        if (menuPanel) menuPanel.SetActive(false);
        if (settingsPanel) settingsPanel.SetActive(true);
        
        ResetButtons();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        if (settingsPanel) settingsPanel.SetActive(false);
        if (menuPanel) menuPanel.SetActive(true);
        
        ResetButtons();
        
    }

    private void ResetButtons()
    {
        foreach (Animator anim in buttonAnimators)
        {
            if (anim!=null && anim.gameObject.activeInHierarchy) 
            {
                anim.Play("Normal");
            }
        }
    }
}
