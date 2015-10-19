using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerSetup : MonoBehaviour {
 
    public bool player2;
    public float accelAmount,maxSpeed;

	public bool useController;

    private Vector3 vel, acc;

    private CharacterController controller;
	// Use this for initialization
	void Start () {
        vel = Vector3.zero;
        acc = Vector3.zero;
        controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        vel *= 0.85f;
        acc = Vector3.zero;

		if(useController)
			CheckContoller();
        else
			CheckKeyboard();

        vel += acc;
        if (vel.magnitude > maxSpeed)
        {
            vel = vel.normalized * maxSpeed * Time.deltaTime;
        }
        controller.Move(vel);
	}

	void CheckContoller(){
		if(!player2){
			acc.z = -Input.GetAxis("LStickX") * 0.5f;
			acc.x = Input.GetAxis("LStickY") * 0.5f;
		}
		else{
			acc.z = -Input.GetAxis("RStickX") * 0.5f;
			acc.x = Input.GetAxis("RStickY") * 0.5f;
		}
	}

    void CheckKeyboard(){
        if (!player2){
            if (Input.GetKey(KeyCode.W)){
                acc.z += accelAmount;
            }
            if (Input.GetKey(KeyCode.S)){
                acc.z -= accelAmount;
            }
            if (Input.GetKey(KeyCode.A)){
                acc.x -= accelAmount;
            }
            if (Input.GetKey(KeyCode.D)){
                acc.x += accelAmount;
            }
        }
        else{
            if (Input.GetKey(KeyCode.I))
            {
                acc.z += accelAmount;
            }
            if (Input.GetKey(KeyCode.K))
            {
                acc.z -= accelAmount;
            }
            if (Input.GetKey(KeyCode.J))
            {
                acc.x -= accelAmount;
            }
            if (Input.GetKey(KeyCode.L))
            {
                acc.x += accelAmount;
            }
        }
    }
}
