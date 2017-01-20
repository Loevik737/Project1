using UnityEngine;
using System.Collections;

public class collect : MonoBehaviour {
	private Animator collectAnimator;
	public GameObject player;
	private int lights;
	void Start(){
		collectAnimator = this.GetComponent<Animator> ();
	}
	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Player")
		{
			//this.gameObject.GetComponent<SphereCollider> ().enabled = false;
			player.GetComponent<PlayerAutoScroll> ().collectLight ();
			collectAnimator.SetTrigger ("collect");
		}
	}
	public void disable(){
		gameObject.SetActive (false);
	}
}
