using UnityEngine;

public class movimentotorre : MonoBehaviour
{
    public float movimentoMouse = 100f;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");    //movimento para esquerda e direita com o mouse
        transform.Rotate(0f, mouseX * movimentoMouse * Time.deltaTime, 0f);
    }
}
