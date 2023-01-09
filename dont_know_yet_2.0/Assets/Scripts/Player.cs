using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rb;
    public float force = 500f;
    public LayerMask enemyMask;
    public LayerMask coinMask;

    private Vector3 moveInput;
    private Vector3 moveForce;

    public event System.Action OnDeath;
    public event System.Action OnCollectCoin;

    // Update is called once per frame
    void Update()
    {
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        moveForce = moveInput.normalized;
    }

    void FixedUpdate() {
        rb.AddForce(moveForce * force * Time.deltaTime, ForceMode.VelocityChange);
    }

    void OnCollisionEnter(Collision collision) {
        int collisionLayer = collision.gameObject.layer;
        int collisionLayerMask = 1 << collisionLayer;
        if ((enemyMask.value & collisionLayerMask) != 0) {
            OnDeath();
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
