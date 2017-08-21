using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerModePlay : MonoBehaviour {
	#region private
	Animation thisPlayAnimation;
	RaycastHit beerHit;
	#endregion
	// Use this for initialization
	void Awake () {
		thisPlayAnimation =	this.gameObject.GetComponent<Animation> ();
	}
	void Start(){
		thisPlayAnimation.Stop ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out beerHit)) {
				if (beerHit.collider.tag == "AnimalMode") {
					Debug.Log ("AnimalHit is true");

					this.thisPlayAnimation.Play ("smothup");

				} else {
					Debug.Log ("AnimalHit is flas");
				}
			}
		}
	}

	void StopDeerModePlay(){
		
	}
}
