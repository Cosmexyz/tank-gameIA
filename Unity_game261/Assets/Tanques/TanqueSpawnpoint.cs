using UnityEngine;

public class TanqueSpawnpoint : MonoBehaviour
{
    [SerializeField] private GameObject TanqueUsuario1;
    [SerializeField] private Transform spawnPoint;

    private void Start()
    {
        Instantiate(TanqueUsuario1, spawnPoint.position, spawnPoint.rotation);
    }
}