using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(BoxCollider))]
public class CameraFollow : MonoBehaviour {

    private GameObject p1, p2;
    public GameObject P1
    {
        set { p1 = value; }
    }
    public GameObject P2
    {
        set { p2 = value; }
    }

    private Vector3 target;
    public float dist, speed;

    private Renderer p1R, p2R;

	// Use this for initialization
	void Start () {
        
	}

    public void SetRenderer()
    {
        p1R = p1.GetComponent<Renderer>();
        p2R = p2.GetComponent<Renderer>();
    }

    private Vector3 FindTarget()
    {
        Vector3 temp = (p1.transform.position + p2.transform.position) / 2;
        temp.y = transform.position.y;
        return temp;
    }

	// Update is called once per frame
	void Update () {
		if (p1.activeSelf && p2.activeSelf) {
			target = FindTarget ();
			target.y = (Vector3.Distance(p1.transform.position, p2.transform.position) / 1.125f > dist)?Vector3.Distance(p1.transform.position, p2.transform.position) / 1.125f : dist;
		} else if (p1.activeSelf) {
			target = p1.transform.position;
			target.y = dist;
		} else if (p2.activeSelf) {
			target = p2.transform.position;
			target.y = dist;
		}

        //Debug.Log(Vector3.Distance(p1.transform.position, p2.transform.position) / transform.position.y);

        transform.position = Vector3.Lerp(transform.position, target, speed * Time.deltaTime);
	}
}
