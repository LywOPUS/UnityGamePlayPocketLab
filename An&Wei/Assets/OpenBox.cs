using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OpenBox : MonoBehaviour
{
	#region private

	Animation[] animations;
	Quaternion CourrentAngle;
	Quaternion TargetAngle;


	#endregion


	#region public

	public float fRightAngel = 0;

	public GameObject box;
	public Texture[] Meth;
	public MeshRenderer[] MethOpen;
	public float RotateSpeed = 0.3f;
	public int RotateCheekCount = 0;

	#endregion

	#region bool

	bool isClick = false;
	bool isRet;

	#endregion



	void Start ()
	{


		this.animations = box.GetComponentsInChildren <Animation> ();
		box.GetComponentsInChildren<Animation> ();
		StopPlayAnimations ();

	}



	// Update is called once per frame
	void Update ()
	{
		isClick = false;
		if (Input.GetKeyDown (KeyCode.Space) && isClick == false) {
			
			EqualsTexture (this.Meth, this.MethOpen, out this.isRet);
			if (isRet == true) {
				StartCoroutine (OpenBoxAnimations());
				showUIbutton.S.enabled = false;
				AWButton.A.enabled = false;
				GameManager.instance.enabled = false;
			
			} else if (isRet == false) {
				Debug.Log ("testFlase");
			}
			isClick = true;                             
		}



	}
	//end Update









	//停止播放开箱动画
	void StopPlayAnimations ()
	{
		for (int i = 0; i < animations.Length; i++) {
			animations [i].Stop ();
		}
	}

	//开箱动画
	IEnumerator OpenBoxAnimations ()
	{
		for (int i = 0; i < animations.Length; i++) {
			if (animations [i].isPlaying == false) {
				RotateBox.R.AxisXangle = RotateBox.R.AxisYangle = RotateBox.R.AxisZangel = 0;

				yield return new WaitForSeconds (0.5f);
				animations [i].Play ("RotPlan");
			}
		}
	}


	//比较纹理

	#region panduan

	void EqualsTexture (Texture[] meth, MeshRenderer[] methOpen, out bool isRet)
	{
		isRet = false; 	
		int count = 0;

		for (int i = 0; i < meth.Length; i++) {
			

			if (meth [i].name != methOpen [i].material.mainTexture.name) {
				isRet = false;
				count++;
				Debug.Log ("foreath mesh is error");
				Debug.Log (i);
				//break;
			}

			Debug.Log (meth [i].name + " " + methOpen [i].material.mainTexture.name);
		

		}
		if (count < 1) {
			isRet = true;
		}
		Debug.Log ("count" + count);
		Debug.Log (isRet.ToString ());
		
		
	}

	#endregion





}
