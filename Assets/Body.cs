using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    public SpriteRenderer Base;
    public SpriteRenderer Eye;
    public SpriteRenderer Mouth;

    public List<Sprite> Eyes = new List<Sprite>();
    public List<Sprite> Mouthes = new List<Sprite>();

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(Blink());    
        }
    }

    IEnumerator Blink()
    {
        float a = 0.05f;
        Eye.sprite = Eyes[0];
        yield return new WaitForSeconds(a);
        Eye.sprite = Eyes[1];
        yield return new WaitForSeconds(a);
        Eye.sprite = Eyes[2];
        yield return new WaitForSeconds(a);
        Eye.sprite = Eyes[3];
        yield return new WaitForSeconds(a);
        Eye.sprite = Eyes[0];
    }
}
