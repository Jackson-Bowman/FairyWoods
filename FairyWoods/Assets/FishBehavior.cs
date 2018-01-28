using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehavior : MonoBehaviour {

	public float speed;
	public int turnFrame;
	private Vector3 direction;
    public bool bobDir = true;
    public float high, low;
    public bool possessed;
	private int moveFrame;

	// Use this for initialization
	void Start () {
        possessed = false;
		direction = Vector3.left;
		moveFrame = 0;

	}
	
	// Update is called once per frame
	void Update () {
        if (possessed) {
            if (transform.position.y >= 2.4)
            {
                transform.position = new Vector2(transform.position.x, 2.3f);
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
            Camera.main.transform.position = transform.position - new Vector3(0, 0, 10);
            if (Input.GetKey(KeyCode.A)) {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(-5, 0));
            }
            if (Input.GetKey(KeyCode.D))
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(5, 0));
            }
            if (Input.GetKey(KeyCode.W))
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 5));
            }
            if (Input.GetKey(KeyCode.S))
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -5));
            }
        } else {
			if (moveFrame < turnFrame) {
				transform.Translate (direction * speed * Time.deltaTime);
				moveFrame++;
			} else {
				moveFrame = 0;
				direction = -direction;
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
            other.transform.GetChild(0).gameObject.GetComponent<PlayerController>().targetAnimal = gameObject;
            GetComponent<SkinnedMeshRenderer>().material = other.gameObject.GetComponent<PlayerController>().highlightedMat;
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            other.transform.GetChild(0).gameObject.GetComponent<PlayerController>().targetAnimal = null;
            GetComponent<SkinnedMeshRenderer>().material = other.gameObject.GetComponent<PlayerController>().defaultMat;
        }
    }
}
