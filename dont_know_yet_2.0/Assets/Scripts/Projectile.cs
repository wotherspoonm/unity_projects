using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Enemy {
    public LayerMask collisionMask;
    float lifetime = 5;
    float skinWidth = 0.1f; // To compenstate for the velocity of the enemies

    void Start() {
        Destroy(gameObject, lifetime);

        Collider[] initialCollisions = Physics.OverlapSphere(transform.position, 0.1f, collisionMask);
        if (initialCollisions.Length > 0) {
            OnHitObject(initialCollisions[0], transform.position);
        }
    }

    public void SetSpeed(float newSpeed) {
        speed = newSpeed;
    }
    void Update() {
        float moveDistance = speed * Time.deltaTime;
        /*CheckCollisions(moveDistance);*/
        transform.Translate(Vector3.forward * moveDistance);
    }

    public override void OnCollisionEnter(Collision collision) {
        base.OnCollisionEnter(collision);
        Destroy(gameObject);
    }

    void CheckCollisions(float moveDistance) {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, moveDistance + skinWidth, collisionMask, QueryTriggerInteraction.Collide)) {
            OnHitObject(hit.collider, hit.point);
        }
    }

    void OnHitObject(Collider c, Vector3 hitPoint) {
        IDamageable damageableObject = c.GetComponent<IDamageable>();
        if (damageableObject != null) {
            damageableObject.TakeDamage(damage);
        }
        GameObject.Destroy(gameObject);
    }
}