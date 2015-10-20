using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerAbilities : MonoBehaviour {

    public GameObject EnergyWave;
	public GameObject Slingshot;

	private GameObject p1a1Button;
	private GameObject p1a2Button;
	private GameObject p2a1Button;
	private GameObject p2a2Button;
	private GameObject p1a1CD;
	private GameObject p1a2CD;
	private GameObject p2a1CD;
	private GameObject p2a2CD;

    float EnergyWaveCooldown = 0.0f;
	float SlingshotCooldown = 0.0f;

    KeyCode p1a1 = KeyCode.Q;
    KeyCode p1a2 = KeyCode.E;
    KeyCode p2a1 = KeyCode.U;
    KeyCode p2a2 = KeyCode.O;

	// Use this for initialization
	void Start () {
		p1a1Button = GameObject.Find ("P1 Ability 1");
		p1a2Button = GameObject.Find ("P1 Ability 2");
		p2a1Button = GameObject.Find ("P2 Ability 1");
		p2a2Button = GameObject.Find ("P2 Ability 2");
		p1a1CD = GameObject.Find ("P1 A1 CDBar");
		p1a2CD = GameObject.Find ("P1 A2 CDBar");
		p2a1CD = GameObject.Find ("P2 A1 CDBar");
		p2a2CD = GameObject.Find ("P2 A2 CDBar");
	}
	
	// Update is called once per frame
	void Update () {

        if ((Input.GetKey(p1a1)||Input.GetButtonDown("LB")) && !GetComponent<PlayerSetup>().player2 && EnergyWaveCooldown < 0.0f)

		// Do ablility and disable button
        if (Input.GetKey(p1a1) && !GetComponent<PlayerSetup>().player2 && EnergyWaveCooldown < 0.0f)

        {
			Player1Ability1();
			p1a1Button.GetComponent<Button>().interactable = false;
        }


		if ((Input.GetKey(p1a2)||Input.GetAxis("LT") > 0.1) && !GetComponent<PlayerSetup>().player2 && SlingshotCooldown < 0.0f)

        if (Input.GetKey(p1a2) && !GetComponent<PlayerSetup>().player2 && SlingshotCooldown < 0.0f)

        {
			Player1Ability2();
			p1a2Button.GetComponent<Button>().interactable = false;
        }


        if ((Input.GetKey(p2a1)||Input.GetButtonDown("RB")) && GetComponent<PlayerSetup>().player2 && EnergyWaveCooldown < 0.0f)

        if (Input.GetKey(p2a1) && GetComponent<PlayerSetup>().player2 && EnergyWaveCooldown < 0.0f)

        {
			Player2Ability1();
			// Change this when we get new ability for p2
			p2a1Button.GetComponent<Button>().interactable = false;
			p2a2Button.GetComponent<Button>().interactable = false;
        }


		if ((Input.GetKey(p2a2)||Input.GetAxis("RT") > 0.1) && GetComponent<PlayerSetup>().player2 && EnergyWaveCooldown < 0.0f)

        if (Input.GetKey(p2a2) && GetComponent<PlayerSetup>().player2 && EnergyWaveCooldown < 0.0f)

        {
			Player2Ability2();
			// Change this when we get new ability for p2
			p2a1Button.GetComponent<Button>().interactable = false;
			p2a2Button.GetComponent<Button>().interactable = false;
        }

		// Update Cooldown and Cooldown bar
		if (EnergyWaveCooldown >= 0.0f && !GetComponent<PlayerSetup>().player2)
        {
            EnergyWaveCooldown -= Time.deltaTime;
			p1a1CD.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(70, Mathf.Lerp(0, 70, EnergyWaveCooldown));
        }
		if (SlingshotCooldown >= 0.0f && !GetComponent<PlayerSetup>().player2)
		{
			SlingshotCooldown -= Time.deltaTime;
			p1a2CD.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(70, Mathf.Lerp(0, 70, SlingshotCooldown));
		}
		if (EnergyWaveCooldown >= 0.0f && GetComponent<PlayerSetup>().player2)
		{
			EnergyWaveCooldown -= Time.deltaTime;
			p2a1CD.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(70, Mathf.Lerp(0, 70, EnergyWaveCooldown));
		}
		if (EnergyWaveCooldown >= 0.0f && GetComponent<PlayerSetup>().player2)
		{
			EnergyWaveCooldown -= Time.deltaTime;
			p2a2CD.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(70, Mathf.Lerp(0, 70, EnergyWaveCooldown));
		}

		// ReEnable all buttons after cooldown
		if(EnergyWaveCooldown < 0.0f && !GetComponent<PlayerSetup>().player2)
		{
			p1a1Button.GetComponent<Button>().interactable = true;
		}
		if (SlingshotCooldown < 0.0f && !GetComponent<PlayerSetup>().player2)
		{
			p1a2Button.GetComponent<Button>().interactable = true;
		}
		if(EnergyWaveCooldown < 0.0f && GetComponent<PlayerSetup>().player2)
		{
			p2a1Button.GetComponent<Button>().interactable = true;
		}
		if (EnergyWaveCooldown < 0.0f && GetComponent<PlayerSetup>().player2)
		{
			p2a2Button.GetComponent<Button>().interactable = true;
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
