using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float sleepTime = 1f;
    public GameObject player;
    public SpawnManager spawnManager;
    public Text scoreText;
    public Text updateText;
    public int speedIncreaseInterval = 5;
    public int newEnemyInterval = 20;
    public float speedIncreaseMultiplier = 1.2f;

    private int score = 0;

    void Start()
    {
        spawnManager.SpawnEnemy();
        spawnManager.SpawnEnemy();
        spawnManager.SpawnCoin();
    }

    public void CollectCoin() {
        score++;
        spawnManager.SpawnCoin();
        scoreText.text = "Score: " + score;
        CheckDifficultyIncrease();
    }

    public void EndGame() {
        // Disable movement
        EnableMovement(false);
        Invoke("GameOver", sleepTime);
    }

    private void CheckDifficultyIncrease() {
        if (score % newEnemyInterval == 0) {
            spawnManager.SpawnEnemy();
            StartCoroutine(UpdateMessage("New Enemy!"));
        }
        else if (score % speedIncreaseInterval == 0) {
            for (int i = 0; i < spawnManager.enemies.Count; i++) {
                spawnManager.enemies[i].GetComponent<EnemyMovement>().force *= speedIncreaseMultiplier;
            }
            StartCoroutine(UpdateMessage("Speed Up!"));
        }
    }

    private void GameOver() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void EnableMovement(bool enable) {
        player.GetComponent<PlayerMovement>().enabled = enable;
        spawnManager.EnableEnemyMovement(enable);
    }

    IEnumerator UpdateMessage(string text) {
        updateText.text = text;
        updateText.enabled = true;
        yield return new WaitForSeconds(2);
        updateText.enabled = false;
    }
}
