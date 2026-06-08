using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float detectionRange = 20f;
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private float patrolSpeed = 3.5f;
    [SerializeField] private float chaseSpeed = 5f;

    private NavMeshAgent agent;
    private Transform player;
    private Vector3[] patrolPoints;
    private int currentPatrolIndex = 0;
    private bool isChasing = false;

    IEnumerator Start()
    {
        yield return null;
    
        agent = GetComponent<NavMeshAgent>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj == null)
        {
            Debug.LogError("Player não encontrado.");
            yield break;
        }
        player = playerObj.transform;
        if (agent != null)
        {
            agent.speed = patrolSpeed;
        }
        patrolPoints = new Vector3[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            patrolPoints[i] = transform.GetChild(i).position;
        }
        if (patrolPoints.Length == 0)
        {
            patrolPoints = new Vector3[] { transform.position };
        }
    }
    void Update()
    {
        if (player == null || agent == null) return;
        float distanceToPlayer =
            Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < detectionRange)
        {
            isChasing = true;
            agent.speed = chaseSpeed;
            agent.isStopped = false;
            agent.SetDestination(player.position);
            if (distanceToPlayer < attackRange)
            {
                agent.isStopped = true;
            }
        }
        else
        {
            isChasing = false;
            agent.speed = patrolSpeed;
            agent.isStopped = false;
            Patrol();
        }
    }
    private void Patrol()
    {
        if (patrolPoints.Length == 0) return;
        if (!agent.hasPath || agent.remainingDistance < 0.5f)
        {
            currentPatrolIndex =
                (currentPatrolIndex + 1) % patrolPoints.Length;
            agent.SetDestination(patrolPoints[currentPatrolIndex]);
    }
        }
    public bool IsChasing()
    {
        return isChasing;
    }
}