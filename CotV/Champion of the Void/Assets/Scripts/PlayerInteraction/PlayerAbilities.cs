using UnityEngine;
using System.Collections;

public class PlayerAbilities : MonoBehaviour {

    public GameObject EnergyWave;
    float EnergyWaveCooldown = 0.0f;

    KeyCode p1a1 = KeyCode.Q;
    KeyCode p1a2 = KeyCode.E;
    KeyCode p2a1 = KeyCode.U;
    KeyCode p2a2 = KeyCode.O;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(p1a1) && !GetComponent<PlayerSetup>().player2 && EnergyWaveCooldown < 0.0f)
        {
            Player1Ability1();
        }

        if (Input.GetKey(p1a2) && !GetComponent<PlayerSetup>().player2 && EnergyWaveCooldown < 0.0f)
        {
            Player1Ability2();
        }

        if (Input.GetKey(p2a1) && GetComponent<PlayerSetup>().player2 && EnergyWaveCooldown < 0.0f)
        {
            Player2Ability1();
        }

        if (Input.GetKey(p2a2) && GetComponent<PlayerSetup>().player2 && EnergyWaveCooldown < 0.0f)
        {
            Player2Ability2();
        }

        if (EnergyWaveCooldown >= 0.0f)
        {
            EnergyWaveCooldown -= Time.deltaTime;
        }
	}

    void Player1Ability1 ()
    {
        Vector3 temp = transform.position;
        temp.y = 0.0f;
        GameObject.Instantiate(EnergyWave, temp, Quaternion.identity);
        EnergyWaveCooldown = 2.0f;
    }

    void Player1Ability2()
    {
        Vector3 temp = transform.position;
        temp.y = 0.0f;
        GameObject.Instantiate(EnergyWave, temp, Quaternion.identity);
        EnergyWaveCooldown = 2.0f;
    }

    void Player2Ability1()
    {
        Vector3 temp = transform.position;
        temp.y = 0.0f;
        GameObject.Instantiate(EnergyWave, temp, Quaternion.identity);
        EnergyWaveCooldown = 2.0f;
    }

    void Player2Ability2()
    {
        Vector3 temp = transform.position;
        temp.y = 0.0f;
        GameObject.Instantiate(EnergyWave, temp, Quaternion.identity);
        EnergyWaveCooldown = 2.0f;
    }
}
