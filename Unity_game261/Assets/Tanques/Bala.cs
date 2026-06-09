using UnityEngine;

public class Bala : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Acertou: " + collision.gameObject.name);

    VidaInimigo vidaInimigo =
        collision.gameObject.GetComponent<VidaInimigo>();

        if (vidaInimigo != null)
        {
            Debug.Log("VidaInimigo encontrada!");
            vidaInimigo.TomarDano(10);
        }
        else
        {
            Debug.Log("VidaInimigo NÃO encontrada!");
        }

        Destroy(gameObject);
    }

}
