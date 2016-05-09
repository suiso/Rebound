using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Player : MonoBehaviour {
	public GameObject bullet;
	public Transform bundo;
	//public bool enableFire = false;
	public int transformAngle = 0;
	public int targetAngle = 90;
	public int extraTargetAngle = 0;
	//private	GameObject mainCamera;

	private float nextFire = 0.0F;
	public float fireRate = 0.5F;

	public float speed = 2f;
	public float radius;

	void Start() {
		//mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
	}

	void Update() {
		if (Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			SimplePool.Spawn(bullet, bundo.transform.position ,bundo.transform.rotation);
			//GetComponent<AudioSource>().Play ();
		}
		if( Input.GetKey("space")) {
			radius += 1;
			transform.rotation = Quaternion.Euler(0,0,radius);
			if(radius >= 360) {
				radius = 0;
			}
		}
	}

	private IEnumerator Shot(float waitTime) {
		for (int i = 0; i < waitTime; i++) {
			i = 0;
		}
		yield return new WaitForSeconds (waitTime);
	}

}
