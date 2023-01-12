using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float sleepTime = 1f;
    public Player player;
    public SpawnManager spawnManager;
    public Text scoreText;
    public Text updateText;
    public int speedIncreaseInterval = 5;
    public int newEnemyInterval = 20;

    private int score = 0;

    void Awake() {
        player = FindObjectOfType<Player>();
        player.OnDeath += OnDeath;
        player.OnCollectCoin += OnCollectCoin;
    }

    void Start()
    {
        spawnManager.SpawnTank();
        spawnManager.SpawnCoin();
    }

    void OnDeath() {
        EndGame();
    }

    void OnCollectCoin() {
        CollectCoin();
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
    }

    private void CheckDifficultyIncrease() {
        if (score % newEnemyInterval == 0) {
            spawnManager.SpawnChaser();
            spawnManager.ResetEnemySpeed();
            StartCoroutine(UpdateMessage("New Enemy!"));
        }
        else if (score % speedIncreaseInterval == 0) {
            spawnManager.IncreaseEnemySpeed();
            StartCoroutine(UpdateMessage("Speed Up!"));
        }
    }

    private void EnableMovement(bool enable) {
        player.enabled = enable;
        spawnManager.EnableEnemyMovement(enable);
    }

    IEnumerator UpdateMessage(string text) {
        updateText.text = text;
        updateText.enabled = true;
        yield return new WaitForSeconds(2);
        updateText.enabled = false;
    }
}
