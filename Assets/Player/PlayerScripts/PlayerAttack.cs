using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float damage = 20f;
    public Transform hitBox;
    public Vector3 boxSize = new Vector3(1, 1, 1);
    
    private Collider[] hitResults = new Collider[10]; 
    
    public void PreformeAttack()
    {
        int hitCount = Physics.OverlapBoxNonAlloc(hitBox.position, boxSize * 0.5f, hitResults, hitBox.rotation);
        for (int i = 0; i < hitCount; i++)
        {
             EnemyHealth enemy = hitResults[i].GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
    

}
