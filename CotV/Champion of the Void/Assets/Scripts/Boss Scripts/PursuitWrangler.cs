using UnityEngine;
using System.Collections;

public class PursuitWrangler : MonoBehaviour {

	public GameObject Pursuer;
	public GameObject target;
    public GameObject player1, player2;

	private GameObject[] obstacles;

	private Vector3 vel,acc,dv;

	public float lerpSpeed;
	public float maxSpeed;
	public float seekWeight;

	private CharacterController control;

	private LightBossScript lightBoss;

	private Light spot;
    private Color lightOriginalColor;

	// Use this for initialization
	void Start () {
		obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
		lightBoss =  GameObject.Find("Bossman").GetComponent<LightBossScript>();
        
		spot = transform.FindChild("Spotlight").GetComponent<Light>();

        lightOriginalColor = spot.color;

        //vel = ogPos - transform.position;
        //vel = vel.normalized * maxSpeed/2;
        acc = Vector3.zero;
		vel = Vector3.zero;
		control = GetComponent<CharacterController>();

	}

	void ProcessMovement(){
		vel *= 0.95f;
		vel += transform.forward * 2 * Time.deltaTime;
		vel *= Mathf.Clamp(Vector3.Distance(transform.position,target.transform.position)/10,0.4f,1);
		vel = Vector3.ClampMagnitude(vel,maxSpeed);
	}

	void Seek(){
		//transform.forward = vel.normalized;
		Quaternion tempRot = transform.rotation;
		
		transform.LookAt (target.transform);
		transform.rotation = Quaternion.Lerp (tempRot, transform.rotation, Time.deltaTime * lerpSpeed);
	}

	void RayCheck(){
		RaycastHit hit;
		Vector3 toP1 = target.transform.position - transform.position;
		
		float angle1 = Vector3.Angle(transform.forward, toP1);

		if ( angle1< spot.spotAngle / 2 && Vector3.Distance(target.transform.position,transform.position) < 10) {
			Ray ray = new Ray (transform.position, -transform.position + target.transform.position);
			if(Physics.Raycast(ray,out hit)){
				if(hit.transform.gameObject == target){
					Debug.DrawLine(transform.position, target.transform.position);
                    spot.color = new Color(0.25f,0.1f,0.8f,0.5f);
                    lightBoss.fixatePlayer = target;
				}
			}
		}
        else
            spot.color = lightOriginalColor;
    }


	// Update is called once per frame
	void Update () {
        if (!target.activeSelf)
        {
            target = (target == player1) ? player2 : player1;
        }
		Seek ();
		RayCheck();
		ProcessMovement();
		//Debug.Log(vel);
		control.Move(vel);
		//transform.position += vel;
	}
}
