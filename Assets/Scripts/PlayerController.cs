using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float speed = 15.0f;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.LeftArrow)) {
			transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
		} else if (Input.GetKey(KeyCode.RightArrow)) {
			transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
		}
	}
}
