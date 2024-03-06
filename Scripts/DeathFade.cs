using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathFade : MonoBehaviour
{
    public Image Imagepourlebiz;
    float alphaFadeValue = 0.0f;


    public IEnumerator FadeDeath()
    {
        Imagepourlebiz.enabled = true;
        Time.timeScale = 0f;
        for (int i = 0; i < 100; i++) 
        {
            alphaFadeValue += i/100;
            Imagepourlebiz.color = new Color (0,0,0, alphaFadeValue);
            yield return new WaitForSeconds(0.005f);
        }
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < 100; i++)
        {
            alphaFadeValue += 1 - (i / 100);
            Imagepourlebiz.color = new Color(0, 0, 0, alphaFadeValue);
            yield return new WaitForSeconds(0.005f);
        }
        Time.timeScale = 1f;
        Imagepourlebiz.enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
