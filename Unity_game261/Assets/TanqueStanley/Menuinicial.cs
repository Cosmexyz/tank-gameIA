using UnityEngine;
using UnityEngine.SceneManagement;

public class Menuinicial : MonoBehaviour
{
    public GameObject MenuTela;

    public void StartGame()
    {
        MenuTela.SetActive(false);
    }
}