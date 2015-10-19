using UnityEngine;
using System.Collections;

public class PlayerAbilities : MonoBehaviour {

    public GameObject EnergyWave;
	public GameObject Slingshot;

    float EnergyWaveCooldown = 0.0f;
	float SlingshotCooldown = 0.0f;

    KeyCode p1a1 = KeyCode.Q;
    KeyCode p1a2 = KeyCode.E;
    KeyCode p2a1 = KeyCode.U;
    KeyCode p2a2 = KeyCode.O;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if ((Input.GetKey(p1a1)||Input.GetButtonDown("LB")) && !GetComponent<PlayerSetup>().player2 && EnergyWaveCooldown < 0.0f)
        {
            Player1Ability1();
        }

		if ((Input.GetKey(p1a2)||Input.GetAxis("LT") > 0.1) && !GetComponent<PlayerSetup>().player2 && SlingshotCooldown < 0.0f)
        {
            Player1Ability2();
        }

        if ((Input.GetKey(p2a1)||Input.GetButtonDown("RB")) && GetComponent<PlayerSetup>().player2 && EnergyWaveCooldown < 0.0f)
        {
            Player2Ability1();
        }

		if ((Input.GetKey(p2a2)||Input.GetAxis("RT") > 0.1) && GetComponent<PlayerSetup>().player2 && EnergyWaveCooldown < 0.0f)
        {
            Player2Ability2();
        }

        if (EnergyWaveCooldown >= 0.0f)
        {
            EnergyWaveCooldown -= Time.deltaTime;
        }

		if (SlingshotCooldown >= 0.0f)
		{
			SlingshotCooldown -= Time.deltaTime;
		}
	}

    void Player1Ability1 ()
    {
        Vector3 temp = transform.position;
        temp.y = 0.01f;
        GameObject.Instantiate(EnergyWave, temp, Quaternion.identity);
        EnergyWaveCooldown = 2.0f;
    }

    void Player1Ability2()
    {
        Vector3 temp = transform.position;
        temp.y = 0.01f;
        GameObject mySlingshot = (GameObject) Instantiate(Slingshot, temp, Quaternion.identity);
		mySlingshot.GetComponent<Slingshot>().owner = gameObject;
		mySlingshot.GetComponent<Slingshot> ().detonateKey = p1a2;
        SlingshotCooldown = 6.0f;
    }

    void Player2Ability1()
    {
        Vector3 temp = transform.position;
        temp.y = 0.01f;
        GameObject.Instantiate(EnergyWave, temp, Quaternion.identity);
        EnergyWaveCooldown = 2.0f;
    }

    void Player2Ability2()
    {
        Vector3 temp = transform.position;
        temp.y = 0.01f;
        GameObject.Instantiate(EnergyWave, temp, Quaternion.identity);
        EnergyWaveCooldown = 2.0f;
    }
}
