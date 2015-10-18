using UnityEngine;
using System.Collections;

public class LightBossScript : MonoBehaviour
{
	public GameObject[] wanderPath;
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

    Vector3 toP1;
    Vector3 toP2;
                
    float angle1;
    float angle2;

    // Use this for initialization
    public void Init()
    {
        spot = transform.FindChild("Spotlight").GetComponent<Light>();
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");

		p1Health = player1.GetComponent<HealthUI> ();
		p2Health = player2.GetComponent<HealthUI> ();
		wander = false;

		controller = GetComponent<CharacterController> ();
		fixateTarget = transform.position;

		//RandomSearch ();
    }

    // Update is called once per frame
    void Update()
    {
        if(player1 == null)
        {
            player1 = GameObject.Find("Player1");
        }
        else if(player2 == null)
        {
            player2 = GameObject.Find("Player2");
        }
        else
        {
            Fixate();
        }
		if (wander) {
			FollowPath ();
		}

		TurnToFace ();
		ProcessMovement ();
		Damage ();
        /*
		if (Vector3.Distance (transform.position, player1.transform.position) < 10.0f) {
			lastKnownLocation = player1.transform.position;
		}
		if (Vector3.Distance (transform.position, player2.transform.position) < 10.0f) {
			lastKnownLocation = player2.transform.position;
		}
        */
//		if (Vector3.Distance (transform.position, lastKnownLocation) < 2) {
//			RandomSearch();
//		}
//    }
//
//	private void RandomSearch(){
//		lastKnownLocation.x = Random.Range (-100, 100);
//		lastKnownLocation.y =  0.5f;
//		lastKnownLocation.z = Random.Range (-100, 100);
	}

	private void FollowPath()
	{
		print ("following path");
		if (Vector3.Distance (transform.position, wanderPath[lastWanderIndex].transform.position) < 5) {
			lastWanderIndex++;
			lastWanderIndex = lastWanderIndex % wanderPath.Length;
		}
		fixateTarget = wanderPath[lastWanderIndex].transform.position;
		fixateTarget.y = transform.position.y;
	}

	private void Damage(){
		RaycastHit hit;

        toP1 = player1.transform.position - transform.position;
        toP2 = player2.transform.position - transform.position;

        angle1 = Vector3.Angle(transform.forward, toP1);
        angle2 = Vector3.Angle(transform.forward, toP2);

		if ( angle1< spot.spotAngle / 2) {
			Ray ray = new Ray (transform.position, -transform.position + player1.transform.position);
			if(Physics.Raycast(ray,out hit)){
				if(hit.transform.gameObject == player1){
                    Debug.DrawLine(transform.position, player1.transform.position);
					p1Health.TakeDamage(Time.deltaTime / (Vector3.Distance(transform.position,player1.transform.position) / 15));
				}
			}
		} if (angle2 < spot.spotAngle / 2) {
			Ray ray = new Ray (transform.position, -transform.position + player2.transform.position);
			if(Physics.Raycast(ray,out hit)){
				if(hit.transform.gameObject == player2){
                    Debug.DrawLine(transform.position, player2.transform.position);
                    p2Health.TakeDamage(Time.deltaTime / (Vector3.Distance(transform.position, player2.transform.position) / 15));
				}
			}
		}
	}

	private void Fixate2(){
		float p1Dist = Vector3.Distance(transform.position, player1.transform.position);
		float p2Dist = Vector3.Distance(transform.position, player2.transform.position);
		
		GameObject fixatePlayer = null;

		if (!wander) {
			for (int i = 0; i < wanderPath.Length; i++) {
				GameObject obj = wanderPath [i];
				if (Vector3.Distance (transform.position, obj.transform.position) < Vector3.Distance (wanderPath [lastWanderIndex].transform.position, transform.position)) {
					fixatePlayer = obj;
					lastWanderIndex = i;
				}
			}
			wander = true;
		}

		if ((angle1 < spot.spotAngle / 1.5) && angle2 < spot.spotAngle / 1.5  || (p1Dist < 10 || p2Dist < 10)) {
			if (p1Dist < p2Dist) {
				fixatePlayer = (player1.activeSelf)? player1:null;
			} else {
				fixatePlayer = (player2.activeSelf)? player2:null;
			}
		} else if (angle1 < spot.spotAngle / 2) {
			fixatePlayer = (player1.activeSelf)? player1:null;
		} else if (angle2 < spot.spotAngle / 2)  {
			fixatePlayer = (player2.activeSelf)? player2:null;
		}

	}


