using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRegion : MonoBehaviour
{
    public BoxCollider spawnRegion;
    public float minDistance = 5f;
    public LayerMask entityMask;

    private Vector3 boundsMin;
    private Vector3 boundsMax;

    void Awake() {
        boundsMin = spawnRegion.bounds.min;
        boundsMax = spawnRegion.bounds.max;
    }

    public Vector3 GenerateSafeSpawnPoint() {
        Vector3 spawnPoint = GenerateRandomSpawnPoint();
        while (Physics.OverlapSphere(spawnPoint, minDistance, entityMask).Length > 0) {
            spawnPoint = GenerateRandomSpawnPoint();
        }
        return spawnPoint;
    }

    public Vector3 GenerateRandomSpawnPoint() {
        return new Vector3(Random.Range(boundsMin.x, boundsMax.x), spawnRegion.center.y + 0.3f, Random.Range(boundsMin.z, boundsMax.z));
    }
}
