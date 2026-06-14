using UnityEngine;

/// <summary>
/// Bala do jogador.
/// Ao acertar um inimigo, notifica o EnemyAI para registrar o padrão de ataque.
/// </summary>
public class Bala : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Acertou: " + collision.gameObject.name);

        // Tenta achar VidaInimigo (script de vida do inimigo)
        VidaInimigo vidaInimigo = collision.gameObject.GetComponentInParent<VidaInimigo>();
        if (vidaInimigo != null)
        {
            vidaInimigo.TomarDano(10);
        }

        // ── NOVO: notifica EnemyAI para aprender com o ataque ──
        EnemyAI ia = collision.gameObject.GetComponentInParent<EnemyAI>();
        if (ia != null)
        {
            ia.RegistrarAtaquePlayer();
        }

        Destroy(gameObject);
    }
}