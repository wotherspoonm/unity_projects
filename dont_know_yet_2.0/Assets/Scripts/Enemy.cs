using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : LivingEntity
{
    public Transform playerTransform;
    public Vector3 playerDirection;
    public Rigidbody rb;
    public int damage = 1;

    protected LayerMask playerMask;

    private void Awake() {
        playerMask = LayerMask.GetMask("Player");
        ResetSpeed();
    }

    public virtual bool OnCollisionEnter(Collision collision) {
        int collisionLayer = collision.gameObject.layer;
        int collisionLayerMask = 1 << collisionLayer;
        if ((playerMask.value & collisionLayerMask) != 0) {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null) {
                player.TakeDamage(damage);
                return true;
            }
        }
        return false;
    }
}
