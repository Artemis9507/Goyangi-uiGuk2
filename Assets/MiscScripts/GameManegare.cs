using UnityEngine;

public class GameManegare : MonoBehaviour
{
    public static GameManegare Instance;
    [Header("UI")]
    public GameObject loseMenu;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void PlayerDied()
    {
        loseMenu.SetActive(true);
        
        Time.timeScale = 0f;
    }
}
