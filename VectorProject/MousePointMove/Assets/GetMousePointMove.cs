using UnityEngine;

public class GetMousePointMove : MonoBehaviour
{
    public Transform GO;
    public float speed;
    private bool isOver = true;
    private Vector3 target;

    /// <summary>
    /// 移动物体到目标点位置
    /// </summary>
    /// <param name="_target">目标点</param>
    /// <param name="GO">要移动的物体</param>
    private void MoveToMouse(Vector3 _target, Transform GO)
    {
        if (!isOver)
        {
            Vector3 offset = _target - GO.transform.position;
            GO.position += offset.normalized * speed * Time.deltaTime;
            if (Vector3.Distance(_target, transform.position) < 0.5F)
            {
                isOver = true;
                transform.position = _target;
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo = new RaycastHit();
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.name == "Plane")
                {
                    Debug.Log("isHit");
                    target = hitInfo.point;
                    target.y += 0.5f;
                    isOver = false;
                }
            }
        }

        MoveToMouse(target, GO);
    }
}