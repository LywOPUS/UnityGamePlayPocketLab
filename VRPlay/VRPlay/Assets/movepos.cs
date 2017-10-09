using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movepos : MonoBehaviour {


	public	float speed = 10;
	public float HorizontalSpeed= 10;
	void Update(){
		move ();
		
	}
	void move(){
		if (Input.GetKey(KeyCode.LeftShift)) {
			speed++;
		} else {
			speed = 10;
		}
		var right =	Input.GetAxis ("Horizontal");
		var front =	Input.GetAxis ("Vertical");
		this.gameObject.transform.Translate (Vector3.right * right * HorizontalSpeed * Time.deltaTime,Space.Self);
		this.gameObject.transform.Translate (Vector3.forward * front * speed * Time.deltaTime,Space.Self);
		if (Input.GetKey(KeyCode.E)) {
			this.gameObject.transform.Translate (Vector3.up * speed * Time.deltaTime, Space.World);
		}
		if (Input.GetKey(KeyCode.C)&&this.transform.position.y>0) {
			this.gameObject.transform.Translate (Vector3.down * speed * Time.deltaTime, Space.World);

		}

	}
}
