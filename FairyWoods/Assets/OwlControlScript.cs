using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlControlScript : MonoBehaviour {
    public float jumpPeak;
    public bool gliding;
    public bool possesed;
	// Use this for initialization
	void Start () {
        jumpPeak = transform.position.y + 4.9f;
	}
	
	// Update is called once per frame
	void Update () {
        if (possesed)
        {
            FindObjectOfType<PlayerController>().possesedAnimal = gameObject;
            Camera.main.transform.position = transform.position - new Vector3(0, 0, 10);
            if (Input.GetKeyDown(KeyCode.W))
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 500));
            }
            if (Input.GetKey(KeyCode.A) && gliding)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(-10, 0));
            }
            if (Input.GetKey(KeyCode.D) && gliding)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(10, 0));
            }
            if (transform.position.y >= jumpPeak)
            {
                GetComponent<Rigidbody2D>().gravityScale = 0.01f;
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -1));
                gliding = true;
            }
        }
	}

    void OnCollisionEnter2D(Collision2D col) {
        if (possesed)
        {
            GetComponent<Rigidbody2D>().gravityScale = 1f;
            gliding = false;
            jumpPeak = Mathf.Round(transform.position.y) + 4.9f;
        }
    }
    void OnCollisionStay2D (Collision2D col)
    {
        if (possesed)
        {
            GetComponent<Rigidbody2D>().gravityScale = 1f;
            gliding = false;
            jumpPeak = Mathf.Round(transform.position.y) + 4.9f;
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (possesed)
        {
            if (other.gameObject.name.Contains("Owl") && other.gameObject.GetComponent<OwlControlScript>() && other.gameObject != this.gameObject)
            {
                FindObjectOfType<PlayerController>().targetAnimal = other.gameObject;
            }
            if (other.gameObject.name.Contains("Squirrel") && other.gameObject.GetComponent<SquirrelControlScript>())
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
