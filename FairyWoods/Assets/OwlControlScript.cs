using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlControlScript : MonoBehaviour {
    public float jumpPeak;
    public bool gliding;
    public bool possessed;
    public float cameraLerp;
    Animator anim;
	// Use this for initialization
	void Start () {
        cameraLerp = 0;
        jumpPeak = transform.position.y + 3.7f;
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (possessed)
        {
            Camera.main.orthographicSize = Mathf.Lerp(5, 10, cameraLerp);
            FindObjectOfType<PlayerController>().possesedAnimal = gameObject;
            Camera.main.transform.position = transform.position - new Vector3(0, 0, 10);
            if (gliding)
            {
                if (cameraLerp < 1)
                {
                    cameraLerp += 0.1f;
                }
                anim.Play("Glide_Cycle");
            }
            else
            {
                if (cameraLerp > 0)
                {
                    cameraLerp -= 0.1f;
                }
                anim.Play("Idle");
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 500));
                gliding = true;
            }
            if (Input.GetKey(KeyCode.A) && gliding)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 0));
            }
            if (Input.GetKey(KeyCode.D) && gliding)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 0));
            }
            if (transform.position.y >= jumpPeak)
            {
                GetComponent<Rigidbody2D>().gravityScale = 0.01f;
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -1));
                gliding = true;
            }
        }
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        gliding = false;
        if (possessed)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().gravityScale = 1f;
            jumpPeak = Mathf.Round(transform.position.y) + 3.7f;
        }
    }
    void OnCollisionStay2D (Collision2D col)
    {
        if (possessed)
        {
            GetComponent<Rigidbody2D>().gravityScale = 1f;
            jumpPeak = Mathf.Round(transform.position.y) + 3.7f;
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (possessed)
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
            GetComponent<SkinnedMeshRenderer>().material = other.gameObject.GetComponent<PlayerController>().highlightedMat;
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().targetAnimal = null;
            GetComponent<SkinnedMeshRenderer>().material = other.gameObject.GetComponent<PlayerController>().defaultMat;
        }
    }
}
