using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Landmark
{
    public float X;
    public float Y;
    
    public Landmark(float x, float y)
    {
        X = x;
        Y = y;
    }
}