using System.Collections.Generic;
using UnityEngine;

public class GetMousePointMove : MonoBehaviour
{
    private bool isOver = true;
    public bool isRotate;
    public float speed;
    public Transform GO;
    private Vector3 target;
    public Queue<Vector3> qPoint = new Queue<Vector3>();

    private void MoveToMouse(Vector3 _target, Transform GO)
    {
        if (!isOver)
        {
            Vector3 offset = _target - GO.transform.position;
            GO.position += offset.normalized * speed * Time.deltaTime;
            if ((_target - GO.position).magnitude < 0.2f)
            {
                GO.position = _target;
                isOver = true;
            }
        }
    }

    /// 通过判断sine函数图像和cosine函数图像的旋转的方法

    #region RotateFunc

    private void DotRotate(Transform Go, Vector3 target)
    {
        Debug.DrawRay(Go.position, target);
        Vector3 direction = target - Go.position;
        float rotateAngle = Mathf.Acos(Vector3.Dot(Go.forward, direction.normalized)) * Mathf.Rad2Deg;
        float directionIAngle = Vector3.Dot(Go.right, direction.normalized);//included angle夹角

        if (Vector3.Dot(Go.forward, direction.normalized) < 0.999999f)
        {
            if (directionIAngle >= 0)
            {
                Debug.Log("朝右转了");
                // GO.Rotate(GO.up, rotateAngle * 6 * Time.deltaTime);
                Go.rotation *= Quaternion.Euler(0, rotateAngle * 6 * Time.deltaTime, 0);
                Debug.Log(rotateAngle.ToString());
            }
            else
            {
                Debug.LogError("朝左转了");
                //欧拉转
                //GO.Rotate(GO.up, -rotateAngle * 6 * Time.deltaTime);
                Go.rotation *= Quaternion.Euler(0, -rotateAngle * 6 * Time.deltaTime, 0);
                Debug.Log(rotateAngle.ToString());
            }
        }
    }

    #endregion RotateFunc

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo = new RaycastHit();
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.transform.tag == "Ground")
                {
                    target = hitInfo.point;
                    target.y = 1;
                    isOver = false;
                }
            }
        }

        MoveToMouse(qPoint.Peek(), GO);
        if (isOver == false)
        {
            DotRotate(GO, qPoint.Peek());
        }
    }
}