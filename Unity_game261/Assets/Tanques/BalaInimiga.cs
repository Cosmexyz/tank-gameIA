using UnityEngine;

public class BalaInimiga : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Bala inimiga acertou: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Acertou o PLAYER!");

            VidaTanque vidaJogador = FindObjectOfType<VidaTanque>();

            if (vidaJogador != null)
            {
                vidaJogador.TomarDano(10);
            }
            else
            {
                Debug.LogError("VidaTanque não encontrado na cena!");
            }
        }

        Destroy(gameObject);
    }
}