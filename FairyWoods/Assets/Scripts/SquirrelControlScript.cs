using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquirrelControlScript : MonoBehaviour {

    GameObject currentTree;
    public bool possessed;
	private Animator anim;
	public float speed;
	private Rigidbody2D rb;
	private bool jumping;
	private bool climbing;

	// Use this for initialization
	void Start () {
        currentTree = null;
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
		jumping = false;
		climbing = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (possessed)
        {
            Camera.main.transform.position = transform.position - new Vector3(0, 0, 10);
            FindObjectOfType<PlayerController>().possesedAnimal = gameObject;
			if (!climbing)
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
					if (!jumping) {
						rb.AddForce(new Vector2(0, 400));
						jumping = true;
					}

                }
				if (Input.GetAxis ("Horizontal") > 0) {
					
					transform.Find ("Squirrel_Rig").transform.eulerAngles = new Vector3 (-90, -90, 0);
					if (jumping) {
						anim.Play ("Jump");
					} else {
						anim.Play ("Run_Cycle");
					}
				} else if (Input.GetAxis ("Horizontal") < 0) {

					transform.Find ("Squirrel_Rig").transform.eulerAngles = new Vector3 (-90, 90, 0);
					if (jumping) {
						anim.Play ("Jump");
					} else {
						anim.Play ("Run_Cycle");
					}
				} else {
					anim.Play ("Wait");
				}
            }
            else
            {
				rb.gravityScale = 0;
                if (Input.GetKey(KeyCode.W) && transform.position.y <= 10)
                {
					transform.Find ("Squirrel_Rig").transform.eulerAngles = new Vector3 (-90, 0, -90);
					anim.Play ("Climb_Cycle");
                    transform.Translate(new Vector3(0, 0.1f, 0));
				}
                if (Input.GetKey(KeyCode.S) && transform.position.y >= 2)
                {
					transform.Find ("Squirrel_Rig").transform.eulerAngles = new Vector3 (-90, 0, -90);
					anim.Play ("Climb_Cycle");
                    transform.Translate(new Vector3(0, -0.1f, 0));
				} 
				if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S)){
					transform.Find ("Squirrel_Rig").transform.eulerAngles = new Vector3 (0, 180, 0);
					anim.Play ("Wait");

				}
                transform.position = new Vector2(currentTree.transform.position.x, transform.position.y);
				if (Input.GetKeyDown(KeyCode.Space)) {
					climbing = false;
                    rb.AddForce(new Vector2(100, 200));
				}
            }
		} else {
			anim.Play ("Idle");
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
		jumping = false;
	}

	void OnCollisionExit2D(Collision2D collision) {
		if (collision.gameObject.CompareTag ("Ramp")) {
			transform.eulerAngles = Vector3.zero;
		}
	}

    public void OnTriggerStay2D(Collider2D other)
	{
		if (possessed) {
//			if (other.gameObject.name.Contains ("Tree")) {
//				Physics2D.IgnoreCollision (GetComponent<Collider2D> (), other);
//			}
			if (other.gameObject.name.Contains ("Tree") && Input.GetKeyUp (KeyCode.Space)) {
				if (currentTree == null) {
					currentTree = other.gameObject;
					climbing = true;
					rb.velocity = Vector2.zero;
					anim.Play ("Wait");
					transform.Find ("Squirrel_Rig").transform.eulerAngles = new Vector3 (0, 180, 0);

				} else {
					currentTree = null;
				}
			}
			if (other.gameObject.name.Contains ("Owl") && other.gameObject.GetComponent<OwlControlScript> ()) {
				FindObjectOfType<PlayerController> ().targetAnimal = other.gameObject;
			}
			if (other.gameObject.name.Contains ("Squirrel") && other.gameObject.GetComponent<SquirrelControlScript> () && other.gameObject != this.gameObject) {
				FindObjectOfType<PlayerController> ().targetAnimal = other.gameObject;
			}
			if (other.gameObject.name.Contains ("Fish") && other.gameObject.GetComponent<FishBehavior> ()) {
				FindObjectOfType<PlayerController> ().targetAnimal = other.gameObject;
			}
		}
		if (other.gameObject.name.Contains ("Player")) {
			other.gameObject.GetComponent<PlayerController> ().targetAnimal = gameObject;
            Material[] newMat = transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().materials;
            newMat[1] = other.gameObject.GetComponent<PlayerController>().highlightedMat;
            transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().materials = newMat;
        }
	}
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("Player")) {
            other.gameObject.GetComponent<PlayerController>().targetAnimal = null;
            if (!possessed)
            {
                Material[] newMat = transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().materials;
                newMat[1] = other.gameObject.GetComponent<PlayerController>().redFurMat;
                transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().materials = newMat;
            }
        }
    }
}
