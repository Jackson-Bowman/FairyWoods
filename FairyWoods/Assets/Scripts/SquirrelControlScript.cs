using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquirrelControlScript : MonoBehaviour {

    GameObject currentTree;
    public bool possessed;
	private Animator anim;
	public float speed;
	private Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        currentTree = null;
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
        if (possessed)
        {
            Camera.main.transform.position = transform.position - new Vector3(0, 0, 10);
            FindObjectOfType<PlayerController>().possesedAnimal = gameObject;
            if (currentTree == null)
            {
				rb.gravityScale = 1;
                if (Input.GetKey(KeyCode.A))
                {
                    transform.Translate(new Vector3(-speed, 0, 0));

                }
                if (Input.GetKey(KeyCode.D))
                {
                    transform.Translate(new Vector3(speed, 0, 0));
                }
                if (Input.GetKeyDown(KeyCode.W))
                {
					rb.AddForce(new Vector2(0, 400));
                }
				if (Input.GetAxis ("Horizontal") > 0) {
					
					transform.Find ("Squirrel_Rig").transform.eulerAngles = new Vector3 (-90, -90, 0);
					anim.Play ("Run_Cycle");
				} else if (Input.GetAxis ("Horizontal") < 0) {

					transform.Find ("Squirrel_Rig").transform.eulerAngles = new Vector3 (-90, 90, 0);
					anim.Play ("Run_Cycle");
				} else {
					anim.Play ("Wait");
				}
            }
            else
            {
				rb.gravityScale = 0;
                if (Input.GetKey(KeyCode.W) && transform.position.y <= 6)
                {
                    transform.Translate(new Vector3(0, 0.1f, 0));
                }
                if (Input.GetKey(KeyCode.S) && transform.position.y >= -3)
                {
                    transform.Translate(new Vector3(0, -0.1f, 0));
                }
                transform.position = new Vector2(currentTree.transform.position.x, transform.position.y);
            }
        }
    }

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.CompareTag("Ramp")) {
			if (Input.GetAxis("Horizontal") > 0) {
				transform.eulerAngles = new Vector3(0, 0, collision.transform.eulerAngles.z);
			} else if (Input.GetAxis("Horizontal") < 0) {
				transform.eulerAngles = new Vector3(0, 0, -collision.transform.eulerAngles.z);
			}
		}
	}

	void OnCollisionExit2D(Collision2D collision) {
		if (collision.gameObject.CompareTag ("Ramp")) {
			transform.eulerAngles = Vector3.zero;
		}
	}

    public void OnTriggerStay2D(Collider2D other)
    {
        if (possessed)
        {
            if (other.gameObject.name.Contains("Tree")) {
                Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(), other);
            }
            if (other.gameObject.name.Contains("Tree") && Input.GetKeyDown(KeyCode.Space))
            {
                if (currentTree == null)
                {
                    currentTree = other.gameObject;
                }
                else
                {
                    currentTree = null;
                }
            }
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
        if (other.gameObject.name.Contains("Player")) {
            other.gameObject.GetComponent<PlayerController>().targetAnimal = gameObject;
        }

//		void OnCollisionEnter2D(Collision2D) {
//			
//		}
    }
}
