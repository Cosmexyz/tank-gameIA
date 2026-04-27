using UnityEngine;

public class movimentotanqueStanley : MonoBehaviour
{
    public float velocidadeAndar = 5f;
    public float velocidadeGirar = 100f;

    void Update()
    {
        float andar = Input.GetAxis("Vertical");
        float girar = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            andar = 0f;
        }

        transform.position += transform.forward * andar * velocidadeAndar * Time.deltaTime;
        transform.Rotate(0f, girar * velocidadeGirar * Time.deltaTime, 0f);
    }
}