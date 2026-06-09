using UnityEngine;
using UnityEngine.UI;

public class VidaTanque : MonoBehaviour
{
    public Slider barraVida;

    public float vidaMaxima = 100f;
    private float vidaAtual;

    void Start()
    {
        barraVida = GetComponent<Slider>();

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

        Debug.Log("Vida jogador: " + vidaAtual);
    }
}