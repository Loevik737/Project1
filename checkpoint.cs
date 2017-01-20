using UnityEngine;
using System.Collections;

public class checkpoint : MonoBehaviour {
	private GameObject player;
	void Start(){
		player = GameObject.Find("playerObj");
	}
	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Player")
		{
			this.gameObject.GetComponent<CapsuleCollider> ().enabled = false;
			player.GetComponent<PlayerAutoScroll> ().setSpawnPoint (this.gameObject.transform.position.x);
			this.gameObject.transform.GetChild (0).GetComponent<Renderer> ().material.color = new Color (0,1,0);

		}
	}
}
