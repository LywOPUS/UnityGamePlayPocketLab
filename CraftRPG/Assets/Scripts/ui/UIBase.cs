using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBase : MonoBehaviour
{
    public void SetSprite(Image rImage, string rIcon)
    {
        rImage.sprite = Resources.Load<GameObject>("assetsbundles/icon/" + rIcon).GetComponent<Image>().sprite;
    }

    public EventTriggerListener SetEventTrigger(GameObject rObj)
    {
        var eventListener = rObj.GetComponent<EventTriggerListener>();
        if (eventListener == null)
        {
            eventListener = rObj.AddComponent<EventTriggerListener>();
        }
        return eventListener;
    }
}