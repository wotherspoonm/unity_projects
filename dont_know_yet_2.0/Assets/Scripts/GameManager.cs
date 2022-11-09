using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public GameObject enemyPrefab;
    public Transform playerTransform;
    public float sleepTime = 1f;

    public GameObject player;
    public List<GameObject> enemies = new List<GameObject>();
    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        LoadObjects();
    }
    public static GameManager Instance {
        get { return instance; }
    }

    void LoadObjects()
    {
        Quaternion spawnRotation = new();
        Vector3 startPos1 = new Vector3(10, 1, 10);
        GameObject enemy1 = Instantiate(enemyPrefab, startPos1, spawnRotation);
        enemy1.GetComponent<EnemyMovement>().playerTransform = playerTransform;
        enemies.Add(enemy1);
        Vector3 startPos2 = new Vector3(-10, 1, -10);
        GameObject enemy2 = Instantiate(enemyPrefab, startPos2, spawnRotation);
        enemy2.GetComponent<EnemyMovement>().playerTransform = playerTransform;
        enemies.Add(enemy2);
    }

    public void EndGame() {
        // Disable movement
        EnableMovement(false);
        Invoke("Restart", sleepTime);
    }

    private void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void EnableMovement(bool enable) {
        player.GetComponent<PlayerMovement>().enabled = enable;
        foreach (var enemy in enemies) {
            enemy.GetComponent<EnemyMovement>().enabled = enable;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
