using UnityEngine;

public class MovimentoTorre : MonoBehaviour
{
    public float velocidadeRotacao = 10f;
    public float limiteRotacao = 90f; // 90 graus para cada lado = 180 no total

    private float rotacaoInicialLocalY;

    void Start()
    {
        rotacaoInicialLocalY = transform.localEulerAngles.y;
    }

    void Update()
    {
        if (Camera.main == null) return;

        Plane plano = new Plane(Vector3.up, transform.position);
        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (plano.Raycast(raio, out float distancia))
        {
            Vector3 pontoMouse = raio.GetPoint(distancia);
            Vector3 direcaoMundo = pontoMouse - transform.position;
            direcaoMundo.y = 0f;

            if (direcaoMundo.sqrMagnitude < 0.001f)
                return;

            Vector3 direcaoLocal = transform.parent != null
                ? transform.parent.InverseTransformDirection(direcaoMundo)
                : direcaoMundo;

            float anguloLocal = Mathf.Atan2(direcaoLocal.x, direcaoLocal.z) * Mathf.Rad2Deg;
            float anguloLimitado = Mathf.Clamp(anguloLocal, -limiteRotacao, limiteRotacao);

            Quaternion rotacaoAlvoLocal = Quaternion.Euler(0f, rotacaoInicialLocalY + anguloLimitado, 0f);

            transform.localRotation = Quaternion.Lerp(
                transform.localRotation,
                rotacaoAlvoLocal,
                velocidadeRotacao * Time.deltaTime
            );
        }
    }
}