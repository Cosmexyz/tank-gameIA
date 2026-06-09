using UnityEngine;
using UnityEngine.UI;

public class VidaInimigo : MonoBehaviour
{
    public Slider barraVida;
    public GameObject telaVitoria;

    public float vidaMaxima = 100f;
    private float vidaAtual;

    void Start()
    {
        vidaAtual = vidaMaxima;

        barraVida.maxValue = vidaMaxima;
        barraVida.value = vidaAtual;
    }

    public void TomarDano(float dano)
    {
        vidaAtual -= dano;

        if (vidaAtual < 0)
            vidaAtual = 0;

        barraVida.value = vidaAtual;

        Debug.Log("Vida inimigo: " + vidaAtual);

        if (vidaAtual <= 0)
        {
            VencerJogo();
        }
    }

    void VencerJogo()
    {
        if (telaVitoria != null)
        {
            telaVitoria.SetActive(true);
        }

        Time.timeScale = 0f;

        Debug.Log("Você venceu!");
    }
}