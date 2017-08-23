using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Com_Item : UIBase
{
    public Transform Tran_Darg;
    public Image Img_Icon;

    public configBagData data;

    private void Start()
    {
        Tran_Darg = gameObject.transform.Find("Transform_darg");
        Img_Icon = this.gameObject.transform.Find("Transform_darg/Item_Icon").GetComponent<Image>();
        Debug.Log(Img_Icon.gameObject.ToString());
        Tran_Darg.gameObject.AddComponent<DargItem>();
        SetIcon();
    }

    private void SetIcon()
    {
        SetSprite(Img_Icon, data.spriteName);
    }
}