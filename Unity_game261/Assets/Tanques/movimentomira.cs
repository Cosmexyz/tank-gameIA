using UnityEngine;

public class MovimentoMira : MonoBehaviour
{
    public float movimentoMiraVertical = 100f;
    public float anguloMaximo = -45f;
    public float anguloMinimo = 0f;

    private float anguloX = 0f;

    void Start()
    {
        anguloX = 0f;
    }

    void Update()
    {
        float movimentoVertical = Input.GetAxis("Mouse Y");
        anguloX -= movimentoVertical * movimentoMiraVertical * Time.deltaTime;

        anguloX = Mathf.Clamp(anguloX, anguloMaximo, anguloMinimo);

        transform.localRotation = Quaternion.Euler(anguloX, 0f, 0f);
    }
}