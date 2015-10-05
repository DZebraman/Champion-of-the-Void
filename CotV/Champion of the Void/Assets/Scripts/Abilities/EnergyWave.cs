using UnityEngine;
using System.Collections;

public class EnergyWave : MonoBehaviour {

    float lifeSpan = 1.4f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        lifeSpan -= Time.deltaTime;

        Vector3 temp = transform.localScale;
        temp.x *= 1.1f;
        temp.z *= 1.1f;
        transform.localScale = temp;

        if (lifeSpan < 0.0f)
        {
            Destroy(gameObject);
        }
	}
}
