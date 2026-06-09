using UnityEngine;

public class GirarImagem : MonoBehaviour
{
    public float velocidade = 100f;

    void Update()
    {
        transform.Rotate(
            0,
            0,
            velocidade * Time.unscaledDeltaTime
        );
    }
}