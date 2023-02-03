using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Enemy {
    public LayerMask collisionMask;
    float lifetime = 5;
    float skinWidth = 0.1f; // To compenstate for the velocity of the enemies

    protected override void Start() {
        base.Start();
        Destroy(gameObject, lifetime);
    }

    public void SetSpeed(float newSpeed) {
        speed = newSpeed;
    }
    void Update() {
        float moveDistance = speed * Time.deltaTime;
        transform.Translate(Vector3.forward * moveDistance);
    }

    public override bool OnCollisionEnter(Collision collision) {
        Destroy(gameObject);
        if (base.OnCollisionEnter(collision)) {
            Vector3 bulletDirection = transform.forward;
            Vector3 forceVector = new Vector3(bulletDirection.x, 0, bulletDirection.z).normalized;
            float force = 50;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(bulletDirection * force, ForceMode.VelocityChange);
            return true;
        }
        return false;
    }
}