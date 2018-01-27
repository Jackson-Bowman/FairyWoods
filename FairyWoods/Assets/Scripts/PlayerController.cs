using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed;
	private Rigidbody rb;
	private Animator anim;
	public string mode;
	private bool floating;
	private int floatingFrames;
	public float buoyancy;
	public int floatFrameMax;
	public float sink;
	public int floatFrameSwitch;

	void Start() {
		rb = GetComponent<Rigidbody> ();
		bool floating = false;
		floatingFrames = 0;
		//anim = transform.GetComponentInChildren<Animator> ();
		//anim = GetComponent<Animator>();
		//anim.enabled = false;
		//mode = "fairy";
	}

	void FixedUpdate() {
		if (mode == "fairy") {
			FairyUpdate ();
		}
		if (floating) {

//			if (floatingFrames <= floatFrameSwitch) {
//				rb.AddForce (Vector3.up * buoyancy* Time.deltaTime);
//			}
//			if (floatingFrames == floatFrameMax) {
//				floatingFrames = 0;
//			} else {
//				floatingFrames++;
//			}

			if (floatingFrames <= floatFrameSwitch) {
				transform.Translate (Vector3.down * buoyancy * Time.deltaTime);
			} else {
				transform.Translate (Vector3.up * buoyancy * Time.deltaTime);
			}
			if (floatingFrames == floatFrameMax) {
				floatingFrames = 0;
			} else {
				floatingFrames++;
			}

//			if (floatingFrames <= floatFrameSwitch) {
//				rb.AddForce (new Vector3 (0, sink, 0));
//			} else {
//				rb.AddForce (new Vector3 (0, buoyancy, 0));
//			}
//			if (floatingFrames == floatFrameMax) {
//				floatingFrames = 0;
//			} else {
//				floatingFrames++;
//			}
			
//			if (floatingFrames == 0) {
//				rb.AddForce (new Vector3 (0, buoyancy * Time.deltaTime, 0));
//			}
//			if (floatingFrames < floatFrameMax) {
//				floatingFrames++;
//			} else {
//				floatingFrames = 0;
//			}
		}
	}

	void FairyUpdate() {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, 0.0f);

		rb.AddForce (movement * speed);

	}
		
	void OnTriggerEnter(Collider other) {
		Debug.Log ("triggered");
		floating = true;
		rb.useGravity = false;
		rb.velocity = Vector3.zero;
		rb.isKinematic = true;
	}

	void OnTriggerExit(Collider other) {
		floating = false;
		floatingFrames = 0;
		rb.useGravity = true;
//		rb.isKinematic = true;
//		rb.useGravity = false;
//		rb.AddForce (new Vector3 (0, buoyancy, 0));
	}
}
