using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PaperManager : MonoBehaviour {

	public GameObject[] paperArray = null;
	public GameObject[] playerPaperArray = null;

	void FixedUpdate () {
		testpaper ();
	}
	//检测方法
	void testpaper(){
		if (Input.GetKeyDown(KeyCode.Space)) {
			
			if (text (paperArray, playerPaperArray)) {
				nextEvent ();
			}else{
				los();
			}		
		}
	}

	// Update is called once per frame
	bool text(GameObject[] paperArray1,GameObject[] paperArray2){
		bool isRet = false;

		for(int i = 0; i <paperArray1.Length; i++) {
			if ((paperArray1 [i].GetComponent<MeshRenderer> ().material.mainTexture.name) != (paperArray2 [i].GetComponent<MeshRenderer> ().material.mainTexture.name)) {
				isRet  =false;
				break;
			}
			if (i==paperArray1.Length-1) {
				isRet  =  true;
			}

		}
	
		return isRet; 

	}//end text


	#region  difrent event
   void win(){
		Debug.Log ("true");
	}
	void los(){
		Debug.Log ("fals");
	}
	void nextEvent(){
		Debug.Log ("Next Level");
	}
	#endregion

}
