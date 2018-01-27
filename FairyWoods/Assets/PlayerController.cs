using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed;
	private Rigidbody rb;
	public string mode;

	void Start() {
		rb = this.GetComponent<Rigidbody> ();
		//mode = "fairy";
	}

	void Update() {
		switch (mode) {
		case "squirrel":
			SquirrelUpdate ();
			break;
		case "fish":
			FishUpdate ();
			break;
		case "owl":
			OwlUpdate ();
			break;
		} 
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

	void SquirrelUpdate() {
		if (Input.GetKey(KeyCode.A)) {
			transform.Translate (Vector3.left * speed * Time.deltaTime);
		} if (Input.GetKey(KeyCode.D)) {
			transform.Translate (Vector3.right * speed * Time.deltaTime);
		}
	}

	void FishUpdate() {
		
	}

	void OwlUpdate() {
		
	}

}