    private void Fixate()
    {
        float p1Dist = Vector3.Distance(transform.position, player1.transform.position);
        float p2Dist = Vector3.Distance(transform.position, player2.transform.position);

		GameObject fixatePlayer = null;



		if (player1.activeSelf && p1Dist < 10) {
			if (p1Dist < p2Dist && player2.activeSelf) {
				wander = false;
				fixatePlayer = player1;
			}
			else{
				wander = false;
				fixatePlayer = player1;
			}
		}else if(player2.activeSelf && p2Dist < 10){
			if(p2Dist < p1Dist && player1.activeSelf){
				wander = false;
				fixatePlayer = player2;
			}
			else{
				wander = false;
				fixatePlayer = player2;
			}
		}

		if ((angle1 < spot.spotAngle / 1.5) && angle2 < spot.spotAngle / 1.5 && (player2.activeSelf && player1.activeSelf)) {
			if (p1Dist < p2Dist) {
				fixatePlayer = player1;
			} else {
				fixatePlayer = player2;
			}
		} else if (angle1 < spot.spotAngle / 2 && player2.activeSelf) {
			//Debug.Log ("P1");
			fixatePlayer = player1;
		} else if (angle2 < spot.spotAngle / 2 && player2.activeSelf) {
			//Debug.Log ("P2");
			fixatePlayer = player2;
		} else {
			if (!wander) {
				for (int i = 0; i < wanderPath.Length; i++) {
					GameObject obj = wanderPath [i];
					if (Vector3.Distance (transform.position, obj.transform.position) < Vector3.Distance (wanderPath [lastWanderIndex].transform.position, transform.position)) {
						fixatePlayer = obj;
						lastWanderIndex = i;
					}
				}
				wander = true;
			}
//			if (wander) {
//
//			}
		}
		if (fixatePlayer == player1 && !player1.activeSelf) {
			fixatePlayer = wanderPath[lastWanderIndex];
			wander = true;
		}
		if (fixatePlayer == player2 && !player2.activeSelf) {
			fixatePlayer = wanderPath[lastWanderIndex];
			wander = true;
		}

		if (fixatePlayer != null) {
			fixateTarget = fixatePlayer.transform.position;
			fixateTarget.y = gameObject.transform.position.y;
		}

		         

//        if (p1Dist < 100 || p2Dist < 100 && (player2.activeSelf && player1.activeSelf))
//        {
//            Debug.Log("Close");
//            fixateTarget = (p1Dist < p2Dist) ? player1.transform.position : player2.transform.position;
//        }
//
//        else if ((angle1 < spot.spotAngle / 1.5) && angle2 < spot.spotAngle / 1.5 && (player2.activeSelf && player1.activeSelf))
//        {
//			if (p1Dist < p2Dist) {
//				fixateTarget = player1.transform.position;
//			} else {
//				fixateTarget = player2.transform.position;
//			}
//			lastKnownLocation = fixateTarget;
//		} else if (angle1 < spot.spotAngle / 2 && player2.activeSelf) {
//			//Debug.Log ("P1");
//			fixateTarget = player1.transform.position;
//			lastKnownLocation = fixateTarget;
//		} else if (angle2 < spot.spotAngle / 2 && player2.activeSelf) {
//			//Debug.Log ("P2");
//			fixateTarget = player2.transform.position;
//			lastKnownLocation = fixateTarget;
//        }
//        else {
//            fixateTarget = lastKnownLocation;
//        }
//        if (fixateTarget == player1.transform.position)
//            Debug.Log("Player1");
//        if (fixateTarget == player2.transform.position)
//            Debug.Log("Player2");
//        if (fixateTarget == lastKnownLocation)
//            Debug.Log("Last");
    }



	void ProcessMovement(){
		//vel *= 0.1f;
		accel = Vector3.zero;

		Vector3 dv = fixateTarget - transform.position;

		accel = vel - dv;
		accel.y = 0;
		//accel = accel.normalized * -maxSpeed;

		vel += accel;

		vel = vel.normalized * -maxSpeed;

		//transform.position += vel;
		controller.Move (vel);
	}

    private void TurnToFace()
    {
        //Vector3 targetVector = fixateTarget - transform.position;
		Quaternion tempRot = transform.rotation;

		transform.LookAt (fixateTarget);
		transform.rotation = Quaternion.Lerp (tempRot, transform.rotation, Time.deltaTime * 0.82f);
			//Quaternion.Lerp(transform.rotation,transform.LookAt(fixateTarget.transform.position),Time.deltaTime);
    }
}