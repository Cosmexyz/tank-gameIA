using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// IA avançada dos tanques inimigos.
/// Possui 3 sistemas principais:
///   1. APRENDIZADO — lê e memoriza padrões de ataque do usuário (intervalo, posição, direção)
///   2. ESTRATÉGIA ADAPTATIVA — ajusta comportamento com base nos dados aprendidos
///   3. SISTEMA DE FUGA — ativa quando o inimigo percebe que está em desvantagem
/// </summary>
public class EnemyAI : MonoBehaviour
{
    // ─────────────────────────────────────────────
    // CONFIGURAÇÕES GERAIS
    // ─────────────────────────────────────────────
    [Header("Detecção e Movimento")]
    [SerializeField] private float detectionRange   = 20f;
    [SerializeField] private float attackRange      = 8f;
    [SerializeField] private float fleeRange        = 12f;   // distância mínima durante fuga
    [SerializeField] private float patrolSpeed      = 3.5f;
    [SerializeField] private float chaseSpeed       = 5f;
    [SerializeField] private float fleeSpeed        = 7f;

    [Header("Ataque")]
    [SerializeField] private GameObject balaPrefab;
    [SerializeField] private Transform  pontoDisparo;
    [SerializeField] private float      tempoEntreTiros     = 2f;
    [SerializeField] private float      tempoTiroMinimo     = 0.5f;  // limite mínimo ao adaptar
    [SerializeField] private float      velocidadeBala      = 20f;

    [Header("Vida / Fuga")]
    [SerializeField] private float vidaMaxima            = 100f;
    [SerializeField] private float porcentagemFugaBaixa  = 0.25f;  // foge com < 25% HP
    [SerializeField] private float porcentagemFugaVitoria = 0.60f; // foge se player tem > 60% HP e inimigo < 50%

    // ─────────────────────────────────────────────
    // ESTADO INTERNO
    // ─────────────────────────────────────────────
    private NavMeshAgent agent;
    private Transform    player;
    private VidaTanque   vidaPlayer;
    private float        vidaAtual;

    private Vector3[] patrolPoints;
    private int       currentPatrolIndex = 0;

    private float contadorTiro = 0f;
    private float tempoTiroAtual;          // pode ser reduzido com aprendizado

    // ─── Máquina de estados ───
    private enum Estado { Patrulha, Perseguindo, Atacando, Fugindo }
    private Estado estadoAtual = Estado.Patrulha;

    // ─────────────────────────────────────────────
    // SISTEMA DE APRENDIZADO
    // ─────────────────────────────────────────────
    // Registra cada disparo do player: posição + tempo
    private struct RegistroAtaque
    {
        public Vector3 posicaoPlayer;
        public float   tempo;
    }

    private List<RegistroAtaque> historicoAtaques = new List<RegistroAtaque>();
    private float ultimoTempoAtaquePlayer  = -999f;
    private float intervaloMedioAtaquePlayer = 2f;   // estimativa inicial
    private int   totalAcertosNoPlayer     = 0;

    // Direção predominante dos ataques do player (aprendida)
    private Vector3 direcaoAtaquePredominante = Vector3.zero;

    // Nível de ameaça calculado (0 = calmo, 1 = muito perigoso)
    private float nivelAmeaca = 0f;

    // ─────────────────────────────────────────────
    // INICIALIZAÇÃO
    // ─────────────────────────────────────────────
    IEnumerator Start()
    {
        yield return null;   // aguarda 1 frame para a cena estar pronta

        agent = GetComponent<NavMeshAgent>();
        vidaAtual = vidaMaxima;
        tempoTiroAtual = tempoEntreTiros;

        // Acha o player
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj == null) { Debug.LogError("[EnemyAI] Player não encontrado!"); yield break; }
        player     = playerObj.transform;
        vidaPlayer = playerObj.GetComponentInParent<VidaTanque>();

        if (agent != null) agent.speed = patrolSpeed;

        // Pontos de patrulha são os filhos do objeto
        patrolPoints = new Vector3[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
            patrolPoints[i] = transform.GetChild(i).position;

        if (patrolPoints.Length == 0)
            patrolPoints = new Vector3[] { transform.position };
    }

    // ─────────────────────────────────────────────
    // UPDATE — Máquina de estados principal
    // ─────────────────────────────────────────────
    void Update()
    {
        if (player == null || agent == null) return;

        float distancia = Vector3.Distance(transform.position, player.position);

        // Atualiza aprendizado a cada frame (barato)
        AtualizarAprendizado();

        // Decide o estado
        EstadoAtual(distancia);

        // Executa o estado
        switch (estadoAtual)
        {
            case Estado.Patrulha:    Patrulhar();         break;
            case Estado.Perseguindo: Perseguir();         break;
            case Estado.Atacando:    Atacar(distancia);  break;
            case Estado.Fugindo:     Fugir();             break;
        }

        // Contador de tiro (independente do estado de fuga)
        contadorTiro += Time.deltaTime;
    }

