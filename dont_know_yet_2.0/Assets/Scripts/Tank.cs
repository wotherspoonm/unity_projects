using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Enemy
{
    public Projectile bullet;
    public Transform projectileSpawn;
    public Transform tankTop;
    public Transform tankBottom;
    public float msBetweenShots = 100;
    public float muzzleVelocity = 20;

    float nextShotTime;

    void Update() {
        if (Time.time > nextShotTime) {
            Projectile newProjectile = Instantiate(bullet, projectileSpawn.position, projectileSpawn.rotation) as Projectile;
            newProjectile.SetSpeed(muzzleVelocity);
            nextShotTime = Time.time + msBetweenShots/1000;
        }

        tankTop.LookAt(playerTransform.position);
    }
}
