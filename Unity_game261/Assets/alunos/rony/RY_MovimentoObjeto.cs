using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimentos : MonoBehaviour
{
    public float velocidade = 10f;
    public float velocidadeRotacao = 100f;
    public Transform torre;
    public Transform cano;
    private Rigidbody rb;
    private float moverInput;
    private float girarInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Codigo do Tanque para dar movimento a ele
    void Update()
    {
        // Lê os inputs do teclado (W/S = Vertical, A/D = Horizontal)
        moverInput = Input.GetAxis("Vertical");
        girarInput = Input.GetAxis("Horizontal");

        MirarComMouse();
    }

    void FixedUpdate()
    {
        // Aplica a física
        Mover();
        Girar();
    }

    void Mover()
    {
        Vector3 movimento = transform.forward * moverInput * velocidade * Time.deltaTime;
        rb.MovePosition(rb.position + movimento);
    }

    void Girar()
    {
        float rotacao = girarInput * velocidadeRotacao * Time.deltaTime;
        Quaternion novaRotacao = Quaternion.Euler(0f, rotacao, 0f);
        rb.MoveRotation(rb.rotation * novaRotacao);
    }
    
    // Codigo da Torre para mirar com o mouse

    void MirarComMouse()
    {
        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(raio, out hit))
        {
            Vector3 alvo = hit.point;

            // =========================
            // 🔄 TORRE (só horizontal)
            // =========================
            Vector3 direcaoHorizontal = alvo - torre.position;
            direcaoHorizontal.y = 0f;

            if (direcaoHorizontal != Vector3.zero)
            {
                Quaternion rotacaoTorre = Quaternion.LookRotation(direcaoHorizontal);
                torre.rotation = rotacaoTorre;
            }

            // =========================
            // 🔼 CANO (só vertical)
            // =========================
            Vector3 direcaoCano = alvo - cano.position;

            float distanciaHorizontal = new Vector3(direcaoCano.x, 0f, direcaoCano.z).magnitude;
            float altura = direcaoCano.y;

            float angulo = Mathf.Atan2(altura, distanciaHorizontal) * Mathf.Rad2Deg;

            cano.localRotation = Quaternion.Euler(-angulo, 0f, 0f);
        }
    }

    /*
    void Update()
    {
    moverInput = Input.GetAxis("Vertical");
    girarInput = Input.GetAxis("Horizontal");

    MirarComMouse();
    }
    */

    // Torre vai mover na Horizontal e Cano (CTRL_RotY) vai mexer na vertical

    /* 
    void MirarComMouse()
    {
        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Define um plano (ex: chão)
        if (Physics.Raycast(raio, out hit))
        {
            Vector3 direcao = hit.point - torre.position;
            direcao.y = 0f; // evita inclinar pra cima/baixo

            if (direcao != Vector3.zero)
            {
                Quaternion rotacao = Quaternion.LookRotation(direcao);
                torre.rotation = rotacao;
            }
        }
    }
    */

}
