using UnityEngine;

public class PickUpItems : MonoBehaviour
{
    public int itemIndex;
    public Inventory inventory;
    public ParticleSystem pickupEffect;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inventory.PickUp(itemIndex);
            if (pickupEffect != null)
            {
                Instantiate(pickupEffect, transform.position, Quaternion.identity);
            }
            Destroy(gameObject); 
        }
    }
}
