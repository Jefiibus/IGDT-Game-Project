using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mininglasersound : MonoBehaviour
{
    public AudioClip LS;
    private AudioSource AD;
    private MiningLaser ML;
    // Start is called before the first frame update
    void Start()
    {
        AD = GetComponent<AudioSource>();
        ML = GameObject.Find("Player").GetComponent<MiningLaser>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ML.isfiring == true && !AD.isPlaying) 
        {
            AD.PlayOneShot(LS, 0.3f);
        }
        if (ML.isfiring == false)
        {
            AD.Stop();
        }
    }
}
