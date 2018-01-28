using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float speed;
    private Rigidbody2D rb;
    private Animator anim;
    public string mode;
    public GameObject targetAnimal, possesedAnimal;
    private GameObject[] animals;
    public bool paused;
    public GameObject pauseUI;
    public Material highlightedMat, possessedMat, defaultMat, fishMat01, fishMat02, fishMat03, fishMat04, fishMat05, redFurMat;

    void Start()
    {
        paused = false;
        rb = GetComponent<Rigidbody2D>();
        animals = GameObject.FindGameObjectsWithTag("Animal");
        foreach (GameObject animal in animals)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), animal.GetComponent<Collider2D>());
        }
    }

    void FixedUpdate()
    {
        if (mode == "fairy")
        {
            FairyUpdate();
        }
    }

    void FairyUpdate()
    {
        if (paused)
        {
            pauseUI.SetActive(true);
        }
        else
        {
            pauseUI.SetActive(false);
        }
        if (Time.timeScale == 1 && pauseUI.activeSelf)
        {
            paused = false;
            pauseUI.SetActive(false);
        }
        if (!rb.isKinematic)
        {
            Camera.main.transform.position = transform.position - new Vector3(0, -2, 10);
            float moveHorizontal = Input.GetAxis("Horizontal");
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);

            rb.AddForce(movement * speed);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                paused = true;
                pauseUI.SetActive(true);
                Time.timeScale = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (targetAnimal != null)
            {
                if (targetAnimal.GetComponent<SquirrelControlScript>())
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
                            possesedAnimal.GetComponent<OwlControlScript>().possessed = false;
                        }
                        possesedAnimal = null;
                    }
                    possesedAnimal = targetAnimal;
                    possesedAnimal.GetComponent<SquirrelControlScript>().possessed = true;
                    Material[] newMat = possesedAnimal.transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().materials;
                    newMat[1] = possessedMat;
                    possesedAnimal.transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().materials = newMat;
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
                    possesedAnimal.GetComponent<OwlControlScript>().possessed = true;
                    possesedAnimal.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = possessedMat;
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
                    possesedAnimal.GetComponent<FishBehavior>().possessed = true;
                    possesedAnimal.transform.GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().material = possessedMat;
                    transform.position = new Vector2(0, 500);
                    rb.velocity = Vector2.zero;
                    rb.isKinematic = true;
                    possesedAnimal.GetComponent<Rigidbody2D>().gravityScale = 0;
                    possesedAnimal.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                }
                targetAnimal = null;
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (possesedAnimal != null)
            {
                if (possesedAnimal.GetComponent<SquirrelControlScript>())
                {
                    possesedAnimal.GetComponent<SquirrelControlScript>().possessed = false;
                    Material[] newMat = possesedAnimal.transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().materials;
                    newMat[1] = redFurMat;
                    possesedAnimal.transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().materials = newMat;
                    transform.position = possesedAnimal.transform.position - new Vector3(1, -1, 0);
                    rb.isKinematic = false;
                    rb.AddForce(new Vector2(100, 300));
                }
                if (possesedAnimal.GetComponent<OwlControlScript>())
                {
                    possesedAnimal.GetComponent<OwlControlScript>().possessed = false;
                    possesedAnimal.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = defaultMat;
                    transform.position = possesedAnimal.transform.position - new Vector3(1, -1, 0);
                    rb.isKinematic = false;
                    rb.AddForce(new Vector2(-50, 300));
					GameObject spawn = GameObject.FindGameObjectWithTag ("Spawn Point");
					if (spawn != null) {
						possesedAnimal.transform.position = spawn.transform.position;
					}
					possesedAnimal.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
                }
                if (possesedAnimal.GetComponent<FishBehavior>())
                {
                    //possesedAnimal.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                    possesedAnimal.GetComponent<Rigidbody2D>().gravityScale = 1;
                    possesedAnimal.GetComponent<FishBehavior>().possessed = false;
                    possesedAnimal.transform.GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().material = defaultMat;
                    transform.position = possesedAnimal.transform.position - new Vector3(1, -1, 0);
                    rb.isKinematic = false;
                    rb.AddForce(new Vector2(-50, 300));
                }
                possesedAnimal = null;
            }
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Rock")) {
            rb.velocity = Vector2.zero;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
//        if (collision.gameObject.name.Contains("Water"))
//        {
//            Physics2D.IgnoreCollision(GetComponents<CircleCollider2D>()[1], collision);
//            rb.AddForce(new Vector2(0, -(Mathf.Abs(transform.position.y) - 4f) * Physics2D.gravity.y));
//        }
		if (collision.gameObject.CompareTag("Water")) {
			GameObject spawn = GameObject.FindGameObjectWithTag ("Spawn Point");
			if (spawn != null) {
				transform.position = spawn.transform.position;
			}
		}
    }

	void OnTriggerEnter2d(Collider2D collider) {
		
	}

    public void Continue()
    {
        Time.timeScale = 1;
        Debug.Log("Continue");
        paused = false;
        pauseUI.SetActive(false);
    }

    public void Exit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Title Screen");
    }
}
