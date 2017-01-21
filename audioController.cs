using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioController : MonoBehaviour {

	private Dictionary<string,AudioClip> clips;
	private AudioSource playerSounds;
	public GameObject player;

	void Start ()
	{ 
		clips = new Dictionary<string,AudioClip> ();
		clips["hitGround"] = Resources.Load ("Audio/hittGround") as AudioClip;
		clips["death"] = Resources.Load ("Audio/8-bit/Hit_01") as AudioClip;
		clips["checkPoint"] = Resources.Load ("Audio/R&C/check") as AudioClip;
		clips["collectLight"] = Resources.Load ("Audio/8-bit/Collect_Point_00") as AudioClip;
		playerSounds = player.GetComponent<AudioSource> ();
}

	public void playAudio(string name){
		playerSounds.PlayOneShot (clips[name], 0.8f);
	}
}		
