using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class OreShrink : MonoBehaviour

{
    public float shrinkRate = 1.5f; 
    public float minSize = 0.5f;   

    private bool isShrinking = false;
    private float shrinkTimer = 0f;
    public int scorePerSecond = 10;
    private int randomSpin;
    private PlayerController playerControllerScript;
    private GameObject playerObj;
    public GameObject sonarPing;
    private float offsetDistance = 5f;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("PlayerObject").GetComponent<PlayerController>();
        playerObj = GameObject.Find("PlayerObject");
        randomSpin = Random.Range(-10,10);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,0,randomSpin*Time.deltaTime);
        if (isShrinking)
        {
            Vector3 newScale = transform.localScale - Vector3.one * shrinkRate * Time.deltaTime;

            
            newScale = new Vector3(
                Mathf.Max(newScale.x, 0),
                Mathf.Max(newScale.y, 0),
                Mathf.Max(newScale.z, 0)
            );
            transform.localScale = newScale;

            shrinkTimer += Time.deltaTime;
            if (shrinkTimer >= 0.1f)
            {
                playerControllerScript.AddScore(scorePerSecond);
                shrinkTimer = 0f;
            }

            if (transform.localScale.x <= minSize && transform.localScale.y <= minSize && transform.localScale.z <= minSize)
            {
                Destroy(gameObject);
            }
        }
    }
    public void StartMining()
    {
        isShrinking = true;
    }
    public void StopMining()
    {
        isShrinking = false;
    }
    public void FindPlayer()
    {
        Vector2 playerPosition = playerObj.transform.position;
        Vector2 asteroidPos = transform.position;
        Vector2 direction = (asteroidPos - playerPosition).normalized;
        Vector2 raycastStart = playerPosition + direction * offsetDistance;
        int layerMask = ~LayerMask.GetMask("Ignore Raycast");

        RaycastHit2D raycastHit = Physics2D.Raycast(raycastStart, direction,layerMask);

        if (raycastHit)
        {
            Vector2 point = raycastHit.point;
            Instantiate(sonarPing, point, Quaternion.identity);
        }
        
    }
}
