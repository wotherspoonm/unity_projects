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
    public Text highScoreText;
    public Text updateText;
    public int speedIncreaseInterval = 5;
    public int newEnemyInterval = 20;
    public bool devMode;

    private int score = 0;
    private int highScore = 0;

    void Awake() {
        player = FindObjectOfType<Player>();
        player.OnDeath += OnDeath;
        player.OnCollectCoin += OnCollectCoin;
    }

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateScore();
        spawnManager.SpawnTank();
        spawnManager.SpawnCoin();
    }

    void Update() {
        if (devMode) {
            if (Input.GetKeyDown(KeyCode.Return)) {
                score++;
                scoreText.text = "Score: " + score;
                CheckDifficultyIncrease();
            }
            if (Input.GetKeyDown(KeyCode.Backspace)) {
                spawnManager.SpawnChaser();
            }
        }
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
        UpdateScore();
        CheckDifficultyIncrease();
    }

    public void EndGame() {
        // Disable movement
        PlayerPrefs.SetInt("HighScore", highScore);
        EnableMovement(false);
    }

    private void CheckDifficultyIncrease() {
        if (score % newEnemyInterval == 0) {
            spawnManager.SpawnChaser();
            ResetSpeed();
            StartCoroutine(UpdateMessage("New Enemy!"));
        }
        else if (score % speedIncreaseInterval == 0) {
            IncreaseSpeed();
            StartCoroutine(UpdateMessage("Speed Up!"));
        }
    }

    private void IncreaseSpeed() {
        player.GetComponent<ISpeedable>().IncreaseSpeed();
        spawnManager.IncreaseEnemySpeed();
    }

    private void ResetSpeed() {
        player.GetComponent<ISpeedable>().ResetSpeed();
        spawnManager.ResetEnemySpeed();
    }

    private void EnableMovement(bool enable) {
        player.enabled = enable;
        spawnManager.EnableEnemyMovement(enable);
    }

    private void UpdateScore() {
        scoreText.text = "Score: " + score;
        if (score > highScore) {
            highScore = score;
        }
        highScoreText.text = "High Score: " + highScore;
    }

    IEnumerator UpdateMessage(string text) {
        updateText.text = text;
        updateText.enabled = true;
        yield return new WaitForSeconds(2);
        updateText.enabled = false;
    }
}
