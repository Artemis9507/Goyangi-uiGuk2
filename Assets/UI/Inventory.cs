using UnityEngine;
using UnityEngine.UI;


public class Inventory : MonoBehaviour
{
    public Image[] slots;
    public Sprite[] itemSprites;

    private bool[] pickedUpKeys = new bool[3];
    private int collectedCount = 0;

    public void PickUp(int itemIndex)
    {
        if (pickedUpKeys[itemIndex])
            return;
        
        pickedUpKeys[itemIndex] = true;
        
        if (collectedCount < slots.Length)
        {
            slots[collectedCount].sprite = itemSprites[itemIndex];
            slots[collectedCount].enabled = true;
            collectedCount++;
            
        }
        CheckCollected();
    }

    public void CheckCollected()
    {
        foreach (bool collected in pickedUpKeys)
        {
            if (!collected)
                return;
        }
        Debug.Log("Door unlocked");
        DoorUnlock.instance.UnlockDoor();
        
        }
}
