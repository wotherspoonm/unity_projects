using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : LivingEntity
{
    public Transform playerTransform;
    public Vector3 playerDirection;
    public Rigidbody rb;
    public float startForce = 20f;
    public float force;
    public float damage = 1f;

    private void Awake() {
        ResetSpeed();
    }

    public void ResetSpeed() {
        force = startForce;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerDirection = (playerTransform.position - transform.position).normalized;
        rb.AddForce(playerDirection * force * Time.deltaTime, ForceMode.VelocityChange);
    }
}
