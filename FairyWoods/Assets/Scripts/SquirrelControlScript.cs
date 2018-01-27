using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquirrelControlScript : MonoBehaviour {

    GameObject currentTree;
	// Use this for initialization
	void Start () {
        currentTree = null;
	}
	
	// Update is called once per frame
	void Update () {
        if (currentTree == null)
        {
            GetComponent<Rigidbody2D>().gravityScale = 1;
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(new Vector3(-0.1f, 0, 0));
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(new Vector3(0.1f, 0, 0));
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 400));
            }
        }
        else {
            GetComponent<Rigidbody2D>().gravityScale = 0;
            if (Input.GetKey(KeyCode.W) && transform.position.y <= 6)
            {
                transform.Translate(new Vector3(0, 0.1f, 0));
            }
            if (Input.GetKey(KeyCode.S) && transform.position.y >= -3)
            {
                transform.Translate(new Vector3(0, -0.1f, 0));
            }
            transform.position = new Vector2(currentTree.transform.position.x - 0.5f, transform.position.y);
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("Tree") && Input.GetKeyDown(KeyCode.Space)) {
            if (currentTree == null)
            {
                currentTree = other.gameObject;
            }
            else {
                currentTree = null;
            }
        }
    }
}
