using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public BoxCollider spawnRegion;
    public GameObject chaserPrefab;
    public GameObject coinPrefab;
    public Transform playerTransform;
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
        Vector3 spawnPoint = new Vector3(Random.Range(boundsMin.x, boundsMax.x), spawnRegion.center.y + 0.1f, Random.Range(boundsMin.z, boundsMax.z));
        while (Mathf.Sqrt(Mathf.Pow(spawnPoint.x - playerTransform.position.x, 2) + Mathf.Pow(spawnPoint.z - playerTransform.position.z, 2)) < minDistance) {
            spawnPoint = new Vector3(Random.Range(boundsMin.x, boundsMax.x), spawnRegion.center.y + 0.1f, Random.Range(boundsMin.z, boundsMax.z));
        }
        GameObject newObject = Instantiate(objectToSpawn, spawnPoint, spawnRotation);
        return newObject;
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
