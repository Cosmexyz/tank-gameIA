using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject menu;
    public GameObject barraVida;

    public void ComecarJogo()
    {
        menu.SetActive(false);

        barraVida.SetActive(true);

        Debug.Log("Jogo começou");
    }
}