using UnityEngine;
using System.Collections;

class PathNode{
	public PathNode next;
	public Vector3 position;
	public PathNode(){
		next = null;
		position = Vector3.zero;
	}
}

public class LightBossScript : MonoBehaviour
{
	public GameObject[] wanderPath;

	private PathNode[] wanderPath2;
	private PathNode wanderTarget;

	private GameObject[] Pursuers;
	public int numPursuers;

	private int lastWanderIndex;
	private bool wander;

    private Light spot;
    public Vector3 fixateTarget;

    private GameObject player1;
    private GameObject player2;

	private HealthUI p1Health;
	private HealthUI p2Health;

	private CharacterController controller;

	private Vector3 vel, accel;
	public float maxSpeed;
	public float turnSpeed;

	GameObject fixatePlayerOld;
	public GameObject fixatePlayer;

    Vector3 toP1;
    Vector3 toP2;
                
    float angle1;
    float angle2;

    float spotAngleInit;
    float spotRangeInit;

	bool player1Raycast;
	bool player2Raycast;

	public float pursuerOffset;

    // Use this for initialization
    public void Init()
    {
        spot = transform.FindChild("Spotlight").GetComponent<Light>();
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");

		p1Health = player1.GetComponent<HealthUI> ();
		p2Health = player2.GetComponent<HealthUI> ();
		wander = false;

		wanderPath2 = new PathNode[wanderPath.Length];

		for (int i = 0; i < wanderPath2.Length; i++) {
			wanderPath2[i] = new PathNode();
			wanderPath2[i].position = wanderPath[i].transform.position;
		}

		for (int i = 0; i < wanderPath2.Length; i++) {
			wanderPath2[i].next = (i+1 == wanderPath2.Length) ? wanderPath2[0]:wanderPath2[i+1];
		}

		Pursuers = new GameObject[numPursuers];

		Vector3 offset = new Vector3(pursuerOffset,0,0);
		for(int i = 0; i < numPursuers; i++){
			Pursuers[i] = (GameObject)Instantiate(Resources.Load("Prefabs/Pursuer"),transform.position+offset,Quaternion.identity);
			PursuitWrangler wrangler = Pursuers[i].GetComponent<PursuitWrangler>();
            wrangler.target = (i < numPursuers/2)?player1:player2;
            wrangler.player1 = player1;
            wrangler.player2 = player2;
            offset = (Quaternion.Euler(0,(360/numPursuers) * i,0) * offset) + new Vector3(Random.Range(0,10),0,Random.Range(0,10));
		}

		controller = GetComponent<CharacterController> ();
		fixateTarget = transform.position;

		wanderTarget = wanderPath2[6];

        spotAngleInit = spot.spotAngle;
        spotRangeInit = spot.range;

		//RandomSearch ();
    }

    // Update is called once per frame
    void LateUpdate()
    {

		Debug.DrawLine(this.transform.position,wanderTarget.position);
		RayCheck();
        Fixate();
		TurnToFace ();
		ProcessMovement ();
		Damage ();
        if (fixatePlayer == player1 || fixatePlayer == player2)
            Debug.Log("Following Player");

        spot.spotAngle = Mathf.Lerp(spot.spotAngle, spotAngleInit, 0.5f*Time.deltaTime);
        spot.range = Mathf.Lerp(spot.range, spotRangeInit, Time.deltaTime);
    }

	private void FollowPath()
	{


		//Finds unSqrt distance between this and target
		//if less, goes to next position in the linked list;
		Vector3 tempVec = transform.position - wanderTarget.position;
		float tempDist = Mathf.Abs(tempVec.sqrMagnitude);
		if(tempDist < 10){
			wanderTarget = wanderTarget.next;
		}

		fixateTarget = wanderTarget.position;
		fixateTarget.y = transform.position.y;
	}

	private void Damage(){
		RaycastHit hit;

//		if(player1Raycast){
//			p1Health.TakeDamage(Time.deltaTime / (Vector3.Distance(transform.position,player1.transform.position) / 15));
//		}
//		if(player2Raycast){
//			p2Health.TakeDamage(Time.deltaTime / (Vector3.Distance(transform.position, player2.transform.position) / 15));
//		}


		if ( angle1< spot.spotAngle / 2 && Vector3.Distance(player1.transform.position, transform.position) < spot.range) {
			Ray ray = new Ray (transform.position, -transform.position + player1.transform.position);
			if(Physics.Raycast(ray,out hit)){
				if(hit.transform.gameObject == player1){
                    Debug.DrawLine(transform.position, player1.transform.position);
					p1Health.TakeDamage(Time.deltaTime / (Vector3.Distance(transform.position,player1.transform.position) / 5));
				}
			}
		}if (angle2 < spot.spotAngle / 2 && Vector3.Distance(player2.transform.position, transform.position) < spot.range) {
			Ray ray = new Ray (transform.position, -transform.position + player2.transform.position);
			if(Physics.Raycast(ray,out hit)){
				if(hit.transform.gameObject == player2){
					Debug.DrawLine(transform.position, player2.transform.position);
					p2Health.TakeDamage(Time.deltaTime / (Vector3.Distance(transform.position,player2.transform.position) / 5));
				}
			}
		}


	}

