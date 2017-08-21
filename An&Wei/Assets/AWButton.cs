using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class AWButton : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IPointerEnterHandler,IPointerExitHandler {
	Image showImage;

	public Sprite normalSprite;
	public Sprite hightlightSprite;
	public Color normalColor;
	public Color hightlightColor;
	public int id;
	public static AWButton A;
	// Use this for initialization
	void Start () {
		
		A = this;
		showImage = GetComponent<Image> ();
		if (normalSprite!=null) {
			showImage.sprite = normalSprite;
		}
		showImage.color = normalColor;
	}

	// Update is called once per frame

	void Update () {

	}
	#region IPointerDownHandler implementation

	public void OnPointerDown (PointerEventData eventData)
	{

	}

	#endregion

	#region IPointerUpHandler implementation

	public void OnPointerUp (PointerEventData eventData)
	{
		OnClick ();
	}

	#endregion

	#region IPointerEnterHandler implementation

	public void OnPointerEnter (PointerEventData eventData)
	{
		if (hightlightSprite!=null) {
			showImage.sprite = hightlightSprite;
		}
		showImage.color = hightlightColor;
	}

	#endregion

	#region IPointerExitHandler implementation

	public void OnPointerExit (PointerEventData eventData)
	{
		if (normalSprite!=null) {
			showImage.sprite = normalSprite;
		}
		showImage.color = normalColor;
	}

	#endregion

	void OnClick(){
		switch (id) {
		case 0:
			MoveToBox ();
			break;
		case 1:
			SelTexture ();
			break;
		case 2:
			Application.Quit ();
			break;


		default:
			break;
		}

	

	}

	void MoveToBox(){
		GameManager.instance.selImage.sprite = showImage.sprite;
	}

	void SelTexture(){
		GameManager.instance.selTexture =  Resources.Load(GameManager.instance.selImage.sprite.name) as Texture2D;
		if (GameManager.instance.selTexture!=null) {
			Debug.Log ("OK");
		}
	}

}
