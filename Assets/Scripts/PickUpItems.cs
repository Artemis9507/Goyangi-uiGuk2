using UnityEngine;

public class PickUpItems : MonoBehaviour
{
    public int itemIndex;
    public Inventory inventory;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inventory.PickUp(itemIndex);
            Destroy(gameObject); 
        }
    }
}
