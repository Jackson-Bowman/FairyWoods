using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed;
	private Rigidbody2D rb;
	private Animator anim;
	public string mode;
    public GameObject targetAnimal, possesedAnimal;
	private GameObject[] animals;

	void Start() {
		rb = GetComponent<Rigidbody2D> ();
		animals = GameObject.FindGameObjectsWithTag ("Animal");
		foreach (GameObject animal in animals) {
			Physics2D.IgnoreCollision (GetComponent<Collider2D> (), animal.GetComponent<Collider2D>());
		}
	}

	void FixedUpdate() {
		if (mode == "fairy") {
			FairyUpdate ();
		}
	}

	void FairyUpdate() {
        if (!rb.isKinematic)
        {
            Camera.main.transform.position = transform.position - new Vector3(0, 0, 10);
            float moveHorizontal = Input.GetAxis("Horizontal");
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);

            rb.AddForce(movement * speed);
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            if (targetAnimal != null)
            {
                if (targetAnimal.GetComponent<SquirrelControlScript>())
                {
                    if (possesedAnimal != null) {
                        if (possesedAnimal.GetComponent<SquirrelControlScript>()) {
                            possesedAnimal.GetComponent<SquirrelControlScript>().possessed = false;
                        }
                        if (possesedAnimal.GetComponent<OwlControlScript>())
                        {
                            possesedAnimal.GetComponent<OwlControlScript>().possessed = false;
                        }
                        if (possesedAnimal.GetComponent<FishBehavior>())
                        {
                        }
                        possesedAnimal = null;
                    }
                    possesedAnimal = targetAnimal;
                    targetAnimal = null;
                    possesedAnimal.GetComponent<SquirrelControlScript>().possessed = true;
                    transform.position = new Vector2(0, 500);
                    rb.velocity = Vector2.zero;
                    rb.isKinematic = true;
                }
                if (targetAnimal.GetComponent<OwlControlScript>())
                {
                    if (possesedAnimal != null)
                    {
                        if (possesedAnimal.GetComponent<SquirrelControlScript>())
                        {
                            possesedAnimal.GetComponent<SquirrelControlScript>().possessed = false;
                        }
                        if (possesedAnimal.GetComponent<OwlControlScript>())
                        {
                            possesedAnimal.GetComponent<OwlControlScript>().possessed = false;
                        }
                        if (possesedAnimal.GetComponent<FishBehavior>())
                        {
                            possesedAnimal.GetComponent<FishBehavior>().possessed = false;
                        }
                        possesedAnimal = null;
                    }
                    possesedAnimal = targetAnimal;
                    targetAnimal = null;
                    possesedAnimal.GetComponent<OwlControlScript>().possessed = true;
                    transform.position = new Vector2(0, 500);
                    rb.velocity = Vector2.zero;
                    rb.isKinematic = true;
                }
                if (targetAnimal.GetComponent<FishBehavior>())
                {
                    if (possesedAnimal != null)
                    {
                        if (possesedAnimal.GetComponent<SquirrelControlScript>())
                        {
                            possesedAnimal.GetComponent<SquirrelControlScript>().possessed = false;
                        }
                        if (possesedAnimal.GetComponent<OwlControlScript>())
                        {
                            possesedAnimal.GetComponent<OwlControlScript>().possessed = false;
                        }
                        if (possesedAnimal.GetComponent<FishBehavior>())
                        {
                            possesedAnimal.GetComponent<FishBehavior>().possessed = false;
                        }
                        possesedAnimal = null;
                    }
                    possesedAnimal = targetAnimal;
                    targetAnimal = null;
                    possesedAnimal.GetComponent<FishBehavior>().possessed = true;
                    transform.position = new Vector2(0, 500);
                    rb.velocity = Vector2.zero;
                    rb.isKinematic = true;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (possesedAnimal != null) {
                if (possesedAnimal.GetComponent<SquirrelControlScript>())
                {
                    possesedAnimal.GetComponent<SquirrelControlScript>().possessed = false;
                    transform.position = possesedAnimal.transform.position - new Vector3(1, -1, 0);
                    possesedAnimal = null;
                    rb.isKinematic = false;
                    rb.AddForce(new Vector2(-50, 300));
                }
                if (possesedAnimal.GetComponent<OwlControlScript>())
                {
                    possesedAnimal.GetComponent<OwlControlScript>().possessed = false;
                    transform.position = possesedAnimal.transform.position - new Vector3(1, -1, 0);
                    possesedAnimal = null;
                    rb.isKinematic = false;
                    rb.AddForce(new Vector2(-50, 300));
                }
                if (possesedAnimal.GetComponent<FishBehavior>())
                {
                    possesedAnimal.GetComponent<FishBehavior>().possessed = false;
                    transform.position = possesedAnimal.transform.position - new Vector3(1, -1, 0);
                    possesedAnimal = null;
                    rb.isKinematic = false;
                    rb.AddForce(new Vector2(-50, 300));
                }
            }
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Physics2D.IgnoreCollision(GetComponents<CircleCollider2D>()[1], collision);
        if (collision.gameObject.name.Contains("Water"))
        {
            rb.AddForce(new Vector2(0, -(Mathf.Abs(transform.position.y) - 4f) * Physics2D.gravity.y));
        }
    }
}
