using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public int startingHealth;
    public int health { get; protected set; }
    public bool dead;

    public event System.Action OnDeath;

    void Awake() {
        health = startingHealth;
    }

    protected virtual void Start() {

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
