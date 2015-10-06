using UnityEngine;
using System.Collections;

public class LightBossScript : MonoBehaviour
{
    private Light spot;
    private Vector3 fixatePlayer;
	private Vector3 lastKnownLocation;
    private GameObject player1;
    private GameObject player2;

	private HealthUI p1Health;
	private HealthUI p2Health;

	private CharacterController controller;

	private Vector3 vel, accel;
	public float maxSpeed;


    // Use this for initialization
    public void Init()
    {
        spot = transform.FindChild("Spotlight").GetComponent<Light>();
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");

		p1Health = player1.GetComponent<HealthUI> ();
		p2Health = player2.GetComponent<HealthUI> ();

		controller = GetComponent<CharacterController> ();

		RandomSearch ();
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

		TurnToFace ();
		ProcessMovement ();
		Damage ();
		if (Vector3.Distance (transform.position, lastKnownLocation) < 2) {
			RandomSearch();
		}
    }

	private void RandomSearch(){
		lastKnownLocation.x = Random.Range (-100, 100);
		lastKnownLocation.y =  0.5f;
		lastKnownLocation.z = Random.Range (-100, 100);
	}

	private void Damage(){
		Quaternion lookAtp1 = Quaternion.LookRotation (player1.transform.position);
		Quaternion lookAtp2 = Quaternion.LookRotation (player2.transform.position);
		RaycastHit hit;

		if (Quaternion.Angle (transform.rotation, lookAtp1) < spot.spotAngle / 2) {
			Ray ray = new Ray (transform.position, -transform.position + player1.transform.position);
			if(Physics.Raycast(ray,out hit)){
				if(hit.transform.gameObject == player1){
					p1Health.TakeDamage(Time.deltaTime);
				}
			}
		} if (Quaternion.Angle (transform.rotation, lookAtp2) < spot.spotAngle / 2) {
			Ray ray = new Ray (transform.position, -transform.position + player2.transform.position);
			if(Physics.Raycast(ray,out hit)){
				if(hit.transform.gameObject == player2){
					p2Health.TakeDamage(Time.deltaTime);
				}
			}
		}

//		//Debug.Log ("P2");
//		fixatePlayer = player2.transform.position;
//		lastKnownLocation = fixatePlayer; 
//
//
//		if (fixatePlayer == player1.transform.position) {
//			Ray ray = new Ray (transform.position, -transform.position + fixatePlayer);
//			if(Physics.Raycast(ray,out hit)){
//				if(hit.transform.gameObject == player1){
//					p1Health.TakeDamage(Time.deltaTime);
//				}
//			}
//		} else if (fixatePlayer == player2.transform.position) {
//			Ray ray = new Ray (transform.position, -transform.position + fixatePlayer);
//			if(Physics.Raycast(ray,out hit)){
//				if(hit.transform.gameObject == player2){
//					p2Health.TakeDamage(Time.deltaTime);
//				}
//			}
//		}
	}

    private void Fixate()
    {
//		Debug.Log ("Fixate");
//        float p1Dist = Vector3.Distance(transform.position, player1.transform.position);
//        float p2Dist = Vector3.Distance(transform.position, player2.transform.position);
//        if (p1Dist < p2Dist)
//        {
//            fixatePlayer = player1;
//        }
//        else
//        {
//            fixatePlayer = player2;
//        }

		Quaternion lookAtp1 = Quaternion.LookRotation (player1.transform.position);
		Quaternion lookAtp2 = Quaternion.LookRotation (player2.transform.position);

	
		if ((Quaternion.Angle (transform.rotation, lookAtp2) < spot.spotAngle / 2) && (Quaternion.Angle (transform.rotation, lookAtp1) < spot.spotAngle / 2)) {
			float p1Dist = Vector3.Distance (transform.position, player1.transform.position);
			float p2Dist = Vector3.Distance (transform.position, player2.transform.position);
			if (p1Dist < p2Dist) {
				fixatePlayer = player1.transform.position;
			} else {
				fixatePlayer = player2.transform.position;
			}
			lastKnownLocation = fixatePlayer;
		} else if (Quaternion.Angle (transform.rotation, lookAtp1) < spot.spotAngle / 2) {
			//Debug.Log ("P1");
			fixatePlayer = player1.transform.position;
			lastKnownLocation = fixatePlayer;
		} else if (Quaternion.Angle (transform.rotation, lookAtp2) < spot.spotAngle / 2) {
			//Debug.Log ("P2");
			fixatePlayer = player2.transform.position;
			lastKnownLocation = fixatePlayer; 
		}else
			fixatePlayer = lastKnownLocation;
    }



	void ProcessMovement(){
		//vel *= 0.1f;
		accel = Vector3.zero;

		Vector3 dv = fixatePlayer - transform.position;

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
        Vector3 targetVector = fixatePlayer - transform.position;
		Quaternion tempRot = transform.rotation;

		transform.LookAt (fixatePlayer);
		transform.rotation = Quaternion.Lerp (tempRot, transform.rotation, Time.deltaTime * 0.82f);
			//Quaternion.Lerp(transform.rotation,transform.LookAt(fixatePlayer.transform.position),Time.deltaTime);
    }
}
