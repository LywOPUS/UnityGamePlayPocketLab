using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TimeEngine : MonoBehaviour
{
    [SerializeField] private float timeScale = 1.0f;
    [SerializeField] private float nowTime = 0;
    [SerializeField] private bool rewindEffect = false;



    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            nowTime -= Time.deltaTime * timeScale;
        }
        else
        {
            nowTime += Time.deltaTime * timeScale;
        }
        UpdateTime();
    }

    private void UpdateTime()
    {
        TimeManager.Instance.Time = nowTime;
    }
}
