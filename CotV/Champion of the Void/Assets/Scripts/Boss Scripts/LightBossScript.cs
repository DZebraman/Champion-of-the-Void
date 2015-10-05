using UnityEngine;
using System.Collections;

public class LightBossScript : MonoBehaviour
{
    private Light spot;
    private GameObject fixatePlayer;
    private GameObject player1;
    private GameObject player2;

    // Use this for initialization
    void Start()
    {
        spot = transform.FindChild("Spotlight").GetComponent<Light>();
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
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
    }

    private void Fixate()
    {
        float p1Dist = Vector3.Distance(transform.position, player1.transform.position);
        float p2Dist = Vector3.Distance(transform.position, player2.transform.position);
        if (p1Dist < p2Dist)
        {
            fixatePlayer = player1;
        }
        else
        {
            fixatePlayer = player2;
        }
    }

    private void TurnToFace()
    {
        Vector3 targetVector = fixatePlayer.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation((Vector2)targetVector);
    }
}
