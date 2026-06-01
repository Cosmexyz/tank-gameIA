using UnityEngine;

// Este script é um template de teste e pode ser usado como base para novos comportamentos
public class MoverTeste01 : MonoBehaviour
{
    [SerializeField] private float velocidade = 5f;

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direcao = new Vector3(horizontal, 0f, vertical).normalized;
        transform.position += direcao * velocidade * Time.deltaTime;
    }
}
