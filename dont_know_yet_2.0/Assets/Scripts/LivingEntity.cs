using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable, ISpeedable
{
    public int startingHealth;
    public int health { get; protected set; }
    public bool dead;

    public float speedIncreaseMultiplier = 1.2f;
    [SerializeField]
    protected float startSpeed = 20f;
    [SerializeField]
    protected float speed;

    public event System.Action OnDeath;

    void Awake() {
        health = startingHealth;
        ResetSpeed();
    }

    protected virtual void Start() {

    }

    public void IncreaseSpeed() {
        speed *= speedIncreaseMultiplier;
    }

    public void ResetSpeed() {
        speed = startSpeed;
    }

    public virtual void TakeDamage(int damage) {
        health -= damage;
        if (health <= 0 && !dead) {
            Die();
        }
    }

    [ContextMenu("Self Destruct")]
    public virtual void Die() {
        dead = true;
        if (OnDeath != null) {
            OnDeath();
        }
    }
}
