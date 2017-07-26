using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScle : TimeBehaviour
{
    [SerializeField]private Vector3 scole;

    public Vector3 Scole
    {
        get { return scole;}
        set
        { scole = value; }
    }
    public float Speed
    {
        get { return speed; } set { speed = value; }

    }

    [SerializeField] private float speed;
    

    public override void UpdateTime(float deltaTime)
    {
        transform.localScale += Scole * deltaTime;
    }
}
