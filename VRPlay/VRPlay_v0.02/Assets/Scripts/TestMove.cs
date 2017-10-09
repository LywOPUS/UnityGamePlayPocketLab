using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    public WheelCollider front;
    public WheelCollider back;
    public UnityEngine.TextMesh speedText;
    public Rigidbody rigi;
    private int count = 6;
    private List<GameObject> tempList = new List<GameObject>();

    private void Update()
    {
        speedText.text = "RPM: " + back.rpm.ToString() + "\n"
                       + "Speed" + rigi.velocity;
        ;
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            //back.motorTorque++;
            //Debug.Log(back.motorTorque);

            //Debug.Log(rigi.velocity);
        }
        if (Input.GetMouseButtonUp(0))
        {
            //   back.motorTorque--;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "LoadNext")
        {
            if (tempList.Count > 0)
            {
                foreach (var item in tempList)
                {
                    Debug.Log("tempList:" + item.name);
                    SimpleObjPool.Instance.BackRes(item.name, item);
                }
            }
            Debug.Log("LoadNext");
            var pos = new Vector3(0, 0, 100);
            other.transform.position += pos;
            NatureScenesPwan.Instance.UpdateSelf(pos, 6, 1);

            foreach (var item in tempList)
            {
                Debug.Log("tempList:" + item.name);
                SimpleObjPool.Instance.BackRes(item.name, item);
            }
        }
    }
}