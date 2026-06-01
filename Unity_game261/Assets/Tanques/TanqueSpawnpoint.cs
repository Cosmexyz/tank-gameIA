using UnityEngine;

public class TanqueSpawnpoint : MonoBehaviour
{
    [SerializeField] private GameObject TanqueUsuario1;
    [SerializeField] private Transform spawnPoint;

    private void Start()
    {
        if (TanqueUsuario1 == null)
        {
            Debug.LogError("TanqueSpawnpoint: TanqueUsuario1 prefab not assigned in Inspector!");
            return;
        }

        if (spawnPoint == null)
        {
            Debug.LogError("TanqueSpawnpoint: spawnPoint not assigned in Inspector!");
            return;
        }

        Instantiate(TanqueUsuario1, spawnPoint.position, spawnPoint.rotation);
    }
}