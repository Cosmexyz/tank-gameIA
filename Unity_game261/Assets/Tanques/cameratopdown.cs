using UnityEngine;

public class CameraTopDown : MonoBehaviour
{
    public Terrain terrain;
    public Vector3 offset = new Vector3(0f, 10f, -15f);

    public Transform player;
    private float minX, maxX, minZ, maxZ;

    void Start()
    {
        Vector3 terrainPos = terrain.transform.position;
        Vector3 terrainSize = terrain.terrainData.size;

        minX = terrainPos.x;
        maxX = terrainPos.x + terrainSize.x;
        minZ = terrainPos.z;
        maxZ = terrainPos.z + terrainSize.z;
    }

    void LateUpdate()
    {
        Vector3 desiredPos = player.position + offset;

        desiredPos.x = Mathf.Clamp(desiredPos.x, minX, maxX);
        desiredPos.z = Mathf.Clamp(desiredPos.z, minZ, maxZ);

        transform.position = desiredPos;
    }
}