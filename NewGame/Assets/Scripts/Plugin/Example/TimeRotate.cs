using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRotate : TimeBehaviour {

    public Vector3 My_EulerAngle
    {
        get { return my_eulerAngle; }
        set { my_eulerAngle = value; }
    }

    [SerializeField] private Vector3 my_eulerAngle;
    [SerializeField] private float rate;

    public override void UpdateTime(float deltaTime)
    {
        transform.rotation *= Quaternion.Euler(my_eulerAngle*rate*Time.deltaTime);
    }



}
