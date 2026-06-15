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
// Updated upstream


        if (barraVida == null)
        {
            Debug.LogError("VidaTanque precisa estar no mesmo objeto que o Slider da BarraVida!");
            return;
        }

// Stashed changes
        vidaAtual = vidaMaxima;
        barraVida.maxValue = vidaMaxima;
        barraVida.value    = vidaAtual;
    }

    public void TomarDano(float dano)
    {
        vidaAtual -= dano;
        if (vidaAtual < 0) vidaAtual = 0;
        barraVida.value = vidaAtual;
        Debug.Log("Vida jogador: " + vidaAtual);
    }

    /// <summary>Retorna HP do jogador de 0 (morto) a 1 (cheio). Usado pela IA.</summary>
    public float GetVidaPercentual() => vidaAtual / vidaMaxima;
}