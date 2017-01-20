using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerAutoScroll : MonoBehaviour
{
	//setting accesslevel, type and name for variables I will use
	public GameObject camera;
	public GameObject stats;
	private GameObject player;
	private Rigidbody playerPhysics;
	private Animator playerAnimator;
	private Animator cameraAnimator;
	private BoxCollider playerCollider;
	private AudioSource playerSounds;
	public GameObject smoke;

	private Dictionary<string,AudioClip> clips;
	private float spawnPoint;
	private static float startPoint;
	private bool isAlive;
	private bool inAir;
	private float axcJump;
	public float playerSpeed;


	// initialization
	void Start ()
	{ 
		//giving the variables values
		player = this.gameObject;
		playerPhysics = player.GetComponent<Rigidbody> ();
		playerAnimator = player.GetComponent<Animator> ();
		cameraAnimator = camera.GetComponent<Animator> ();
		playerCollider = player.GetComponent<BoxCollider> ();
		playerSounds = player.GetComponent<AudioSource> ();


		clips = new Dictionary<string,AudioClip> ();
		clips["hitGround"] = Resources.Load ("Audio/hittGround") as AudioClip;
		clips["death"] = Resources.Load ("Audio/8-bit/Hit_01") as AudioClip;
		clips["checkPoint"] = Resources.Load ("Audio/R&C/check") as AudioClip;
		clips["collectLight"] = Resources.Load ("Audio/8-bit/Collect_Point_00") as AudioClip;
		inAir = true;
		spawnPoint = -5f;
		startPoint = -5f;
		Spawn ();

	}

	// Update is called once per frame
	void Update ()
	{
		
		if (isAlive) {
			//get key press
			Jump(false);

			if (Input.GetButton ("Restart")) {
				SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
			}
			if (Input.GetButtonDown ("Down")) {
				Down ();
			}

			if(Input.GetButtonDown("SlowMotion")){
				slowMotionTrigger();
			}
		}
	}

	private void slowMotionTrigger(){
		if (Time.timeScale != 1f) {
			Time.timeScale = 1f;
		} else {
			Time.timeScale = 0.7f;
		}
	}

	public void Jump(bool isTouch){
		if (isTouch  && !inAir && Mathf.Round (playerPhysics.velocity.y) >= 0) {
			playerPhysics.AddForce (new Vector3 (0, axcJump, 0));
			inAir = true;
			playerAnimator.SetTrigger ("jump");
			smoke.gameObject.GetComponent<ParticleSystem> ().Stop ();
		}
		else if(Input.GetButton ("Jump") && !inAir && Mathf.Round (playerPhysics.velocity.y) >= 0) {
		playerPhysics.AddForce (new Vector3 (0, axcJump, 0));
		inAir = true;
		playerAnimator.SetTrigger ("jump");
		smoke.gameObject.GetComponent<ParticleSystem> ().Stop ();
		}
	}

	private void Down(){
		playerPhysics.AddForce (new Vector3 (0, -axcJump, 0));
	}


	//sets the x location where the player spawns to point
	public void setSpawnPoint(float point){
		spawnPoint = point;
		playerSounds.PlayOneShot (clips ["checkPoint"], 0.8f);
	}


	public void collectLight(){
		playerSounds.PlayOneShot (clips ["collectLight"], 0.8f);
		stats.GetComponent<Stats> ().updateCollectedLights ();
	}


	//do stuff when I spawn, its set to public so i can access it in the death animation
	public void Spawn (){
		
		player.transform.position = new Vector3 (spawnPoint, 5, 0);
		camera.transform.position = new Vector3 (spawnPoint, 4, -10);	
		playerCollider.enabled = true;
		playerPhysics.velocity = Vector3.zero;
		playerPhysics.angularVelocity = Vector3.zero;
		//give the ball the speed corrensponging to the camera
		playerPhysics.AddForce(new Vector3(playerSpeed,0,0));
		playerAnimator.SetTrigger ("spawn");
		isAlive = true;
		if (spawnPoint == startPoint) {
			stats.GetComponent<Stats> ().resetTime ();
		}
	}

	//function that is called when I die
	private void diedFunction(){
		stats.GetComponent<Stats> ().updateDeaths ();
		isAlive = false;
		playerSounds.PlayOneShot (clips ["death"], 0.8f);
		playerPhysics.velocity = Vector3.zero;
		playerPhysics.AddForce (new Vector3 (0, 20, 0));
		playerCollider.enabled = false;
		playerAnimator.SetTrigger ("died");
		//cameraAnimator.SetTrigger ("died");
	}

	private void checkCollision(Collision col){
		if (col.contacts [0].normal.x < 0) {
			diedFunction ();
		};
	}
		

	//do stuff when you collide with an spesiffic object(by tag)
	void OnCollisionEnter (Collision col)
	{

		if(col.gameObject.tag == "platform")
		{
			checkCollision (col);
			smoke.gameObject.GetComponent<ParticleSystem> ().Play ();
			playerSounds.PlayOneShot (clips ["hitGround"],1f);
			inAir = false;
			axcJump = 40f;

		}

		if(col.gameObject.tag == "jumpPad")
		{
			checkCollision (col);
			inAir = false;
			axcJump = 50f;
		}

		if (col.gameObject.tag == "oneHitEnemy") {
			
			diedFunction ();
		} 
			
	}
}