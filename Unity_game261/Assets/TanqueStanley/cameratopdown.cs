using UnityEngine;

public class cameratopdown : MonoBehaviour
{
    public Terrain terrain; // referencia do terreno (pegar dimenses)
    public Vector3 offset = new Vector3(0f, 10f, -15f); // posiçao da camera no player

    public Transform player; // referencia player
    private float minX, maxX, minZ, maxZ; // limite terreno

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