using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class Aim : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float spread = 0f;
    public float fireDelay = 0.2f;
    public float preventSpamfire = 0.5f;
    public Camera playerCam;
    public float rotation;
    public bool paused = false;
    private PlayerController playerControllerScript;
    private LeaderboardManager leaderboardManagerScript;
    private GameObject pauseMenu;
    private GameObject continueButton;
    private damageable damageableScript;
    private bool gameRestarted = false;
    public TextMeshProUGUI pauseText;
    private AudioSource AD;
    public AudioClip fire;
    public AudioClip ded;
    // Start is called before the first frame update
    void Start()
    {
        leaderboardManagerScript = GameObject.Find("LeaderboardManager").GetComponent<LeaderboardManager>();
        playerControllerScript = GameObject.Find("PlayerObject").GetOrAddComponent<PlayerController>();
        pauseMenu = GameObject.Find("PauseMenu");
        continueButton = GameObject.Find("ContinueButton");
        pauseMenu.SetActive(false);
        damageableScript = GameObject.Find("Player").GetComponent<damageable>();
        AD = GetComponent<AudioSource>();
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            PauseGame();
        }
        
        if (paused == false)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePosition - transform.position;
            float angle = Vector2.SignedAngle(Vector2.up, direction);
            transform.eulerAngles = new Vector3(0, 0, angle);

            //starts firing on button downpress, firing a bullet every n seconds, this delay is also applied to the first shot as otherwise you could use a macro to spam lmb
            if (fireDelay <= preventSpamfire && Input.GetMouseButtonDown(0))
            {
                preventSpamfire = Time.deltaTime;
                InvokeRepeating("Fire", 0, fireDelay);
            }
            else
            {
                preventSpamfire += Time.deltaTime;
            }
            //stops firing when mouse is no longer being held
            if (Input.GetMouseButtonUp(0))
            {
                CancelInvoke("Fire");
            }
        }
        if (damageableScript.IsAlive == false && gameRestarted == false)
        {

            AD.PlayOneShot(ded, 0.5f);
            gameRestarted = true;
            PauseGame();
            continueButton.SetActive(false);
            pauseText.text = "Game Over";
            leaderboardManagerScript.ShowUsernamePrompt();

        }
    }

    void Fire()
    {
        if (paused == false)
        {
            //instantiates the bullet, turns it in the right direction and plays sound+gunsmoke
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            bullet.transform.Rotate(0, 180f, 0);
            bullet.transform.Translate(Vector3.up);
            AD.PlayOneShot(fire, 0.3f);
        }
    }
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        paused = true;
    }
    public void UnPauseGame()
    {
        pauseMenu.SetActive(false);
        paused = false;
        Time.timeScale = 1;
    }
}