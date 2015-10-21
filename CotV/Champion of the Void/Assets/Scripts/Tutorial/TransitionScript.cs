using UnityEngine;
using System.Collections;

public class TransitionScript : MonoBehaviour {

	GameObject p1;
	GameObject p2;
	GameObject spotlight;
	public float transitionCharge;

	// Use this for initialization
	void Start () {
		spotlight = GameObject.Find ("Spotlight");
		transitionCharge = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (p1 == null || p2 == null) {
			p1 = GameObject.Find ("Player1");
			p2 = GameObject.Find ("Player2");
		}

		if (Mathf.Abs (p1.transform.position.x - gameObject.transform.position.x) < 3.0f && 
			Mathf.Abs (p2.transform.position.x - gameObject.transform.position.x) < 3.0f &&
		    Mathf.Abs (p1.transform.position.z - gameObject.transform.position.z) < 3.0f && 
		    Mathf.Abs (p2.transform.position.z - gameObject.transform.position.z) < 3.0f )
		{
			spotlight.GetComponent<Light> ().spotAngle += 1f;
			spotlight.GetComponent<Light> ().intensity += .5f;
			Vector3 temp = spotlight.transform.position;
			temp.y += .1f;
			spotlight.transform.position = temp;
			transitionCharge += Time.deltaTime;
		} else {
			transitionCharge = 0;
			spotlight.GetComponent<Light> ().spotAngle = 45.0f;
			spotlight.GetComponent<Light> ().intensity = 2.45f;
			Vector3 temp = new Vector3(-2.4f, 25.4f, 83.2f);
			spotlight.transform.position = temp;
		}

		if (transitionCharge > 1.8f) {
			Application.LoadLevel("TuesdayBuild");
		}
	}
}
