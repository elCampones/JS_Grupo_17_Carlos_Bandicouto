using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlatforms : MonoBehaviour
{
    public GameObject[] platform;

    public int gridZ;
    public float gridSpacingOffset = 1f;
    public Vector3 gridOrigin = Vector3.zero;
    public Vector3 positionRandomization;

    void Start()
    {
        SpawnGrid();
    }

    void SpawnGrid() {
        for(int z = 0; z < gridZ; z++) {
            Vector3 spawnPosition = new Vector3(0, 0, z * gridSpacingOffset) + gridOrigin;
            PickAndSpawn(RandomizePosition(spawnPosition), Quaternion.identity);
        }
    }

    Vector3 RandomizePosition(Vector3 position)
    {
        Vector3 randomizedPosition = new Vector3(0, Random.Range(-positionRandomization.y, positionRandomization.y), Random.Range(-positionRandomization.z, positionRandomization.z)) + position;
        return randomizedPosition;
    }

    void PickAndSpawn(Vector3 positionaToSpawn, Quaternion rotationToSpawn)
    {
        int randomIndex = Random.Range(0, platform.Length);
        GameObject clone = Instantiate(platform[randomIndex], positionaToSpawn,rotationToSpawn);
    }
}
