using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public float sleepTime = 1f;
    public GameObject player;
    public SpawnManager spawnManager;

    private int score = 0;
    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }
    public static GameManager Instance {
        get { return instance; }
    }

    void Start()
    {
        spawnManager.SpawnEnemy();
        spawnManager.SpawnEnemy();
        spawnManager.SpawnCoin();
    }

    public void CollectCoin() {
        score++;
        spawnManager.SpawnCoin();
        if (score %  5 == 0) {
            spawnManager.SpawnEnemy();
        }
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
        spawnManager.EnableEnemyMovement(enable);
    }
}
