using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LivingEntity
{
    // Start is called before the first frame update
    public Rigidbody rb;
    public float force = 500f;
    public float startForce = 500f;
    public LayerMask enemyMask;
    public LayerMask coinMask;
    public float flashTime = 0.5f;
    public int numberOfFlashes = 3;

    Vector3 moveInput;
    Vector3 moveDirection;
    bool isInvincible;

    public event System.Action OnCollectCoin;

    // Update is called once per frame
    void Update()
    {
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        moveDirection = moveInput.normalized;
    }

    void FixedUpdate() {
        rb.AddForce(moveDirection * force * speed * Time.deltaTime, ForceMode.VelocityChange);
    }

    public override void TakeDamage(int damage) {
        if (!isInvincible && !dead) {
            StartCoroutine(AnimateInvincibility());
            base.TakeDamage(damage);
        }
    }

    private void OnTriggerEnter(Collider other) {
        int collisionLayer = other.gameObject.layer;
        int collisionLayerMask = 1 << collisionLayer;
        if ((coinMask.value & collisionLayerMask) != 0) {
            OnCollectCoin();
            Destroy(other.gameObject);
        }
    }

    IEnumerator AnimateInvincibility() {
        isInvincible = true;
        Color originalColor = Color.green;
        Color targetColor = Color.blue;

        Material mat = GetComponent<Renderer>().material;

        for (int i = 0; i < numberOfFlashes; i++) {
            float flashSpeed = 1 / flashTime;
            float percent = 0;
            while (percent < 1) {
                percent += Time.deltaTime * flashSpeed;
                float interpolation = 1 - Mathf.Abs(2 * percent - 1);
                mat.color = Color.Lerp(originalColor, targetColor, interpolation);
                yield return null;
            }
        }
        isInvincible = false;
    }
}
