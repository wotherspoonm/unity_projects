using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof (NavMeshAgent))]
public class Enemy : LivingEntity
{
    public enum State {Idle, Chasing, Attacking};
    State currentState;

    NavMeshAgent pathfinder;
    Transform target;
    LivingEntity targetEntity;
    Material skinMaterial;

    Color originalColour;

    float attackDistanceThreshold = 0.5f;
    float timeBetweenAttacks = 1;
    float damage = 1;

    float nextAttackTime;
    float myCollisionRadius;
    float targetCollisionRadius;

    bool hasTarget;
    protected override void Start()
    {
        base.Start();
        pathfinder = GetComponent<NavMeshAgent>();
        skinMaterial = GetComponent<Renderer>().material;
        originalColour = skinMaterial.color;

        target = GameObject.FindGameObjectWithTag("Player").transform;
        if (target != null) {
            currentState = State.Chasing;
            hasTarget = true;
            targetEntity = target.GetComponent<LivingEntity>();
            targetEntity.OnDeath += OnTargetDeath;

            myCollisionRadius = GetComponent<CapsuleCollider>().radius;
            targetCollisionRadius = GetComponent<CapsuleCollider>().radius;
            StartCoroutine(UpdatePath());
        }
    }

    void OnTargetDeath() {
        hasTarget = false;
        currentState = State.Idle;
    }

    void Update()
    {
        if (hasTarget) {
            if (Time.time > nextAttackTime) {
                float sqrDstToTarget = (target.position - transform.position).sqrMagnitude; // Save doing a sqrt operation as it is farily expensive
                if (sqrDstToTarget < Mathf.Pow(attackDistanceThreshold + myCollisionRadius + targetCollisionRadius, 2)) {
                    nextAttackTime = Time.time + timeBetweenAttacks;
                    StartCoroutine(Attack());
                }
            }
        }
    }

    /// <summary>
    /// Handles the attack of the enemy. The enemy will lunge at the target and return to its original position
    /// The motion of the enemy while attacking follows a parabolic curve.
    /// </summary>
    /// <returns></returns>
    IEnumerator Attack() {
        currentState = State.Attacking;
        pathfinder.enabled = false;

        Vector3 originalPosition = transform.position;
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        Vector3 attackPosition = target.position - directionToTarget * (myCollisionRadius);

        float attackSpeed = 3;
        float percent = 0; // How far into the lunge we are

        skinMaterial.color = Color.red;
        bool hasAppliedDamage = false;

        while (percent <= 1) {
            if (percent >= 0.5f && !hasAppliedDamage) {
                hasAppliedDamage = true;
                targetEntity.TakeDamage(damage);
            }
            percent += Time.deltaTime * attackSpeed;
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4; // A parabolic equation that has x intercepts at 0 and 1, and reaches a height of 1
            transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);
            yield return null;
        }
        skinMaterial.color = originalColour;
        currentState = State.Chasing;
        pathfinder.enabled = true;
    }

    /// <summary>
    /// Function to update the path of the enemy to follow the player. The direction to the target is computed, and this is used along with the
    /// radiuses of both the enemy and target to compute the target position for the path finder. The radiuses are used to ensure the colliders don't
    /// overlap
    /// </summary>
    /// <returns></returns>
    IEnumerator UpdatePath() {
        float refreshRate = 0.25f;
        while (hasTarget) {
            if (currentState == State.Chasing) {
                Vector3 directionToTarget = (target.position - transform.position).normalized;
                Vector3 targetPosition = target.position - directionToTarget * (myCollisionRadius + targetCollisionRadius + attackDistanceThreshold/2); // attackDistanceThreshold/2 ensures that the enemy can lunge at the target from the target position
                if (!dead) {
                    pathfinder.SetDestination(targetPosition);
                }
            }
            yield return new WaitForSeconds(refreshRate);
        }
    }
}
