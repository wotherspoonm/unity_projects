using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LivingEntity
{
    // Start is called before the first frame update
    public Rigidbody rb;
    public float force = 500f;
    public LayerMask enemyMask;
    public LayerMask coinMask;

    private Vector3 moveInput;
    private Vector3 moveDirection;

    public event System.Action OnCollectCoin;

    // Update is called once per frame
    void Update()
    {
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        moveDirection = moveInput.normalized;
    }

    void FixedUpdate() {
        rb.AddForce(moveDirection * force * Time.deltaTime, ForceMode.VelocityChange);
    }

    void OnCollisionEnter(Collision collision) {
        int collisionLayer = collision.gameObject.layer;
        int collisionLayerMask = 1 << collisionLayer;
        if ((enemyMask.value & collisionLayerMask) != 0) {
            base.TakeDamage(collision.gameObject.GetComponent<Enemy>().damage);
        }
    }

    private void OnTriggerEnter(Collider other) {
        int collisionLayer = other.gameObject.layer;
        int collisionLayerMask = 1 << collisionLayer;
        if ((coinMask.value & collisionLayerMask) != 0) {
            OnCollectCoin();
            Destroy(other.gameObject);
        }
    }
}
