using UnityEngine;
using System.Collections;

public class End : MonoBehaviour {
	public GameObject player;
	public GameObject stats;
	public GameObject checkPoint;
	public GameObject lights;

	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Player")
		{
			player.GetComponent<PlayerAutoScroll> ().setSpawnPoint (-5f);
			stats.GetComponent<Stats> ().saveScore ();
			stats.GetComponent<Stats> ().clear ();
			checkPoint.GetComponent<CapsuleCollider> ().enabled = true;
			checkPoint.gameObject.transform.GetChild (0).GetComponent<Renderer> ().material.color = new Color (1,0,0);
			lights.GetComponent<collector> ().enableAll ();
			player.GetComponent<PlayerAutoScroll> ().Spawn ();
		}
	}
}