	private void RayCheck(){
		RaycastHit hit;

		toP1 = player1.transform.position - transform.position;
		toP2 = player2.transform.position - transform.position;
		
		angle1 = Vector3.Angle(transform.forward, toP1);
		angle2 = Vector3.Angle(transform.forward, toP2);

		if ( angle1< spot.spotAngle / 2) {
			Ray ray = new Ray (transform.position, -transform.position + player1.transform.position);
			if(Physics.Raycast(ray,out hit,40)){
				if(hit.transform.gameObject == player1){
					Debug.DrawLine(transform.position, player1.transform.position);
					player1Raycast = true;
				}
			}
		}else{
			player1Raycast = false;
		}if (angle2 < spot.spotAngle / 2) {
			Ray ray = new Ray (transform.position, -transform.position + player2.transform.position);
			if(Physics.Raycast(ray,out hit,40)){
				if(hit.transform.gameObject == player2){
					Debug.DrawLine(transform.position, player2.transform.position);
					player2Raycast = true;
				}
			}
		}else{
			player2Raycast = false;
		}
	}

    private void RangeAngleMod(float p1Dist,float p2Dist)
    {
        if (p1Dist < 20 || p2Dist < 20)
        {
            spot.spotAngle = Mathf.Lerp(spot.spotAngle, spotAngleInit, 5 *Time.deltaTime);
            spot.range = Mathf.Lerp(spot.range,spotRangeInit,Time.deltaTime);
        }
        else
        {
            Debug.Log(spot.spotAngle);
            spot.spotAngle = Mathf.Lerp(spot.spotAngle, 2, 5* Time.deltaTime);
            spot.range = Mathf.Lerp(spot.range, 1000, Time.deltaTime);
        }
    }

	private void Fixate(){

		//Took the old code and organized it.
		//I'm pretty sure the old stuff would have worked, but we had too may combined if blocks
		//Everything is way cleaner and easier to troubleshoot

		float p1Dist = Vector3.Distance(transform.position, player1.transform.position);
		float p2Dist = Vector3.Distance(transform.position, player2.transform.position);

		
		

		FollowPath();

		if ((player1Raycast || player2Raycast)  || (p1Dist < 10 || p2Dist < 10) || fixatePlayer != null) {
			if (p1Dist < p2Dist) {
				fixatePlayer = (player1.activeSelf)? player1:null;
			} else {
				fixatePlayer = (player2.activeSelf)? player2:null;
			}
            RangeAngleMod(p1Dist,p2Dist);
		} else if (angle1 < spot.spotAngle / 2 && player1Raycast && player2Raycast) {
			fixatePlayer = (player1.activeSelf)? player1:null;
            RangeAngleMod(p1Dist,p2Dist);
        } else if (angle2 < spot.spotAngle / 2 && player1Raycast && player2Raycast)  {
			fixatePlayer = (player2.activeSelf)? player2:null;
            RangeAngleMod(p1Dist, p2Dist);
        }

        
		//Fixateplayer will always override the path following, but it always defaults to path
		fixateTarget = (fixatePlayer == null)?fixateTarget:fixatePlayer.transform.position;

		if(fixatePlayer != fixatePlayerOld && fixatePlayer == null){
			float dist = 10000;
			for(int i = 0; i < wanderPath2.Length; i++){
				if(Vector3.Dot(transform.forward,-transform.position+wanderPath2[i].position) > 0){
					Vector3 tempVec = transform.position - wanderTarget.position;
					float tempDist = Mathf.Abs(tempVec.sqrMagnitude);
					
					if(tempDist < dist){
						dist = tempDist;
						wanderTarget = wanderPath2[i];
					}
				}
			}
		}
        fixatePlayerOld = fixatePlayer;
        fixatePlayer = null;
    }

	void ProcessMovement(){
		vel *= 0.95f;
		vel += transform.forward * 2 * Time.deltaTime;
		vel = Vector3.ClampMagnitude(vel,maxSpeed);

		//transform.position += vel;
		controller.Move (vel);
	}

    private void TurnToFace()
    {
        //Vector3 targetVector = fixateTarget - transform.position;
		Quaternion tempRot = transform.rotation;

		transform.LookAt (fixateTarget);
        if (Vector3.Distance(transform.position, fixateTarget) < 10)
        {
            Debug.Log("Butts");
            transform.rotation = Quaternion.Lerp(tempRot, transform.rotation, Time.deltaTime * turnSpeed * 4);
        }
        else
            transform.rotation = Quaternion.Lerp(tempRot, transform.rotation, Time.deltaTime * turnSpeed);
        //Quaternion.Lerp(transform.rotation,transform.LookAt(fixateTarget.transform.position),Time.deltaTime);
    }
}