using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Camera cam;
    public GameObject box_prefeb;

    // Use this for initialization
    private void Start()
    {
        cam = this.GetComponent<Camera>();
    }

    private void OnClickGround()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        // We need to actually hit an object
        RaycastHit hitt = new RaycastHit();
        Physics.Raycast(ray, out hitt, 100);
        Debug.DrawLine(cam.transform.position, ray.direction, Color.red);
        if (hitt.transform != null && hitt.transform.name == "Ground")
        {
            Vector3 p = new Vector3(hitt.point.x, 5, hitt.point.z);
            Instantiate(box_prefeb, p, Quaternion.Euler(0, 0, 0));
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnClickGround();
        }
    }
}