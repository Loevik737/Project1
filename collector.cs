using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class collector : MonoBehaviour {
	List<GameObject> list;
	void Start () {
		list = new List<GameObject> ();
		foreach (Transform t in transform)
			list.Add (t.gameObject);
	}

	public void enableAll(){
		foreach (GameObject col in list){
			col.SetActive (true);
		}
	}
}
