using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 playerDirection;
    public Rigidbody rb;
    public int damage = 1;
    public float speedIncreaseMultiplier = 1.2f;

    [SerializeField]
    protected float startSpeed = 20f;
    [SerializeField]
    protected float speed;
    protected LayerMask playerMask;

    private void Awake() {
        ResetSpeed();
        playerMask = LayerMask.GetMask("Player");
    }

    public void ResetSpeed() {
        speed = startSpeed;
    }

    public void IncreaseSpeed() {
        speed *= speedIncreaseMultiplier;
    }

    public virtual void OnCollisionEnter(Collision collision) {
        int collisionLayer = collision.gameObject.layer;
        int collisionLayerMask = 1 << collisionLayer;
        if ((playerMask.value & collisionLayerMask) != 0) {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null) {
                player.TakeDamage(damage);
            }
        }
    }
}
