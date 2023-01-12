using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : Enemy
{
    void FixedUpdate() {
        playerDirection = (playerTransform.position - transform.position).normalized;
        rb.AddForce(playerDirection * speed * Time.deltaTime, ForceMode.VelocityChange);
    }
}
