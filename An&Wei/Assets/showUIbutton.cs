using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class showUIbutton : MonoBehaviour,IPointerClickHandler {

	public static showUIbutton S;
	public bool isClick = false;
	public GameObject Panel;
	#region panel
	Image[] images ;
	#endregion

	void Awake(){
		S = this;
		//isClick =false;
		images = Panel.GetComponentsInChildren<Image> ();
	}

	void FixedUpdate () {
		if (isClick == true&&Panel.activeSelf!=true) {
			RotateBox.R.enabled = false;
			StartCoroutine (FadeIn ());
		
		}
		if (isClick == false&&Panel.activeSelf!=false) {
			RotateBox.R.enabled = true;
			StartCoroutine(FadeOut());

		
		}

	}

	#region IPointerClickHandler implementation

	public void OnPointerClick (PointerEventData eventData)
	{

		if (isClick == false) {
			isClick = true;
			Debug.Log ("true");
		}else
		if (isClick == true) {
			isClick = false;
			Debug.Log ("false");
		}
	}

	#endregion

	#region FadeIEnumerator
	public IEnumerator FadeOut(){
		float fade = images [0].color.a;
		while (true) {
			yield return null;
			fade -= Time.deltaTime * 4f;
			for (int i = 0; i < images.Length; i++) {
				images [i].color = new Color (images [i].color.r, images [i].color.g, images [i].color.b, fade);
			}
			if (fade<0) {
				this.Panel.SetActive (false);
				break;
			}
		}

	}

	public	IEnumerator FadeIn(){
		float fade = images [0].color.a;
		//gameObject.GetComponent<RectTransform> ().anchoredPosition = new Vector3 (-82, 0, 0);
		Panel.SetActive (true);
		while (true) {
			yield return null;
			fade += Time.deltaTime * 4f;
			for (int i = 0; i < images.Length; i++) {
				images [i].color = new Color (images [i].color.r, images [i].color.g, images [i].color.b, fade);
			}
			if (fade > 1) {
				break;
			}
		}
	}
		#endregion

}
