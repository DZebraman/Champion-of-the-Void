using UnityEngine;
using System.Collections;

public class LightBossScript : MonoBehaviour
{
    private Light spot;
    private GameObject[] players = new GameObject[2];
    private GameObject fixatePlayer;

    // Use this for initialization
    void Start()
    {
        spot = transform.FindChild("Spotlight").GetComponent<Light>();
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Fixate();
    }

    private void Fixate()
    {
        float p1Dist = Vector3.Distance(transform.position, players[0].transform.position);
        float p2Dist = Vector3.Distance(transform.position, players[1].transform.position);
        if (p1Dist < p2Dist)
        {
            fixatePlayer = players[0];
        }
        else
        {
            fixatePlayer = players[1];
        }
    }

    private void TurnToFace()
    {
        Vector3 targetVector = fixatePlayer.transform.position - transform.position;
    }
}
