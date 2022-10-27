using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rb;
    public float force = 500f;

    private bool moveLeft = false;
    private bool moveRight = false;
    private bool moveUp = false;
    private bool moveDown = false;

    // Update is called once per frame
    void Update()
    {
        moveLeft = moveRight = moveDown = moveUp = false;
        if (Input.GetKey("w")) {
            moveUp = true;
        }
        if (Input.GetKey("s")) {
            moveDown = true;
        }
        if (Input.GetKey("a")) {
            moveLeft = true;
        }
        if (Input.GetKey("d")) {
            moveRight = true;
        }
    }

    void FixedUpdate() {
        if (moveUp) {
            rb.AddForce(0, 0, force * Time.deltaTime, ForceMode.VelocityChange);
        }
        if (moveDown) {
            rb.AddForce(0, 0, -force * Time.deltaTime, ForceMode.VelocityChange);
        }
        if (moveLeft) {
            rb.AddForce(-force * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }
        if (moveRight) {
            rb.AddForce(force * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }
    }
}
