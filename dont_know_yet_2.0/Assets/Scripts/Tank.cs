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
    public float startDelayTime = 3;
    public SpawnRegion spawnRegion;

    [Header("Movement")]
    Vector2 moveTimeMinMax = new Vector2(3,5);
    Vector2 moveDelayMinMax = new Vector2(1, 5);
    float rotationTime = 2;

    float nextShotTime;
    float nextMoveTime;
    bool isMoving;

    void Awake() {
        nextShotTime = Time.time + startDelayTime;
        nextMoveTime = Time.time + startDelayTime;
    }

    void Update() {
        if (Time.time > nextShotTime) {
            Projectile newProjectile = Instantiate(bullet, projectileSpawn.position, projectileSpawn.rotation) as Projectile;
            newProjectile.SetSpeed(muzzleVelocity);
            nextShotTime = Time.time + msBetweenShots/1000/speed;
        }
        if (Time.time > nextMoveTime && !isMoving) {
            StartCoroutine(MoveTank());
        }

        tankTop.LookAt(playerTransform.position);
    }
    IEnumerator MoveTank() {
        isMoving = true;

        Vector3 pointToMoveTo = spawnRegion.GenerateSafeSpawnPoint();
        Vector3 correctedPoint = new Vector3(pointToMoveTo.x, transform.position.y, pointToMoveTo.z);
        Vector3 relativePos = correctedPoint - tankBottom.transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(relativePos);

        // Rotate tank
        float rotationSpeed = (1 / rotationTime) / speed;
        Quaternion originalRotation = tankBottom.transform.rotation;
        float percent = 0;
        while (percent < 1) {
            percent += Time.deltaTime * rotationSpeed;
            tankBottom.transform.rotation = Quaternion.Slerp(originalRotation, targetRotation, percent);
            yield return null;
        }

        yield return new WaitForSeconds(1);

        // Move tank
        float moveTime = Random.Range(moveTimeMinMax.x, moveTimeMinMax.y) / speed;
        float moveSpeed = 1 / moveTime;
        Vector3 originalPosition = transform.position;
        percent = 0;
        while (percent < 1) {
            percent += Time.deltaTime * moveSpeed;
            transform.position = Vector3.Lerp(originalPosition, correctedPoint, percent);
            yield return null;
        }

        nextMoveTime = Time.time + Random.Range(moveDelayMinMax.x, moveDelayMinMax.y)/speed;
        isMoving = false;
    }
}
