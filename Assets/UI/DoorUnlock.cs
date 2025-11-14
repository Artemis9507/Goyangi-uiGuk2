using UnityEngine;

public class DoorUnlock : MonoBehaviour
{
    public static DoorUnlock instance;

    private void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
        Debug.Log("Door locked");
    }

    public void UnlockDoor()
    {
        gameObject.SetActive(true);
        Debug.Log("Door unlocked");
    }
}
