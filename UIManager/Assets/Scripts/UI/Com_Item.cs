using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Com_Item : UIBase
{
    public Transform Transform_Darg;
    public Image Img_Icon;

    public configBagData data;

    private void Start()
    {
        Transform_Darg = gameObject.transform.Find("Transform_darg");
        var dargItem = Transform_Darg.gameObject.AddComponent<DargItem>();
    }

    private void SetIcon()
    {
    }
}