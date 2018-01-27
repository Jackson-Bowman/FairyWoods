using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlControlScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Camera.main.transform.position = transform.position - new Vector3(0, 0, 10);
        if (Input.GetKeyDown(KeyCode.W)) {
           GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 500));
           //GetComponent<Rigidbody2D>().gravityScale = 0.001f;
        }
	}
}
