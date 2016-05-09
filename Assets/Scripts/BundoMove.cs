using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BundoMove : MonoBehaviour {

	public float speed = 2f;
	public float radius = 2f;
	Vector2 vec;

	void Awake() {
	}

	void Update () {
		if ( Input.GetKey("space")) {
			RadiusControll();
		}
		if(Input.GetMouseButtonDown(0)) {
			vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	//Debug.Log("x="+vec.x+" y="+vec.y);
		}
		transform.position = Vector2.MoveTowards(transform.position, new Vector2(vec.x,vec.y), speed *Time.deltaTime);
	}


	void RadiusControll() {
		float x = Mathf.Cos(Time.time * speed) * radius;
		float y = Mathf.Sin(Time.time * speed) * radius;
		float z = 0f;
		transform.position = new Vector3(x, y, z);
	}

	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.gameObject.tag == "Enemy") {
			//SimplePool.Despawn(coll.gameObject);
		}
		else if (coll.gameObject.tag == "Life") {
			SimplePool.Despawn(coll.gameObject);
		}

	}
}
