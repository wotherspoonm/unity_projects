using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotationSpeed = 3;
    void Update()
    {
        transform.Rotate(0, rotationSpeed, 0);
    }
}
