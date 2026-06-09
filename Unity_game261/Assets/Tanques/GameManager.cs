using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject menu;
    public GameObject barraVida;
    public GameObject barraVidaInimigo;

    public void IniciarJogo()
    {
        menu.SetActive(false);

        if (barraVida != null)
        {
            barraVida.SetActive(true);
        }

        if (barraVidaInimigo != null)
        {
            barraVidaInimigo.SetActive(true);
        }

        Debug.Log("Jogo iniciado!");
    }
}