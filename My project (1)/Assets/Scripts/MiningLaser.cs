using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

public class MiningLaser : MonoBehaviour
{
    public GameObject playerObj;
    public LineRenderer line;
    private float offsetDistance = 0.51f;
    private float maxDistance = 10f;
    private OreShrink lastHitObject = null;

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
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - playerPosition).normalized;
        Vector2 raycastStart = playerPosition + direction * offsetDistance;
        //Debug.Log("Firing laser");
        line.enabled = true;
        int layerMask = ~LayerMask.GetMask("Ignore Mining Laser");

        line.SetPosition(0, raycastStart);
        RaycastHit2D raycastHit = Physics2D.Raycast(raycastStart, direction, maxDistance, layerMask);
        if (raycastHit)
        {
            OreShrink shrinkable = raycastHit.collider.GetComponent<OreShrink>();
            if (shrinkable != null)
            {
                // Start shrinking the hit object
                shrinkable.StartMining();

                // Stop shrinking the previous object if it's different
                if (lastHitObject != null && lastHitObject != shrinkable)
                {
                    lastHitObject.StopMining();
                }

                // Update the last hit object
                lastHitObject = shrinkable;
            }
            else if (lastHitObject != null)
            {
                // Stop shrinking if no shrinkable object is hit
                lastHitObject.StopMining();
                lastHitObject = null;
                
            }
            line.SetPosition(1, raycastHit.point);
            //Debug.Log("Hit point: " + raycastHit.point);
        }

        else
        {
            // If no hit, extend the line to the maximum distance
            Vector2 lineEndPoint = raycastStart + direction * maxDistance;
            line.SetPosition(1, lineEndPoint);

            // Stop shrinking the last hit object
            if (lastHitObject != null)
            {
                lastHitObject.StopMining();
                lastHitObject = null;
            }

            //Debug.Log("No hit detected. Laser extends to: " + lineEndPoint);
        
        }
    }

    public void CancelBeam()
    {
        line.positionCount = 0;
        line.enabled = false;
        if (lastHitObject != null)
            {
                lastHitObject.StopMining();
                lastHitObject = null;
            }
    } 
}
