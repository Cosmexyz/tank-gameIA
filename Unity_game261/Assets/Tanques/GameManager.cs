using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private TankVida playerHealth;

    private bool gameStarted = false;

    void Update()
    {
        if (gameStarted && Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void ComecarJogo()
    {
        gameStarted = true;

        if (menu != null)
            menu.SetActive(false);

        if (healthBar != null)
            healthBar.SetActive(true);

        if (playerHealth != null)
            playerHealth.MostrarBarra();

        Time.timeScale = 1f;
        Debug.Log("Game started");
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        Debug.Log("Game paused");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        Debug.Log("Game resumed");
    }

    public void GameOver()
    {
        gameStarted = false;
        Time.timeScale = 0f;
        Debug.Log("Game Over");
    }
}