using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	private int robotsLeft = 2;
	private string sceneName;
	public static GameController instance = null;

	public Text robotsLeftText;

	// Use this for initialization
	void Start () {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
		SceneManager.activeSceneChanged += GetRobotsLeft;
	}

	void OnDestroy() {
		SceneManager.activeSceneChanged -= GetRobotsLeft;
	}

	void GetRobotsLeft(Scene previousScene, Scene newScene) {
		if (newScene.name.CompareTo("GameOver") != 0) {
			robotsLeftText = FindObjectOfType<Text>();
			robotsLeftText.text = "x " + robotsLeft;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Break () {
		robotsLeft -= 1;
		if (robotsLeft > 0) {
			robotsLeftText.text = "x "+ robotsLeft;
			Debug.Log(robotsLeft);
		}
		Invoke("RestartLevel", 3f);
	}

	public void LevelEnd () {
		robotsLeft += 1;
		robotsLeftText.text = "x "+ robotsLeft;
		Debug.Log(robotsLeft);
		Invoke("NextLevel", 3f);
	}

	private void RestartLevel () {
		sceneName = SceneManager.GetActiveScene().name;
		if (robotsLeft >= 0) {
			SceneManager.LoadScene(sceneName);
		} else {
			SceneManager.LoadScene("GameOver");
		}
	}

	private void NextLevel () {
		int sceneIndex = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(sceneIndex+1);
	}
}
