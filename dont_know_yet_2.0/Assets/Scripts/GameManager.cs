using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform playerTransform;

    public List<GameObject> enemies = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        Quaternion spawnRotation = new();
        Vector3 startPos1 = new Vector3(10, 1, 10);
        GameObject enemy1 = Instantiate(enemyPrefab, startPos1, spawnRotation);
        enemy1.GetComponent<EnemyMovement>().playerTransform = playerTransform;
        enemies.Add(enemy1);
        Vector3 startPos2 = new Vector3(-10, 1, -10);
        GameObject enemy2 = Instantiate(enemyPrefab, startPos2, spawnRotation);
        enemy2.GetComponent<EnemyMovement>().playerTransform = playerTransform;
        enemies.Add(enemy2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
