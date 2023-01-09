using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject scoreUI;
    public Image fadePlane;
    public RectTransform heartPrefab;
    public RectTransform health;
    public float gapWidth = 5f;

    Player player;
    RectTransform[] healthHearts;
    int playerHealthLastFrame;

    void Start() {
        player = FindObjectOfType<Player>();
        player.OnDeath += OnGameOver;
        float xPos = 0;
        healthHearts = new RectTransform[player.startingHealth];
        for (int i = 0; i < player.health; i++) {
            healthHearts[i] = Instantiate(heartPrefab, health.position + (Vector3.right * xPos), Quaternion.identity);
            healthHearts[i].SetParent(scoreUI.transform);
            xPos += healthHearts[i].localScale.x * healthHearts[i].rect.width + gapWidth; // Because the health prefab has been scaled down, and rect.width returns the width width of the unscaled prefab we need to multiply by localScale.x
        }

        playerHealthLastFrame = player.startingHealth;
    }

    void Update() {
        if (playerHealthLastFrame != player.health) {
            Destroy(healthHearts[playerHealthLastFrame - 1].gameObject);
            playerHealthLastFrame = player.health;
        }
    }

    void OnGameOver() {
        Cursor.visible = true;
        StartCoroutine(Fade(Color.clear, new Color(0, 0, 0, 0.8f), 1));
        scoreUI.gameObject.SetActive(false);
        gameOverUI.SetActive(true);
    }

    IEnumerator Fade(Color from, Color to, float time) {
        float speed = 1 / time;
        float percent = 0;

        while (percent < 1) {
            percent += Time.deltaTime * speed;
            fadePlane.color = Color.Lerp(from, to, percent);
            yield return null;
        }
    }

    // UI Input
    public void StartNewGame() {
        SceneManager.LoadScene("Game");
    }

    public void ReturnToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
