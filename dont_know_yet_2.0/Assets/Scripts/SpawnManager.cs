using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public BoxCollider spawnRegion;
    public GameObject chaserPrefab;
    public GameObject tankPrefab;
    public GameObject coinPrefab;
    public Transform playerTransform;
    public LayerMask entityMask;
    public float minDistance = 3f;
    public List<GameObject> enemies = new List<GameObject>();

    private Vector3 boundsMin;
    private Vector3 boundsMax;

    void Awake() {
        boundsMin = spawnRegion.bounds.min;
        boundsMax = spawnRegion.bounds.max;
    }

    public void SpawnChaser() {
        SpawnEnemy(chaserPrefab);
    }

    public void SpawnTank() {
        SpawnEnemy(tankPrefab);
    }
    public void SpawnEnemy(GameObject enemyToSpawn) {
        GameObject newEnemy = SpawnObject(enemyToSpawn);
        newEnemy.GetComponent<Enemy>().playerTransform = playerTransform;
        enemies.Add(newEnemy);
    }

    public void SpawnCoin() {
        SpawnObject(coinPrefab);
    }

    private GameObject SpawnObject(GameObject objectToSpawn) {
        Quaternion spawnRotation = new();
        Vector3 spawnPoint = GenerateSafeSpawnPoint();
        GameObject newObject = Instantiate(objectToSpawn, spawnPoint, spawnRotation);
        return newObject;
    }

    private Vector3 GenerateSafeSpawnPoint() {
        Vector3 spawnPoint = GenerateRandomSpawnPoint();
        while (Physics.OverlapSphere(spawnPoint, minDistance, entityMask).Length > 0) {
            spawnPoint = GenerateRandomSpawnPoint();
        }
        return spawnPoint;
    }

    private Vector3 GenerateRandomSpawnPoint() {
        return new Vector3(Random.Range(boundsMin.x, boundsMax.x), spawnRegion.center.y + 0.1f, Random.Range(boundsMin.z, boundsMax.z));
    }

    public void EnableEnemyMovement(bool enable) {
        foreach (var enemy in enemies) {
            enemy.GetComponent<Enemy>().enabled = enable;
        }
    }

    public void IncreaseEnemySpeed() {
        for (int i = 0; i < enemies.Count; i++) {
            enemies[i].GetComponent<Enemy>().IncreaseSpeed();
        }
    }

    public void ResetEnemySpeed() {
        foreach (var enemy in enemies) {
            enemy.GetComponent<Enemy>().ResetSpeed();
        }
    }
}
