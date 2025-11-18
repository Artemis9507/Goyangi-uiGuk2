using UnityEngine;

public class DoorUnlock : MonoBehaviour
{
    public static DoorUnlock instance;
    public AudioClip unlockSound;

    private void Awake()
    {
        instance = this;
        Debug.Log("Door locked");
    }
    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void UnlockDoor()
    {
        gameObject.SetActive(true);
        Debug.Log("Door unlocked"); 
        SoundManager.instance.PlayEffect(unlockSound);
    }
}
