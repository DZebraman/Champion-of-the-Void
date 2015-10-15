using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {

	public GameObject healthPanel;
	private GameObject healthPanelP1;
	private GameObject healthPanelP2;

	public bool p1;
	public bool p2;

	public float healthTotal;
	public float healthCurrent;
	public float healthPercent;

	private float scaleX;
	private float scaleY;
	
	// Use this for initialization
	void Start () {
		healthTotal = 10.0f;
		healthCurrent = 10.0f;
		healthPercent = 1.0f;
		healthPanelP1 = GameObject.Find ("P1 Health");
		healthPanelP2 = GameObject.Find ("P2 Health");
		if (p1) {
			scaleX = healthPanelP1.GetComponentInChildren<Image> ().rectTransform.localScale.x;
			scaleY = healthPanelP1.GetComponentInChildren<Image> ().rectTransform.localScale.y;
		}
		else if (p2) {
			scaleX = healthPanelP2.GetComponentInChildren<Image> ().rectTransform.localScale.x;
			scaleY = healthPanelP2.GetComponentInChildren<Image> ().rectTransform.localScale.y;
		}
		else {
			scaleX = healthPanel.GetComponentInChildren<Image> ().rectTransform.localScale.x;
			scaleY = healthPanel.GetComponentInChildren<Image> ().rectTransform.localScale.y;
		}
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
		if (p1) {
			healthPanelP1.transform.localScale = new Vector3 (healthPercent * scaleX, scaleY, 1.0f);
		} 
		else if (p2) {
			healthPanelP2.transform.localScale = new Vector3 (healthPercent * scaleX, scaleY, 1.0f);
		}
		else{
			healthPanel.transform.localScale = new Vector3 (healthPercent * scaleX, scaleY, 1.0f);
		}
		CheckHealth();
	}

	// Linearly interpolates the health bars color between red and green, and sets that color
	void CheckHealth(){
		Color temp = Color.Lerp (Color.red, Color.green, healthPercent);
		if (p1) {
			healthPanelP1.GetComponent<Image> ().color = new Color (temp.r, temp.g, temp.b, 0.5f);
		}
		else if (p2) {
			healthPanelP2.GetComponent<Image> ().color = new Color (temp.r, temp.g, temp.b, 0.5f);
		}
		else {
			healthPanel.GetComponent<Image> ().color = new Color (temp.r, temp.g, temp.b, 0.5f);
		}

	}
}