using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision) {
        if (collision.collider.tag == "Enemy") {
            GameManager.Instance.EndGame();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Coin") {
            GameManager.Instance.CollectCoin();
            Destroy(other.gameObject);
        }
    }
}
