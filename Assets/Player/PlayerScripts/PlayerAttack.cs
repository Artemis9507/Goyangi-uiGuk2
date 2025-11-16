using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float damage = 20f;
    public Transform hitBox;
    public Vector3 boxSize = new Vector3(1, 1, 1);
    
    
    public void PreformeAttack()
    {
        Collider[] hits = Physics.OverlapBox(hitBox.position, boxSize * 0.5f, hitBox.rotation);
        foreach (Collider hit in hits)
        {
             EnemyHealth enemy = hit.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
    

}
