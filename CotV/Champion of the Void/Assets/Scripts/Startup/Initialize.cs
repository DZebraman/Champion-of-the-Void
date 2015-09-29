using UnityEngine;
using System.Collections;

public class Initialize : MonoBehaviour {

    public GameObject player1;
    public GameObject player2;

	// Use this for initialization
	void Start () {
        player1 = (GameObject)Instantiate(Resources.Load("Prefabs/Player"), new Vector3(0, 1, 0), Quaternion.identity);
        player1.GetComponent<MeshRenderer>().material = (Material)Resources.Load("Materials/P1");
        player2 = (GameObject)Instantiate(Resources.Load("Prefabs/Player"), new Vector3(2, 1, 0), Quaternion.identity);
        player2.GetComponent<PlayerSetup>().player2 = true;
        player2.GetComponent<MeshRenderer>().material = (Material)Resources.Load("Materials/P2");

        Camera.main.GetComponent<CameraFollow>().P1 = player1;
        Camera.main.GetComponent<CameraFollow>().P2 = player2;
        Camera.main.GetComponent<CameraFollow>().SetRenderer();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
