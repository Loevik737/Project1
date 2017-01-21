using UnityEngine;
using System.Collections;

public class AutoScroll : MonoBehaviour {

	public float camSpeed;
	public float rotationAmount;
	public bool orthographic;
	//set the target framerate to 200fps because I can ;)
	void Awake(){
		Application.targetFrameRate = 200;

	}
	// Use this for initialization
	void Start () {
		this.GetComponent<Rigidbody> ().AddForce (new Vector3 (camSpeed, 0, 0));
	}

	void Update(){
		if(Input.GetButtonUp ("RotateLeft")){
				this.transform.Rotate (new Vector3 (0, -rotationAmount, 0));

		}
		if(Input.GetButtonUp ("RotateRight")){
				this.transform.Rotate (new Vector3 (0, rotationAmount, 0));
		}
		if(Input.GetButtonUp ("View")){
			if (this.GetComponent<Camera> ().orthographic) {
				this.GetComponent<Camera> ().orthographic = false;
				this.transform.position = new  Vector3 (this.transform.position.x, this.transform.position.y, -10);
			} else {
				this.GetComponent<Camera> ().orthographic = true;
				this.transform.position = new  Vector3 (this.transform.position.x, this.transform.position.y, -3);
			}
		}
	}
}
