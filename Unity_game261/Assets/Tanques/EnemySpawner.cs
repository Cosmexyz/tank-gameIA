using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Inimigo1;
    public Transform spawnPoint;

    void Start()
    {
        Instantiate(Inimigo1, spawnPoint.position, spawnPoint.rotation);
    }
}