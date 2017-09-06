using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerModePlay : MonoBehaviour
{
    #region private

    private Animation thisPlayAnimation;
    private RaycastHit beerHit;

    #endregion private

    // Use this for initialization
    private void Awake()
    {
        thisPlayAnimation = this.gameObject.GetComponent<Animation>();
    }

    private void Start()
    {
        thisPlayAnimation.Stop();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out beerHit))
            {
                if (beerHit.collider.tag == "AnimalMode")
                {
                    Debug.Log("AnimalHit is true");

                    this.thisPlayAnimation.Play("smothup");
                }
                else
                {
                    Debug.Log("AnimalHit is flas");
                }
            }
        }
    }

    private void StopDeerModePlay()
    {
    }
}