    // ─────────────────────────────────────────────
    // MÁQUINA DE ESTADOS — decisão
    // ─────────────────────────────────────────────
    private void EstadoAtual(float distancia)
    {
        if (DeveFugir())
        {
            estadoAtual = Estado.Fugindo;
            return;
        }

        if (distancia < detectionRange)
        {
            estadoAtual = distancia < attackRange ? Estado.Atacando : Estado.Perseguindo;
        }
        else
        {
            estadoAtual = Estado.Patrulha;
        }
    }

    // ─────────────────────────────────────────────
    // SISTEMA DE FUGA
    // ─────────────────────────────────────────────
    private bool DeveFugir()
    {
        float pctVida = vidaAtual / vidaMaxima;

        // Fuga de sobrevivência: HP muito baixo
        if (pctVida <= porcentagemFugaBaixa)
            return true;

        // Fuga tática: player claramente está dominando
        if (vidaPlayer != null)
        {
            float pctPlayer = vidaPlayer.GetVidaPercentual();
            if (pctPlayer > porcentagemFugaVitoria && pctVida < 0.5f)
                return true;
        }

        // Fuga por ameaça aprendida: player é muito agressivo e está perto
        float distancia = Vector3.Distance(transform.position, player.position);
        if (nivelAmeaca > 0.7f && distancia < attackRange * 1.2f && pctVida < 0.5f)
            return true;

        return false;
    }

    private void Fugir()
    {
        agent.isStopped = false;
        agent.speed = fleeSpeed;

        // Calcula ponto de fuga: direção oposta ao player + perpendicular (para não ser previsível)
        Vector3 direcaoFuga = (transform.position - player.position).normalized;
        // Adiciona componente lateral baseada no padrão de ataque aprendido
        Vector3 perpendicular = Vector3.Cross(direcaoFuga, Vector3.up).normalized;
        float lateralBias = Mathf.Sin(Time.time * 0.8f);  // oscila para enganar
        Vector3 pontoFuga = transform.position + (direcaoFuga + perpendicular * lateralBias) * fleeRange;

        // Garante que o ponto está no NavMesh
        if (NavMesh.SamplePosition(pontoFuga, out NavMeshHit hit, 5f, NavMesh.AllAreas))
            agent.SetDestination(hit.position);

        // Tenta atirar enquanto foge (tiro de cobertura)
        if (contadorTiro >= tempoTiroAtual * 1.5f)
        {
            AtirarNaPlayer();
            contadorTiro = 0f;
        }
    }

    // ─────────────────────────────────────────────
    // COMPORTAMENTOS
    // ─────────────────────────────────────────────
    private void Patrulhar()
    {
        agent.speed     = patrolSpeed;
        agent.isStopped = false;

        if (!agent.hasPath || agent.remainingDistance < 0.5f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            agent.SetDestination(patrolPoints[currentPatrolIndex]);
        }
    }

    private void Perseguir()
    {
        agent.speed     = chaseSpeed;
        agent.isStopped = false;

        // Se o nível de ameaça é alto, tenta prever onde o player vai estar
        if (nivelAmeaca > 0.5f)
        {
            Vector3 destino = PreverPosicaoPlayer();
            agent.SetDestination(destino);
        }
        else
        {
            agent.SetDestination(player.position);
        }
    }

    private void Atacar(float distancia)
    {
        agent.isStopped = true;

        // Mira no player
        Vector3 direcao = (player.position - transform.position).normalized;
        direcao.y = 0f;
        if (direcao != Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direcao), 8f * Time.deltaTime);

