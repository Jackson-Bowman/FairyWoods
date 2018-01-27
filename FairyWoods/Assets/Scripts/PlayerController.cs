using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed;
	private Rigidbody2D rb;
	private Animator anim;
	public string mode;

	void Start() {
		rb = GetComponent<Rigidbody2D> ();

		//anim = transform.GetComponentInChildren<Animator> ();
		//anim = GetComponent<Animator>();
		//anim.enabled = false;
		//mode = "fairy";
	}

	void FixedUpdate() {
		if (mode == "fairy") {
			FairyUpdate ();
		}
	}

	void FairyUpdate() {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, 0.0f);

		rb.AddForce (movement * speed);

	}
}
