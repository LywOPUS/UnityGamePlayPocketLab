using UnityEngine;
using System.Collections;

public class Truck : MonoBehaviour
{
    public WheelCollider[] wheelColliders = new WheelCollider[4];
    public GameObject[] wheelMeshs = new GameObject[2];
    public Rigidbody bickRigi;

    public Transform head;
    public SteamVR_TrackedObject leftHand;
    public SteamVR_TrackedObject rightHand;
    public Transform bike;
    public Transform handle;

    private bool isdrive = false;
    private float accelerate = 1;

    private void Start()
    {
        // bickRigi.centerOfMass.Set(0, -0.3f, 0);
    }

    private void Update()
    {
        UpdateMeshesPositions();
        var lDevice = SteamVR_Controller.Input(((int)leftHand.index));
        var rDevice = SteamVR_Controller.Input(((int)rightHand.index));
        if (lDevice.GetHairTrigger() && rDevice.GetHairTrigger())
        {
            isdrive = true;
        }
        if (lDevice.GetHairTriggerUp() || rDevice.GetHairTriggerUp())
        {
            isdrive = false;
        }
    }

    private void FixedUpdate()
    {
        // 前轮转向

        Vector3 dir = leftHand.transform.position - rightHand.transform.position;
        var angle = Vector3.Angle(dir, bike.transform.position) + 90;
        Debug.DrawRay(bike.position, dir, Color.red);

        handle.localRotation = Quaternion.Euler(0, angle, 0);

        wheelColliders[0].steerAngle = angle;
        wheelColliders[1].steerAngle = angle;

        // 后轮添加驱动力
        if (isdrive)
        {
            Debug.Log("is drive");
            wheelColliders[2].motorTorque = accelerate * 1000;
            wheelColliders[3].motorTorque = accelerate * 1000;
        }
        if (isdrive == false)
        {
            // wheelColliders[2].motorTorque = 0;
            //wheelColliders[3].motorTorque = 0;
        }
    }

    //同步车轮模型坐标与角度
    private void UpdateMeshesPositions()
    {
        Vector3 pos;
        Quaternion quat;
        Vector3 pos1;
        Quaternion quat1;
        wheelColliders[0].GetWorldPose(out pos, out quat);
        wheelColliders[2].GetWorldPose(out pos1, out quat1);

        // handle.eulerAngles = new Vector3(0, quat.eulerAngles.y, 0);
        //handle.rotation = quat;
        wheelMeshs[0].transform.position = pos + new Vector3(0.5f, 0, 0);
        wheelMeshs[0].transform.rotation = quat;
        wheelMeshs[1].transform.position = pos1 + new Vector3(0.5f, 0, 0);
        wheelMeshs[1].transform.rotation = quat1;
    }
}