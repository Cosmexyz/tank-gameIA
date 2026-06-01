using UnityEngine;

// Base class for all tank types. Inheritors should implement movement and attack logic.
public class TankBase : MonoBehaviour
{
    [SerializeField] protected float maxHealth = 100f;
    protected float currentHealth;
    protected bool isAlive = true;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(float damage)
    {
        if (!isAlive) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        isAlive = false;
        Debug.Log(gameObject.name + " was destroyed");
        Destroy(gameObject);
    }

    public float GetHealthPercentage()
    {
        return currentHealth / maxHealth;
    }

    public bool IsAlive()
    {
        return isAlive;
    }
}
