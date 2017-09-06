using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBox : MonoBehaviour
{
    private Transform thisTransform;
    public Quaternion targetQuaternion;
    public float speed = 0.1f;

    //int inputCount = 0;
    public static RotateBox R;

    public float AxisYangle = 0;
    public float AxisXangle = 0;
    public float AxisZangel = 0;

    // Use this for initialization
    private void Start()
    {
        R = this;
        thisTransform = this.gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    private void Update()
    {
        targetQuaternion.eulerAngles = new Vector3(AxisXangle, AxisYangle, AxisZangel);
        thisTransform.localRotation = Quaternion.Lerp(thisTransform.localRotation, targetQuaternion, Time.deltaTime * speed);
        if (Input.GetKeyDown(KeyCode.D))
        {
            AxisYangle -= 90;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            AxisYangle += 90;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            AxisXangle += 90;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            AxisXangle -= 90;
        }
    }
}