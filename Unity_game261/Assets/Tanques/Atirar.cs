using UnityEngine;

public class Atirar : MonoBehaviour
{
    public GameObject balaPrefab;
    public Transform pontoDisparo;

    public float velocidadeBala = 20f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AtirarBala();
        }
    }

    void AtirarBala()
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