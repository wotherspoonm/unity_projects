using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rb;
    public float force = 500f;

    private Vector3 moveInput;
    private Vector3 moveForce;

    // Update is called once per frame
    void Update()
    {
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        moveForce = moveInput.normalized;
    }

    void FixedUpdate() {
        rb.AddForce(moveForce * force * Time.deltaTime, ForceMode.VelocityChange);
    }
}
