using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * All entities that can take damage need to inherit from this interface. The use of an interface allows for a single check for a type of class
 * when collisions occur instead of multiple checks for multiple types of classes.
 */
public interface IDamageable {

    void TakeHit(float damage, RaycastHit hit);

}
