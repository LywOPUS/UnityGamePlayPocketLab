using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Com_Item : MonoBehaviour
{
    public Transform Transform_Darg;

    private void Start()
    {
        Transform_Darg = gameObject.transform.Find("Transform_darg");
        var dargItem = Transform_Darg.gameObject.AddComponent<DargItem>();
    }
}