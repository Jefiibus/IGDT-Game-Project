using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MiningLaser : MonoBehaviour
{
    public GameObject playerObj;
    public LineRenderer line;
    //public Transform lineEndPoint;
    Vector2 lineDefaultPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButton(1))
        {
            InvokeRepeating("FireBeam",0,0);
        }
        if (Input.GetMouseButtonUp(1))
        {
            CancelInvoke("FireBeam");
            CancelBeam();
        }
    }
    void FireBeam()
    {
        line.positionCount = 2;
        Vector2 playerPosition = playerObj.transform.position;
        Vector2 lineEndPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log("Firing laser");
        line.enabled = true;

        line.SetPosition(0, playerPosition);
        line.SetPosition(1, lineEndPoint);
        
        Debug.Log(lineEndPoint);
    }
    void CancelBeam()
    {
        line.positionCount = 0;
        line.enabled = false;
        
    }
}
