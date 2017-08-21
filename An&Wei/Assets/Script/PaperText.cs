using UnityEngine;
using System.Collections;

public class PaperText : MonoBehaviour {
	public MeshRenderer mrA;
	public MeshRenderer mrB;

	public static PaperText instance;
	void Awake(){
		instance = this;
	}

}
