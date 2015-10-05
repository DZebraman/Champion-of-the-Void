using UnityEngine;
using System.Collections;

public class LineRenderManager : MonoBehaviour {

    public Transform startPos;
    public Transform endPos;
    private LineRenderer lineRender;

	// Use this for initialization
	void Start () {
        lineRender = GetComponent<LineRenderer>();
        lineRender.SetPosition(0, startPos.position);
        lineRender.SetPosition(1, endPos.position);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
