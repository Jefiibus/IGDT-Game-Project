using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreShrink : MonoBehaviour

{
    public float shrinkRate = 1.5f; 
    public float minSize = 0.5f;   

    private bool isShrinking = false;
    private float shrinkTimer = 0f;
    public int scorePerSecond = 10;
    private PlayerController playerControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("PlayerObject").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,0,10*Time.deltaTime);
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
}
