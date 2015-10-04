using UnityEngine;
using System.Collections;

public class ReflectionManager : MonoBehaviour {
    ReflectionProbe probe;

    // Use this for initialization
    void Start()
    {
        probe = GetComponent<ReflectionProbe>();
    }

    // Update is called once per frame
    void Update() {
        probe.transform.position = new Vector3(Camera.main.transform.position.x,-Camera.main.transform.position.y,Camera.main.transform.position.z);

        probe.RenderProbe();
	}
}
