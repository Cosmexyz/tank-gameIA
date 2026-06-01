using UnityEngine;

public class MovimentoTanqueStanley : MonoBehaviour
{
    public float velocidadeAndar = 10f;
    public float velocidadeGirar = 100f;

    void Update()
    {
        float andar = Input.GetAxis("Vertical");
        float girar = Input.GetAxis("Horizontal");

        // Desabilita movimento para frente se A ou D for pressionado
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            andar = 0f;
        }

        transform.position += transform.forward * andar * velocidadeAndar * Time.deltaTime;
        transform.Rotate(0f, girar * velocidadeGirar * Time.deltaTime, 0f);
    }
}