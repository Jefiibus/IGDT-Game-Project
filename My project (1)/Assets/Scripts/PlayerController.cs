using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI warpText;
    public TextMeshProUGUI warpCountdown;
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
    public List<GameObject> backgrounds;
    private GameObject currentBackground;
    private GameObject pauseMenu;
    private bool readyToWarp = true;
    private bool sonarReady = true;
    public bool iFrames = true;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: " + score;
        warpText.text = "";
        spawnerScript = GameObject.Find("SpawnManager").GetComponent<Spawner>();
        fadeToWhiteScript = GameObject.Find("FadeToWhite").GetComponent<FadeToWhite>();
        currentBackground = backgrounds.Find(bg => bg.activeSelf);
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
        if(Input.GetKey(KeyCode.Space) && sonarReady)
        {
            StartCoroutine(SonarCooldown());
            sonarReady = false;
            GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
            foreach (GameObject asteroid in asteroids)
        {
            oreShrinkScript = asteroid.GetComponent<OreShrink>();
            oreShrinkScript.FindPlayer();
        }
        }
        
        if (score>=lastScore && Input.GetKey(KeyCode.E) && readyToWarp)
        {
            StartCoroutine(FadeToWhite());
            readyToWarp = false;
        }

    }
    IEnumerator SonarCooldown()
    {
        yield return new WaitForSeconds(3);
        sonarReady = true;
    }
    IEnumerator FadeToWhite()
    {
        warpCountdown.text = "3";
        yield return new WaitForSeconds(1);
        warpCountdown.text = "2";
        yield return new WaitForSeconds(1);
        warpCountdown.text = "1";
        yield return new WaitForSeconds(1);
        iFrames = false;
        warpCountdown.text = "";
        warpText.text = "";
        fadeToWhiteScript.FadeIn();
        yield return new WaitForSeconds(1);
        NextLevel();
        yield return new WaitForSeconds(1);
        fadeToWhiteScript.FadeOut();
        readyToWarp = true;
        iFrames = true;
    }
    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score;
        if (score >= lastScore)
        {
            warpText.text = "WARP READY";
        }
        Debug.Log("Player score: " + score);
    }
    public void NextLevel()
    {
        lastScore = score+1000;
        warpText.text = "";
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
        spawnerScript.SpawnEnemies();
        
        GameObject newBackground;
        do
        {
            newBackground = backgrounds[Random.Range(0, backgrounds.Count)];
        } while (newBackground == currentBackground);
        currentBackground.SetActive(false);
        newBackground.SetActive(true);
        currentBackground = newBackground;
    }
    public void RestartButton()
    {
        StartCoroutine(RestartGame());
    }
    IEnumerator RestartGame()
    {
        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.SetActive(false);
        iFrames = false;
        Time.timeScale = 1;
        fadeToWhiteScript.FadeIn();
        yield return new WaitForSecondsRealtime(1);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    public void QuitToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("menu scene");
    }
}
