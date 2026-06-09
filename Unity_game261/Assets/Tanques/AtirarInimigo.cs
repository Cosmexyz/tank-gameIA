using UnityEngine;

public class AtirarInimigo : MonoBehaviour
{
    public GameObject balaPrefab;
    public Transform pontoDisparo;

    public float velocidadeBala = 20f;
    public float tempoEntreTiros = 2f;

    private float contador;

    void Update()
    {
        contador += Time.deltaTime;

        if (contador >= tempoEntreTiros)
        {
            Atirar();
            contador = 0f;
        }
    }

    void Atirar()
    {
        GameObject bala = Instantiate(
            balaPrefab,
            pontoDisparo.position,
            pontoDisparo.rotation
        );

        Rigidbody rb = bala.GetComponent<Rigidbody>();

        rb.velocity = pontoDisparo.forward * velocidadeBala;
    }
}