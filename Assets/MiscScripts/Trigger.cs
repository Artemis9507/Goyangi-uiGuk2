using UnityEngine;

public class Trigger : MonoBehaviour
{
    public Boss boss;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            boss.ActivateBoss();
            Destroy(gameObject);
        }
    }
}
