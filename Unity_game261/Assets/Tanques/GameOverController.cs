using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public GameObject gameOverPanel;

    void Start()
    {
        gameOverPanel.SetActive(false);
    }

    public void MostrarGameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f; // pausar o jogo
    }

    public void TentarNovamente()
    {
        Time.timeScale = 1f; // voltar o jogo
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // recarregar a cena
    }
}