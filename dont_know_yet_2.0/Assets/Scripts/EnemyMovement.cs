using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 playerDirection;
    public Rigidbody rb;
    public float force = 500f;

    // Update is called once per frame
    void FixedUpdate()
    {
        playerDirection = (playerTransform.position - transform.position).normalized;
        rb.AddForce(playerDirection * force * Time.deltaTime, ForceMode.VelocityChange);
    }
}
