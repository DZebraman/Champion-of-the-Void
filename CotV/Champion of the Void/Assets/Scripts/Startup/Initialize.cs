using UnityEngine;
using System.Collections;

public class Initialize : MonoBehaviour {

    public GameObject player1;
    public GameObject player2;

	// Use this for initialization
	void Start () {
        player1 = (GameObject)Instantiate(Resources.Load("Prefabs/Player"), new Vector3(0, 0.5f, 0), Quaternion.identity);
        player1.GetComponent<MeshRenderer>().material = (Material)Resources.Load("Materials/P1");
        player2 = (GameObject)Instantiate(Resources.Load("Prefabs/Player"), new Vector3(2, 0.5f, 0), Quaternion.identity);
        player2.GetComponent<PlayerSetup>().player2 = true;
        player2.GetComponent<MeshRenderer>().material = (Material)Resources.Load("Materials/P2");
		player1.GetComponent<HealthUI> ().p1 = true;
		player2.GetComponent<HealthUI> ().p2 = true;


        player1.name = "Player1";
        player2.name = "Player2";

        Camera.main.GetComponent<CameraFollow>().P1 = player1;
        Camera.main.GetComponent<CameraFollow>().P2 = player2;
        Camera.main.GetComponent<CameraFollow>().SetRenderer();

		GameObject.Find ("Bossman").GetComponent<LightBossScript> ().Init ();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
	}
}
