using UnityEngine;
using System.Collections;

public class MobileInput : MonoBehaviour {

	public GameObject player;
	private PlayerAutoScroll playerScrpt;

	void Start(){
		playerScrpt = player.GetComponent<PlayerAutoScroll> ();
	}
	void Update(){

		if (Input.touches.Length > 0) {
			playerScrpt.Jump(true);
		}
	}
}