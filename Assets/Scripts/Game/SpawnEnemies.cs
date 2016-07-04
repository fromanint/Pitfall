using UnityEngine;
using System.Collections;

public class SpawnEnemies : MonoBehaviour {

    [SerializeField]
    GameObject[] Enemies;        // Array of Obstacles prefabs.
    [SerializeField]
    GameObject parent;           //set a parent to keep organize the debug while test.   
    [SerializeField]
    float spawnTime = 10f;        // The amount of time between each spawn.
    [SerializeField]
    public float spawnDelay = 20f;       // The amount of time before spawning starts.


    void Start()
    {
        // Start calling the Spawn function repeatedly after a delay .
        InvokeRepeating("Spawn", spawnDelay, spawnTime);
    }
  

 void Spawn()
    {
        // Instantiate a random enemy.
     
            int enemyIndex = Random.Range(0, Enemies.Length);
            GameObject GO = Instantiate(Enemies[enemyIndex], transform.position, transform.rotation) as GameObject;
      
      
        
    }
}