        // Atira no tempo adaptado pelo aprendizado
        if (contadorTiro >= tempoTiroAtual)
        {
            AtirarNaPlayer();
            contadorTiro = 0f;
        }
    }

    private void AtirarNaPlayer()
    {
        if (balaPrefab == null || pontoDisparo == null) return;

        // Calcula direção com previsão se ameaça alta
        Vector3 alvo = nivelAmeaca > 0.5f ? PreverPosicaoPlayer() : player.position;
        Vector3 dir  = (alvo - pontoDisparo.position).normalized;
        dir.y = 0f;

        Quaternion rotacaoBala = Quaternion.LookRotation(dir);
        GameObject bala = Instantiate(balaPrefab, pontoDisparo.position, rotacaoBala);
        Rigidbody rb = bala.GetComponent<Rigidbody>();
        if (rb != null) rb.velocity = dir * velocidadeBala;
    }

    // ─────────────────────────────────────────────
    // SISTEMA DE APRENDIZADO
    // ─────────────────────────────────────────────

    /// <summary>
    /// Chamado pela Bala do player ao acertar este inimigo.
    /// Registra o padrão do ataque.
    /// </summary>
    public void RegistrarAtaquePlayer()
    {
        float agora = Time.time;

        RegistroAtaque registro = new RegistroAtaque
        {
            posicaoPlayer = player != null ? player.position : Vector3.zero,
            tempo         = agora
        };
        historicoAtaques.Add(registro);

        // Limita histórico a 30 registros (janela deslizante)
        if (historicoAtaques.Count > 30)
            historicoAtaques.RemoveAt(0);

        // Calcula intervalo médio entre ataques
        if (ultimoTempoAtaquePlayer > 0f)
        {
            float intervalo = agora - ultimoTempoAtaquePlayer;
            intervaloMedioAtaquePlayer = Mathf.Lerp(intervaloMedioAtaquePlayer, intervalo, 0.3f);
        }
        ultimoTempoAtaquePlayer = agora;
        totalAcertosNoPlayer++;

        AtualizarEstrategia();
    }

    /// <summary>
    /// Atualiza cálculos de aprendizado e nível de ameaça.
    /// </summary>
    private void AtualizarAprendizado()
    {
        if (historicoAtaques.Count < 2) return;

        // Calcula direção predominante dos ataques (posição do player ao atacar)
        Vector3 somaDirecoes = Vector3.zero;
        for (int i = 1; i < historicoAtaques.Count; i++)
        {
            Vector3 dir = historicoAtaques[i].posicaoPlayer - historicoAtaques[i - 1].posicaoPlayer;
            somaDirecoes += dir.normalized;
        }
        direcaoAtaquePredominante = somaDirecoes.normalized;

        // Nível de ameaça: combinação de frequência de ataques e acertos
        float freqScore   = Mathf.Clamp01(1f - intervaloMedioAtaquePlayer / 5f);  // mais rápido = mais perigoso
        float acertoScore = Mathf.Clamp01(totalAcertosNoPlayer / 10f);             // mais acertos = mais ameaçador
        nivelAmeaca = Mathf.Lerp(nivelAmeaca, (freqScore + acertoScore) * 0.5f, 0.1f * Time.deltaTime);
    }

    /// <summary>
    /// Adapta parâmetros de ataque com base no que foi aprendido.
    /// Quanto mais perigoso o player, mais rápido e preciso o inimigo reage.
    /// </summary>
    private void AtualizarEstrategia()
    {
        // Reduz intervalo de tiro conforme aprende (mas não abaixo do mínimo)
        float reducao = Mathf.Lerp(0f, tempoEntreTiros - tempoTiroMinimo, nivelAmeaca);
        tempoTiroAtual = Mathf.Max(tempoTiroMinimo, tempoEntreTiros - reducao);

        // Aumenta velocidade de perseguição se player é muito agressivo
        chaseSpeed = Mathf.Lerp(5f, 7f, nivelAmeaca);

        Debug.Log($"[EnemyAI] Aprendizado atualizado — Ameaça: {nivelAmeaca:F2} | Intervalo tiro: {tempoTiroAtual:F2}s | Velocidade: {chaseSpeed:F1}");
    }

    /// <summary>
    /// Tenta prever onde o player estará em breve com base no histórico de movimento.
    /// </summary>
    private Vector3 PreverPosicaoPlayer()
    {
        if (historicoAtaques.Count < 2)
            return player.position;

        // Velocidade estimada do player com base nas últimas posições registradas
        Vector3 posAtual   = player.position;
        Vector3 posAnterior = historicoAtaques[historicoAtaques.Count - 1].posicaoPlayer;
        Vector3 velocidade  = (posAtual - posAnterior) / Mathf.Max(0.1f, Time.time - historicoAtaques[historicoAtaques.Count - 1].tempo);

        // Prevê posição daqui a ~0.5s
        return posAtual + velocidade * 0.5f;
    }

    // ─────────────────────────────────────────────
    // DANO / VIDA
    // ─────────────────────────────────────────────
    public void TomarDano(float dano)
    {
        vidaAtual -= dano;
        vidaAtual  = Mathf.Clamp(vidaAtual, 0f, vidaMaxima);

        // Ao receber dano, registra que o player acertou
        RegistrarAtaquePlayer();

        if (vidaAtual <= 0f)
            Morrer();
    }

    private void Morrer()
    {
        Debug.Log("[EnemyAI] Inimigo destruído.");
        Destroy(gameObject);
    }

    // ─────────────────────────────────────────────
    // GETTERS ÚTEIS
    // ─────────────────────────────────────────────
    public bool EstaFugindo()     => estadoAtual == Estado.Fugindo;
    public bool EstaPerseguindo() => estadoAtual == Estado.Perseguindo;
    public float GetNivelAmeaca() => nivelAmeaca;
    public float GetVidaPercentual() => vidaAtual / vidaMaxima;
}