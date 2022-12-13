using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public BoxCollider spawnRegion;
    public GameObject enemyPrefab;
    public Transform playerTransform;
    public float minDistance = 3f;
    public List<GameObject> enemies = new List<GameObject>();

    private Vector3 boundsMin;
    private Vector3 boundsMax;

    void Awake() {
        boundsMin = spawnRegion.bounds.min;
        boundsMax = spawnRegion.bounds.max;
    }

    public void SpawnEnemy() {
        Quaternion spawnRotation = new();
        Vector3 spawnPoint = new Vector3(Random.Range(boundsMin.x, boundsMax.x), 1, Random.Range(boundsMin.z, boundsMax.z));
        Debug.Log(spawnPoint);
        while (Mathf.Sqrt(Mathf.Pow(spawnPoint.x - playerTransform.position.x, 2) + Mathf.Pow(spawnPoint.z - playerTransform.position.z, 2)) < minDistance) {
            spawnPoint = new Vector3(Random.Range(boundsMin.x, boundsMax.x), spawnRegion.center.y, Random.Range(boundsMin.z, boundsMax.z));
        }
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint, spawnRotation);
        newEnemy.GetComponent<EnemyMovement>().playerTransform = playerTransform;
        enemies.Add(newEnemy);
    }
    public void EnableEnemyMovement(bool enable) {
        foreach (var enemy in enemies) {
            enemy.GetComponent<EnemyMovement>().enabled = enable;
        }
    }
}
