using UnityEngine;
using System.Collections;

public class LightBossScript : MonoBehaviour
{
    private Light spot;
    private Vector3 fixatePlayer;
	public Vector3 lastKnownLocation;
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
        /*
		if (Vector3.Distance (transform.position, player1.transform.position) < 10.0f) {
			lastKnownLocation = player1.transform.position;
		}
		if (Vector3.Distance (transform.position, player2.transform.position) < 10.0f) {
			lastKnownLocation = player2.transform.position;
		}
        */
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

    private void Fixate()
    {
        float p1Dist = Vector3.Distance(transform.position, player1.transform.position);
        float p2Dist = Vector3.Distance(transform.position, player2.transform.position);

        if (p1Dist < 100 || p2Dist < 100 && (player2.activeSelf && player1.activeSelf))
        {
            Debug.Log("Close");
            fixatePlayer = (p1Dist < p2Dist) ? player1.transform.position : player2.transform.position;
        }

        else if ((angle1 < spot.spotAngle / 1.5) && angle2 < spot.spotAngle / 1.5 && (player2.activeSelf && player1.activeSelf))
        {
			if (p1Dist < p2Dist) {
				fixatePlayer = player1.transform.position;
			} else {
				fixatePlayer = player2.transform.position;
			}
			lastKnownLocation = fixatePlayer;
		} else if (angle1 < spot.spotAngle / 2 && player2.activeSelf) {
			//Debug.Log ("P1");
			fixatePlayer = player1.transform.position;
			lastKnownLocation = fixatePlayer;
		} else if (angle2 < spot.spotAngle / 2 && player2.activeSelf) {
			//Debug.Log ("P2");
			fixatePlayer = player2.transform.position;
			lastKnownLocation = fixatePlayer;
        }
        else {
            fixatePlayer = lastKnownLocation;
        }
			

        if (fixatePlayer == player1.transform.position)
            Debug.Log("Player1");
        if (fixatePlayer == player2.transform.position)
            Debug.Log("Player2");
        if (fixatePlayer == lastKnownLocation)
            Debug.Log("Last");
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