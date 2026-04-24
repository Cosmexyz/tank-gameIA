using UnityEngine;

public class  movimentotanqueStanley : MonoBehaviour
{
    public float velocidadeAndar = 5f;
    public float velocidadeGirar = 100f;

   void Update()
    {
        float andar = Input.GetAxis("Vertical");    //movimento para frente e para trás
        float girar = Input.GetAxis("Horizontal");   //movimento para girar

        transform.position += transform.forward * andar * velocidadeAndar * Time.deltaTime;
        transform.Rotate(0f, girar * velocidadeGirar * Time.deltaTime, 0f);
    }
}