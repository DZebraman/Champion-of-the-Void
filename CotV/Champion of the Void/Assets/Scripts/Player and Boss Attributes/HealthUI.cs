using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {

	public GameObject healthPanel;
	
	public float healthTotal;
	public float healthCurrent;
	public float healthPercent;
	
	// Use this for initialization
	void Start () {
		healthTotal = 10.0f;
		healthCurrent = 10.0f;
		healthPercent = 1.0f;
	}

	public void TakeDamage(float damage){
		//Debug.Log ("This code is flawless");
		healthCurrent -= damage;
	}

	// Update is called once per frame
	void Update () {
		//healthCurrent -= 0.1f; // Use this to test health declining
		if (healthCurrent <= 0) {
			//gameObject.GetComponent<MeshRenderer>().enabled = false;
			gameObject.SetActive(false);
		}

		// Get the percentage of health remaing and change the bar based on that percentage
		healthPercent = healthCurrent / healthTotal;
		healthPanel.transform.localScale = new Vector3 (healthPercent, 1.0f, 1.0f);
		CheckHealth();
	}

	// Linearly interpolates the health bars color between red and green, and sets that color
	void CheckHealth(){
		Color temp = Color.Lerp (Color.red, Color.green, healthPercent);
		healthPanel.GetComponent<Image> ().color = new Color (temp.r, temp.g, temp.b, 0.5f);
	}
}