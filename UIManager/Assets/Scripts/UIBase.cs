using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    public EventTrigerLisner SetEventTrigger(GameObject rObj)
    {
        var eventListener = rObj.GetComponent<EventTrigerLisner>();
        if (eventListener == null)
        {
            eventListener = rObj.AddComponent<EventTrigerLisner>();
        }

        return eventListener;
    }
}