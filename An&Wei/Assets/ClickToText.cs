//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class ClickToText : MonoBehaviour {
//	#region public
//	public ClickToText instence;
//	public RaycastHit thisHit;
//	#endregion
//	// Use this for initialization
//	void Start () {
//		
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		if (Input.GetMouseButtonDown(0)) {
//			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out thisHit)) {
//				if (thisHit.collider.GetInstanceID = this.GetComponent<BoxCollider>().GetInstanceID ) {
//					Debug.Log ("text is hit");
//				}
//			}
//		}
//	}
//}
