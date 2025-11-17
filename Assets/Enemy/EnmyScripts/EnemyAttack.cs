using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float damage = 10;
    public Transform hitBox;
    public Vector3 boxSize = new Vector3(1, 1, 1);
    
    private Collider[] hitResults = new Collider[10]; 
    
    public void PreformAttack()
    {
        int hitCount = Physics.OverlapBoxNonAlloc(hitBox.position, boxSize * 0.5f, hitResults, hitBox.rotation);
        for (int i = 0; i < hitCount; i++)
        {
            PlayerHealth player = hitResults[i].GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }
    }
}
