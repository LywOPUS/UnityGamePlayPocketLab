using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMousePointMove : MonoBehaviour {

    private Vector3 target;
    private bool isOver = true;
    public float speed;
    public Transform GO;
	
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo = new RaycastHit();
            if (Physics.Raycast(ray,out hitInfo))
            {
                if (hitInfo.collider.name =="Ground")
                {
                    target = hitInfo.point;
                    target.y += 0.5f;
                    isOver = false;
                }
            }
           
        }

	}
    private void MoveToMouse(Vector3 _target,Transform GO)
    {
        if (!isOver)
        {
            Vector3 offset = target - GO.transform.position;
            GO.position += offset.normalized * speed * Time.deltaTime;
            if (Vector3.Distance(target,transform.position)<0.5F)
            {
                isOver = true;
                transform.position = target;
            }
        }
    }
}
