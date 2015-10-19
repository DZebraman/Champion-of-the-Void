using UnityEngine;
using System.Collections;

public class PursuitWrangler : MonoBehaviour {

	public GameObject Pursuer;
	public GameObject target;

	private GameObject[] obstacles;

	private Vector3 vel,acc,dv;

	public float lerpSpeed;
	public float maxSpeed;

	// Use this for initialization
	void Start () {
		obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
		vel = Vector3.zero;
		acc = Vector3.zero;
	}

	void ProcessMovement(){

	}

	void Seek(){
		dv = transform.position - target.transform.position;
		dv = dv.normalized * acc.magnitude;
		acc = Vector3.Lerp(acc.normalized,dv,Time.deltaTime * lerpSpeed);
	}

	/*
	Vector3 AvoidObstacle (GameObject obst, float safeDistance)
	{ 
		dv = Vector3.zero;
		
		//vector from vehicle to center of obstacle
		Vector3 vecToCenter = obst.transform.position - transform.position;

		// distance should not be allowed to be zero or negative because 
		// later we will divide by it and do not want to divide by zero
		// or cause an inadvertent sign change.
		float dist = Mathf.Max(vecToCenter.magnitude - obRadius - radius, 0.1f);
		
		// if too far to worry about, out of here
		if (dist > safeDistance)
			return Vector3.zero;
		
		//if behind us, out of here
		if (Vector3.Dot (vecToCenter, transform.forward) < 0)
			return Vector3.zero;
		
		float rightDotVTC = Vector3.Dot (vecToCenter, transform.right);
		
		//if we can pass safely, out of here
		if (Mathf.Abs (rightDotVTC) > radius + obRadius)
			return Vector3.zero;
		
		//if we get this far, than we need to steer
		
		//obstacle is on right so we steer to left
		if (rightDotVTC > 0)
			dv = transform.right * -maxSpeed * safeDistance / dist;
		else
			//obstacle on left so we steer to right
			dv = transform.right * maxSpeed * safeDistance / dist;
		
		dv -= vel;    //calculate the steering force
		//dv.y = 0;		   // only steer in the x/z plane
		return dv;
	}
	*/
	// Update is called once per frame
	void Update () {
	
	}
}
