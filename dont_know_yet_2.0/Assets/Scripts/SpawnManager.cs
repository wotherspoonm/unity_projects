using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject chaserPrefab;
    public GameObject tankPrefab;
    public GameObject coinPrefab;
    public Transform playerTransform;
    public List<GameObject> enemies = new List<GameObject>();
    public SpawnRegion spawnRegion;

    public void SpawnChaser() {
        SpawnEnemy(chaserPrefab);
    }

    public void SpawnTank() {
        GameObject newTank = SpawnEnemy(tankPrefab);
        newTank.GetComponent<Tank>().spawnRegion = spawnRegion;
    }
    public GameObject SpawnEnemy(GameObject enemyToSpawn) {
        GameObject newEnemy = SpawnObject(enemyToSpawn);
        newEnemy.GetComponent<Enemy>().playerTransform = playerTransform;
        enemies.Add(newEnemy);
        return newEnemy;
    }

    public void SpawnCoin() {
        SpawnObject(coinPrefab);
    }

    private GameObject SpawnObject(GameObject objectToSpawn) {
        Quaternion spawnRotation = new();
        Vector3 spawnPoint = spawnRegion.GenerateSafeSpawnPoint();
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
