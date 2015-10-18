using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerSetup : MonoBehaviour {
 
    public bool player2;
    public float accelAmount,maxSpeed;

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

        CheckKeyboard();

        vel += acc;
        if (vel.magnitude > maxSpeed)
        {
            vel = vel.normalized * maxSpeed * Time.deltaTime;
        }
        controller.Move(vel);
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
