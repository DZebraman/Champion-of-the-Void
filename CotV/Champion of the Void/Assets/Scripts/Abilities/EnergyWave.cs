using UnityEngine;
using System.Collections;

public class EnergyWave : MonoBehaviour {

    float lifeSpan = 1f;
	GameObject boss;
	HealthUI bossHealth;
	// Use this for initialization
	void Start () {
		boss = GameObject.FindGameObjectWithTag ("Boss");
		bossHealth = boss.GetComponent<HealthUI> ();
		Ray ray = new Ray (transform.position, -transform.position + boss.transform.position);
		Debug.DrawRay(transform.position,transform.position - boss.transform.position);
		RaycastHit hit;
		if (Vector3.Distance (transform.position,boss.transform.position) < 10) {
			if(Physics.Raycast(ray, out hit)){
				if(hit.transform.gameObject == boss){
					bossHealth.TakeDamage(1);
					Debug.Log("HIT");
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
        lifeSpan -= Time.deltaTime;

        Vector3 temp = transform.localScale;
        temp.x *= 1.1f;
        temp.z *= 1.1f;
        transform.localScale = temp;

        if (lifeSpan < 0.0f)
        {
            Destroy(gameObject);
        }


	}
}
