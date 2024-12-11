using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemyPrefabsForStart;
    public GameObject[] enemyPrefabs;
    public float spawnLimit = 27f;
    private float[] spawnArea = { -27f, 27f };
    public GameObject player;
    public GameObject[] asteroidPrefabs;
    public int objectCount;
    public Vector2 areaSize = new Vector2(100,100);
    public float minDistance = 15f;
    private List<Vector2> spawnedPositions = new List<Vector2>();
    public float lastTime = 0f;
    private PlayerController playerControllerScript;

    void Start()
    {
        playerControllerScript = GameObject.Find("PlayerObject").GetComponent<PlayerController>();
        SpawnAsteroidField();
        SpawnEnemies();
    }
    public void FixedUpdate()
    {
        spawnLimit = Random.Range(20f, 30f);
        spawnArea[0] = -spawnLimit;
        spawnArea[1] = spawnLimit;
    }
    // Update is called once per frame
    void Update()
    {
        //this caps checks to max. 10/s, it's not perfect, but better than checks=framerate
        if (ScoreManager.timeFromStart - lastTime > 0.1f && playerControllerScript.score >= playerControllerScript.lastScore)
        {
            //causes the spawnrate to increase slowly with a bit of randomness
            if (Random.Range(50f, 150f + ScoreManager.timeFromStart) / 150f > 1f)
            {
                //this part decides where to spawn the enemy, the spawner itself teleports to a position outside of where the player can see and creates an enemy at its own position
                if (Random.Range(0, 2) == 0)
                {
                    transform.position = player.transform.position + new Vector3(spawnArea[Random.Range(0, 2)], Random.Range(spawnArea[0], spawnArea[1]),0);
                }
                else
                {
                    transform.position = player.transform.position + new Vector3(Random.Range(spawnArea[0], spawnArea[1]), spawnArea[Random.Range(0, 2)],0);
                }
                Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
            }
            //sets the lastTime variable to when the random check was last processed
            lastTime = ScoreManager.timeFromStart;
        }
    }
    public void SpawnAsteroidField()
    {
        SpawnObjects(20, asteroidPrefabs, false);
    }
    public void SpawnEnemies()
    {
        SpawnObjects(20, enemyPrefabsForStart, true);
    }

    public void SpawnObjects(int objectCount, GameObject[] objectType, bool clearSpawn)
    {
        spawnedPositions.Add(new Vector2(0,0));
        for (int i = 0; i < objectCount; i++)
        {
            int attempts = 0;
            bool validPositionFound = false;
            Vector2 spawnPosition = Vector2.zero;

            while (!validPositionFound && attempts < 100)
            {
                attempts++;
                spawnPosition = GetRandomPosition();

                if (IsPositionValid(spawnPosition))
                {
                    validPositionFound = true;
                }
            }

            if (validPositionFound)
            {
                spawnedPositions.Add(spawnPosition);
                Instantiate(objectType[Random.Range(0,objectType.Length)], spawnPosition, Quaternion.identity);
            }
            else
            {
                Debug.Log("Could not find a valid position for object " + (i + 1));
            }
        }
        if (clearSpawn)
        {
        spawnedPositions.Clear();
        }
    }
    Vector2 GetRandomPosition()
    {
        float x = Random.Range(-areaSize.x / 2, areaSize.x / 2);
        float y = Random.Range(-areaSize.y / 2, areaSize.y / 2);
        return new Vector2(x, y);
    }
    bool IsPositionValid(Vector2 position)
    {
        foreach (Vector2 spawnedPosition in spawnedPositions)
        {
            if (Vector2.Distance(position, spawnedPosition) < minDistance)
            {
                return false;
            }
        }
        return true;
    }
}
