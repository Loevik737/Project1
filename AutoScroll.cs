using UnityEngine;
using System.Collections;

public class AutoScroll : MonoBehaviour {

	public float camSpeed;
	public float rotation;
	public bool orthographic;
	//set the target framerate to 144 fps because I can ;)
	void Awake(){
		//QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 200;

	}
	// Use this for initialization
	void Start () {
		
		if (camSpeed == 0f) {
			camSpeed = 100f;
		}
		this.GetComponent<Rigidbody> ().AddForce (new Vector3 (camSpeed, 0, 0));
	}

	void Update(){
		if(Input.GetButtonUp ("RotateLeft")){
				this.transform.Rotate (new Vector3 (0, -rotation, 0));

		}
		if(Input.GetButtonUp ("RotateRight")){
				this.transform.Rotate (new Vector3 (0, rotation, 0));
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
