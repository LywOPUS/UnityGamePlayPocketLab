using UnityEngine;

public class GetMousePointMove : MonoBehaviour
{
    public Transform GO;
    public float speed;
    private bool isOver = true;
    private Vector3 target;
    public bool isRotate;

    private void MoveToMouse(Vector3 _target, Transform GO)
    {
        if (!isOver)
        {
            Vector3 offset = _target - GO.transform.position;
            GO.position += offset.normalized * speed * Time.deltaTime;
            //if (Vector3.Distance(_target, GO.transform.position) < 0.2F)
            //{
            //    transform.position = _target;
            //    isOver = true;
            //}
            if ((_target - GO.position).magnitude < 0.2f)
            {
                transform.position = _target;
                isOver = true;
            }
        }
    }

    #region 旋转的正确示范和错误示范

    private void NoSineRotate()
    {
        Debug.DrawRay(GO.position, target);
        Vector3 direction = target - GO.position;
        float rotateAngle = Mathf.Acos(Vector3.Dot(GO.forward, direction.normalized)) * Mathf.Rad2Deg;
        float directionIAngle = Vector3.Dot(GO.right, direction.normalized);//included angle夹角

        if (Vector3.Dot(GO.forward, direction.normalized) < 0.999999f)
        {
            if (directionIAngle >= 0)
            {
                Debug.Log("朝右转了");
                // GO.Rotate(GO.up, rotateAngle * 6 * Time.deltaTime);
                GO.localRotation = Quaternion.Euler(0, rotateAngle * 6 * Time.deltaTime, 0);//四元数转向
                Debug.Log(rotateAngle.ToString());
                Debug.Log(GO.localEulerAngles.ToString());//打印物体本身的欧拉角
            }
            else
            {
                Debug.LogError("朝左转了");
                //GO.Rotate(GO.up, -rotateAngle * 6 * Time.deltaTime);
                GO.localRotation = Quaternion.Euler(0, -rotateAngle * 6 * Time.deltaTime, 0);//四元数转向
                Debug.Log(rotateAngle.ToString());

                Debug.Log(GO.localEulerAngles.ToString());
            }
        }
    }

    /// <summary>
    /// 在不获取sine值的情况下判定旋转朝向的方法
    /// </summary>
    /// <param name="_target">目标点</param>
    /// <param name="_GO">要旋转的物体</param>
    private void NoSineRotate_Error(Vector3 _target, Transform _GO)
    {
        Debug.DrawRay(_GO.position, _target);
        Vector3 direction = _target - _GO.position;
        //这里有一个明显的错误，也不是很明显，direction没有标准化
        float rotateAngle = Mathf.Acos(Vector3.Dot(_GO.forward, direction)) * Mathf.Rad2Deg;
        float directionIAngle = Vector3.Dot(_GO.right, direction.normalized);

        if (Vector3.Dot(_GO.forward, direction.normalized) < 0.999999f)
        {
            Debug.Log("旋转了");
            if (directionIAngle >= 0)
            {
                Debug.Log("朝右转了");
                //_GO.Rotate(_GO.up, rotateAngle * 6 * Time.deltaTime);
                _GO.rotation = Quaternion.AngleAxis(rotateAngle, Vector3.up);
                Debug.Log(rotateAngle.ToString());

                Debug.Log(_GO.localEulerAngles.ToString());//打印物体本身的欧拉角
            }
            else
            {
                // _GO.Rotate(_GO.up, -rotateAngle * 6 * Time.deltaTime);
                _GO.rotation = Quaternion.AngleAxis(rotateAngle, Vector3.up);
                Debug.Log(rotateAngle.ToString());

                Debug.Log(_GO.localEulerAngles.ToString());
            }
        }
    }

    #endregion 旋转的正确示范和错误示范

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

        MoveToMouse(target, GO);
        if (isOver == false)
        {
            NoSineRotate();
        }
    }
}