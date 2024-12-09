using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public float[] spawnArea = { -27f, 27f };
    public GameObject player;
    public float lastTime = 0f;

    // Update is called once per frame
    void Update()
    {
        //this caps checks to max. 10/s, it's not perfect, but better than checks=framerate
        if (ScoreManager.timeFromStart - lastTime > 0.1f)
        {
            //causes the spawnrate to increase slowly with a bit of randomness
            if (Random.Range(50f, 150f + ScoreManager.timeFromStart) / 150f > 1f)
            {
                //this part decides where to spawn the enemy, the spawner itself teleports to a position outside of where the player can see and creates an enemy at its own position
                if (Random.Range(0, 2) == 0)
                {
                    transform.position = player.transform.position + new Vector3(spawnArea[Random.Range(0, 2)], 0, Random.Range(spawnArea[0], spawnArea[1]));
                }
                else
                {
                    transform.position = player.transform.position + new Vector3(Random.Range(spawnArea[0], spawnArea[1]), 0, spawnArea[Random.Range(0, 2)]);
                }
                Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], new Vector3(transform.position.x, 0.3f, transform.position.z), transform.rotation);
            }
            //sets the lastTime variable to when the random check was last processed
            lastTime = ScoreManager.timeFromStart;
        }
    }
}
