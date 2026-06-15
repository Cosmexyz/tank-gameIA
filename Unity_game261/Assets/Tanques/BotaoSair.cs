using UnityEngine;

public class BotaoSair : MonoBehaviour
{
    public void SairDoJogo()
    {
        Debug.Log("Fechando jogo...");

        Application.Quit();
    }
}