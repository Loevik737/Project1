using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class Stats : MonoBehaviour {
	private string text;
	private int deaths;
	private int lights;
	private float time;
	private float score;
	private float currentScore;
	private string currentLevel;
	void Start(){
		deaths = 0;
		lights = 0;
		score = 0;
		currentScore = 0;
		currentLevel = Application.loadedLevelName.ToString ();
		if (PlayerPrefs.GetFloat (currentLevel) == null || PlayerPrefs.GetFloat (currentLevel) == Mathf.Infinity) {
			PlayerPrefs.SetFloat (currentLevel, 0f);
		}

	}
	void Update(){
		time += Time.deltaTime;
	}

	public void resetTime(){
		time = 0;
	}
	public float getCurrentScore(){
		return ((10000*(lights+1))-time) / (deaths+1);
	}

	public void saveScore(){
		score = getCurrentScore ();
		resetTime ();
		if (PlayerPrefs.GetFloat (currentLevel) < score) {
			PlayerPrefs.SetFloat (currentLevel, score);
			PlayerPrefs.Save();
		}
	
	}
	public void updateCollectedLights(){
		this.lights += 1;
	}

	public void updateDeaths(){
		this.deaths += 1;
	}

	public void clear (){
		deaths = 0;
		lights = 0;
		score = 0;
	}

	void OnGUI()
	{
		int w = Screen.width, h = Screen.height;
		GUIStyle style = new GUIStyle();
		Rect rect = new Rect(0, 0, w, h * 2 / 100);
		style.alignment = TextAnchor.UpperRight;
		style.fontSize = h * 2 / 100;
		style.normal.textColor = new Color (1f, 1f, 1f, 1.0f);

		text = string.Format("Lights:{0} Deaths:{1} Time: {2}s Score:{3} Highscore: {4}", lights, deaths, time.ToString("F2"), Mathf.Round(getCurrentScore()).ToString(), Mathf.Round(PlayerPrefs.GetFloat(currentLevel)).ToString());
		GUI.Label(rect, text, style);
	}
}
