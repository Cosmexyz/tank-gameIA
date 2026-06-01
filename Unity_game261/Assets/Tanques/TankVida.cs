using UnityEngine;
using UnityEngine.UI;

public class TankVida : MonoBehaviour
{
    public float maxHealth = 100f;

    private float currentHealth;

    private GameObject barraVidaObj;
    private Slider barraDeVida;

    void Awake()
    {
        // Procura TODOS os objetos da cena
        GameObject[] objs = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (GameObject obj in objs)
        {
            if (obj.name == "Barravida")
            {
                barraVidaObj = obj;
            }

            if (obj.name == "vida")
            {
                barraDeVida = obj.GetComponent<Slider>();
            }
        }

        // Inicializa vida
        currentHealth = maxHealth;

        // ESCONDE A BARRA
        if (barraVidaObj != null)
        {
            barraVidaObj.SetActive(false);
        }
    }

    void Start()
    {
        if (barraDeVida != null)
        {
            barraDeVida.maxValue = maxHealth;
            barraDeVida.value = currentHealth;
        }
    }

    // CHAMAR NO BOTÃO COMEÇAR
    public void MostrarBarra()
    {
        if (barraVidaObj != null)
        {
            // ATIVA DEFINITIVAMENTE
            barraVidaObj.SetActive(true);

            // ATIVA TODOS OS FILHOS
            foreach (Transform t in barraVidaObj.GetComponentsInChildren<Transform>(true))
            {
                t.gameObject.SetActive(true);
            }

            Debug.Log("BARRA ATIVADA");
        }
    }

    public void TomarDano(float dano)
    {
        currentHealth -= dano;

        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        if (barraDeVida != null)
        {
            barraDeVida.value = currentHealth;
        }
    }

    void Update()
    {
        // TESTE
        if (Input.GetKeyDown(KeyCode.H))
        {
            TomarDano(10f);
        }
    }

    public int vida = 100;
    public GameOverController gameOverController;

    public void TomarDano(int dano)
    {
        vida -= dano;

        if (vida <= 0)
        {
            vida = 0;
            gameOverController.MostrarGameOver();
        }
    }
}