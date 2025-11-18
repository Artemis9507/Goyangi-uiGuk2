using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Stats")]
    public float maxHealth = 50f;
    private float currentHealth;
    
    public bool IsDead { get; private set; }
    
    private Animator anim;
    private Enemy enemy;

    private void Awake()
    {
        currentHealth = maxHealth;

        anim = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();
    }

    public void TakeDamage(float damage)
    {
        
        if (IsDead) return;
        
        currentHealth -= damage;
        
        if (currentHealth <= 0f)
        {
                Die();
        }
        
    }

    private void Die()
    {
        IsDead = true;

        enemy.StopAI();

        var agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (agent != null)
        {
            agent.isStopped = true;
            agent.ResetPath();
        }

        anim.SetTrigger("Die");

        Destroy(gameObject, 5f);
    }
}
