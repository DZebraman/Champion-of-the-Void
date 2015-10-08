using UnityEngine;
using System.Collections;

public class Slingshot : MonoBehaviour {

	float lifeSpan = 3.5f;
	float slingSpan = 2.0f;

    public GameObject EnergyWave;
	public GameObject owner;
	float lerpSpeed = 4.0f;
	Vector3 vel;
	float maxVel = 0.5f;
    Vector3 temp;

	// Use this for initialization
	void Start () {
		temp = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {

		if (slingSpan > 0.0f) {
			temp = transform.position;
			transform.position = Vector3.Lerp (transform.position, owner.transform.position, lerpSpeed * Time.deltaTime);
			temp = transform.position - temp;

			slingSpan -= Time.deltaTime;
		}

		vel = temp.normalized * maxVel;

		if (slingSpan <= 0.0f) {
			transform.position += vel;
		}

        lifeSpan -= Time.deltaTime;

		if (lifeSpan <= 0.0f) {
            Vector3 temp2 = transform.position;
            temp2.y = 0.01f;
            GameObject.Instantiate(EnergyWave, temp2, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
