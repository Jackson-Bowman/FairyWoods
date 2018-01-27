using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehavior : MonoBehaviour {

	public float speed;
	public int turnFrame;
	private int currentFrame;
	private Vector3 direction;

	// Use this for initialization
	void Start () {
		currentFrame = 0;
		direction = Vector3.right;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (direction * speed * Time.deltaTime);
		if (currentFrame < turnFrame) {
			currentFrame++;
		} else {
			currentFrame = 0;
			direction = -direction;
		}
	}
}
