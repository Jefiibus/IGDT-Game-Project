using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreShrink : MonoBehaviour

{
    public float shrinkRate = 1.5f; 
    public float minSize = 0.5f;   

    private bool isShrinking = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isShrinking)
        {
            Vector3 newScale = transform.localScale - Vector3.one * shrinkRate * Time.deltaTime;

            
            newScale = new Vector3(
                Mathf.Max(newScale.x, 0),
                Mathf.Max(newScale.y, 0),
                Mathf.Max(newScale.z, 0)
            );
            transform.localScale = newScale;

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
