using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rb;
    public float force = 500f;

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
        if (collision.collider.tag == "Enemy") {
            OnDeath();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Coin") {
            OnCollectCoin();
            Destroy(other.gameObject);
        }
    }
}
