using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarPing : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SelfDestruct());
        Transform parentTransform = GameObject.Find("SonarTarget").transform;
        transform.SetParent(parentTransform, true);

    }

    // Update is called once per frame
    void LateUpdate()
    {
        
    }
    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
