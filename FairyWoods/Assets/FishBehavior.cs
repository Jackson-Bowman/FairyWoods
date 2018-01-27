using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehavior : MonoBehaviour {

	public float speed;
	public int turnFrame;
	private float currentFrame;
	private Vector3 direction;
    public bool bobDir = true;
    public float high, low;
    public bool possessed;

	// Use this for initialization
	void Start () {
        possessed = false;
		currentFrame = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (bobDir) {
            transform.position = new Vector2(transform.position.x, Mathf.Lerp(low, high, currentFrame));
            currentFrame += 0.033f;
            if (currentFrame >= 1) {
                bobDir = false;
            }
        }
        else
        {
            transform.position = new Vector2(transform.position.x, Mathf.Lerp(low, high, currentFrame));
            currentFrame -= 0.033f;
            if (currentFrame <= 0)
            {
                bobDir = true;
            }
        }
        if (possessed) {
            Camera.main.transform.position = transform.position - new Vector3(0, 0, 10);
            if (Input.GetKey(KeyCode.A)) {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(-5, 0));
            }
            if (Input.GetKey(KeyCode.D))
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(5, 0));
            }
        }
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        if (possessed)
        {
            if (other.gameObject.name.Contains("Owl") && other.gameObject.GetComponent<OwlControlScript>())
            {
                FindObjectOfType<PlayerController>().targetAnimal = other.gameObject;
            }
            if (other.gameObject.name.Contains("Squirrel") && other.gameObject.GetComponent<SquirrelControlScript>() && other.gameObject != this.gameObject)
            {
                FindObjectOfType<PlayerController>().targetAnimal = other.gameObject;
            }
            if (other.gameObject.name.Contains("Fish") && other.gameObject.GetComponent<FishBehavior>())
            {
                FindObjectOfType<PlayerController>().targetAnimal = other.gameObject;
            }
        }
        if (other.gameObject.name.Contains("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().targetAnimal = gameObject;
        }
    }
}
