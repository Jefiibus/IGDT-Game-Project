using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float spread = 0f;
    public float fireDelay = 0.2f;
    public float preventSpamfire = 0.5f;
    public Camera playerCam;
    public float rotation;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - transform.position;
        float angle = Vector2.SignedAngle(Vector2.right, direction);
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

    void Fire()
    {
        //instantiates the bullet, turns it in the right direction and plays sound+gunsmoke
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.transform.Rotate(0, 180f, 0);
        bullet.transform.Translate(Vector3.zero);
    }
}
