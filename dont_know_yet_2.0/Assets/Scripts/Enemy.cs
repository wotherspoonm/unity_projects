using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : LivingEntity
{
    public Transform playerTransform;
    public Vector3 playerDirection;
    public Rigidbody rb;
    public int damage = 1;
    public float speedIncreaseMultiplier = 1.2f;

    [SerializeField]
    protected float startSpeed = 20f;
    [SerializeField]
    protected float speed;

    private void Awake() {
        ResetSpeed();
    }

    public void ResetSpeed() {
        speed = startSpeed;
    }

    public void IncreaseSpeed() {
        speed *= speedIncreaseMultiplier;
    }
}
