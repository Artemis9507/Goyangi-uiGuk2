using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    InputManager inputManager;
    
    public void Restart()
    {
        Debug.Log("Restart");
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
