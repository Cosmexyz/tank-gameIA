using UnityEngine;
using UnityEngine.UI;

public class TankVida : MonoBehaviour
{
    public float maxHealth = 100f;
    public Slider healthSlider;

    private float currentHealth;

    void Awake()
    {
        currentHealth = maxHealth;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
            healthSlider.gameObject.SetActive(false);
        }
    }

    public void MostrarBarra()
    {
        if (healthSlider != null)
        {
            healthSlider.gameObject.SetActive(true);
            Debug.Log("Health bar activated");
        }
    }

    public void TomarDano(float dano)
    {
        currentHealth -= dano;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Morrer();
        }
    }

    public float GetHealthPercentage()
    {
        return currentHealth / maxHealth;
    }

    private void Morrer()
    {
        Debug.Log(gameObject.name + " died");
        gameObject.SetActive(false);
    }
}