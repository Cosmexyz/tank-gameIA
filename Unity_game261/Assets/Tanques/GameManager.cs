using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject menu;
    public GameObject barraVida;
    public GameObject barraVidaInimigo;

    public void IniciarJogo()
    {
        menu.SetActive(false);

        MostrarBarra(barraVida);
        MostrarBarra(barraVidaInimigo);

        Debug.Log("Jogo iniciado!");
    }

    void MostrarBarra(GameObject barra)
    {
        if (barra != null)
        {
            CanvasGroup cg = barra.GetComponent<CanvasGroup>();

            if (cg != null)
            {
                cg.alpha = 1;
                cg.interactable = true;
                cg.blocksRaycasts = true;
            }
        }
    }
}