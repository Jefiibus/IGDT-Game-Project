using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeToWhite : MonoBehaviour
{
    public float FadeRate;
    private Image image;
    public float targetAlpha;
    public AudioClip warp;
    private AudioSource AD;
    // Use this for initialization
    void Start () {
        this.image = this.GetComponent<Image>();
        if(this.image==null)
        {
            Debug.LogError("Error: No image on "+this.name);
        }
        this.targetAlpha = this.image.color.a;
        AD = GetComponent<AudioSource>();
        FadeOut();
    }
    
    // Update is called once per frame
    void Update () {
        Color curColor = this.image.color;
        float alphaDiff = Mathf.Abs(curColor.a-this.targetAlpha);
        if (alphaDiff>0.0001f)
        {
            curColor.a = Mathf.Lerp(curColor.a,targetAlpha,this.FadeRate*Time.deltaTime);
            this.image.color = curColor;
       }
    }

    public void FadeOut()
    {
        this.targetAlpha = 0.0f;
    }

    public void FadeIn()
    {
        AD.PlayOneShot(warp, 1f);
        this.targetAlpha = 1.0f;
    }
}