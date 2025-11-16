using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float damage = 10;
    public Transform hitBox;
    public Vector3 boxSize = new Vector3(1, 1, 1);
    
    
    public void PreformAttack()
    {
        Collider[] hits = Physics.OverlapBox(hitBox.position, boxSize * 0.5f, hitBox.rotation);
        foreach (Collider hit in hits)
        {
            PlayerHealth player = hit.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }
    }
}
