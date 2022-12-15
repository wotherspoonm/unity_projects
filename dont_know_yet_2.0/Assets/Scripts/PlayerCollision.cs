using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public GameManager gameManager;
    void OnCollisionEnter(Collision collision) {
        if (collision.collider.tag == "Enemy") {
            gameManager.EndGame();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Coin") {
            gameManager.CollectCoin();
            Destroy(other.gameObject);
        }
    }
}
