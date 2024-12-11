using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public string up = "w";
    public string down = "s";
    public string left = "a";
    public string right = "d";
    public float speed = 1.0f;
    public int score = 0;
    public int lastScore = 1000;
    private Spawner spawnerScript;
    private OreShrink oreShrinkScript;
    private FadeToWhite fadeToWhiteScript;
    // Start is called before the first frame update
    void Start()
    {
        spawnerScript = GameObject.Find("SpawnManager").GetComponent<Spawner>();
        fadeToWhiteScript = GameObject.Find("FadeToWhite").GetComponent<FadeToWhite>();
    }
    
    
    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKey(up))
        {
            transform.Translate(Vector2.up * Time.deltaTime * speed);
        }
        if (Input.GetKey(down))
        {
            transform.Translate(Vector2.down * Time.deltaTime * speed);
        }
        if (Input.GetKey(left))
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed);
        }
        if(Input.GetKey(right))
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed);
        }
        if(Input.GetKey(KeyCode.Space))
        {
            GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
            foreach (GameObject asteroid in asteroids)
        {
            oreShrinkScript = asteroid.GetComponent<OreShrink>();
            oreShrinkScript.FindPlayer();
        }
        }
        
        if (score>=lastScore && Input.GetKey(KeyCode.E))
        {
            StartCoroutine(FadeToWhite());
        }

    }
    IEnumerator FadeToWhite()
    {
        yield return new WaitForSeconds(3);
        fadeToWhiteScript.FadeIn();
        yield return new WaitForSeconds(1);
        NextLevel();
        yield return new WaitForSeconds(1);
        fadeToWhiteScript.FadeOut();
    }
    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Player score: " + score);
    }
    public void NextLevel()
    {
        lastScore = score+1000;
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject asteroid in asteroids)
        {
            Destroy(asteroid);
        }
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        transform.position = new Vector2(0,0);
        
        spawnerScript.SpawnAsteroidField();
    }
}
