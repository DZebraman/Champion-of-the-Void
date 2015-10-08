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
	}
	
	// Update is called once per frame
	void Update () {
        lifeSpan -= Time.deltaTime;

        Vector3 temp = transform.localScale;
        temp.x += (10.0f * Time.deltaTime);
		temp.z += (10.0f * Time.deltaTime);
        transform.localScale = temp;

        if (lifeSpan <= 0.0f)
        {
			if (boss != null){
				Ray ray = new Ray (transform.position, -transform.position + boss.transform.position);
				Debug.DrawRay(transform.position,transform.position - boss.transform.position);
				RaycastHit hit;
				if (Vector3.Distance (transform.position,boss.transform.position) < 10) {
					if(Physics.Raycast(ray, out hit)){
						if(hit.transform.gameObject == boss){
							bossHealth.TakeDamage(1);
						}
					}
				}
				boss.GetComponent<LightBossScript>().lastKnownLocation = transform.position;
			}
            Destroy(gameObject);
        }


	}
}
