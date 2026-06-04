using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject menu;
    public GameObject barraVida;

    public void IniciarJogo()
    {
        menu.SetActive(false);

        if (barraVida != null)
        {
            barraVida.SetActive(true);
        }

        Debug.Log("Jogo iniciado!");
    }
}