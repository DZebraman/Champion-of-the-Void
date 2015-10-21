using UnityEngine;
using System.Collections;

public class TutorialTextScript : MonoBehaviour {

	private string text;
	float timer;
	int progress;

	// Use this for initialization
	void Start () {
		text = "";
		timer = 5.0f;
		progress = 0;
	}
	
	// Update is called once per frame
	void Update () {
		switch (progress) {
		case 0:
			text = "Welcome to Champion of the Void!";
			break;
		case 1:
			text = "To skip this tutorial proceed\nto the temple entrance to the north.";
			break;
		case 2:
			text = "The blue player can be moved with WASD,\nand the green player with IJKL";
			break;
		default:
			break;
		}

		timer -= Time.deltaTime;

		if (timer <= 0.0f) {
			progress++;
			timer = 5.0f;
		}

		GetComponent<TextMesh> ().text = text;
	}
}
