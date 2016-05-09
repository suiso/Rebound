using UnityEngine;
using System.Collections;

public class BulletMove : MonoBehaviour {
	private float speed = 12f;
	public GameObject _target;
	public Rigidbody2D bulletRigid;
	public GameObject bun;
	Player player;
	Enemy enemy;


	// Use this for initialization
	void Start () {
		//GetComponent<Rigidbody2D>().AddForce(transform.forward * speed);
		//iTween.MoveTo(_target, bun.transform.position, speed);
		GetComponent<Rigidbody2D>().velocity = transform.position.normalized * speed;
		GameObject _player = GameObject.FindWithTag("Player");
		player = _player.GetComponent<Player>();
		GameObject _enemy = GameObject.FindWithTag("Enemy");
		enemy = _enemy.GetComponent<Enemy>();
	}
	void FixedUpdate() {
		if (!GetComponent<Renderer>().isVisible) {
			SimplePool.Despawn(_target);
		}
		else {
			Shot();
		}
	}

	void Shot() {
		Vector2 heading = _target.transform.position - bun.transform.position;

		float headingX = _target.transform.position.x - bun.transform.position.x;
		float headingY = _target.transform.position.y - bun.transform.position.y;

		float rad = Mathf.Atan2(headingX,headingY);
		var direction = rad * Mathf.Rad2Deg;
		GetComponent<Rigidbody2D>().AddForce(heading);
		}

	public void SetVelocityForRigidbody2D(float direction) {
		// Setting velocity.
		Vector2 v;
		v.x = Mathf.Cos (Mathf.Deg2Rad * direction) * speed;
		v.y = Mathf.Sin (Mathf.Deg2Rad * direction) * speed;
		GetComponent<Rigidbody2D>().velocity = v;
	}





	// Update is called once per frame
	/*
	void Update () {
		if (!GetComponent<Renderer>().isVisible) {
			SimplePool.Despawn(_target);
		}
		else {
			GetComponent<Rigidbody2D>().velocity = transform.position.normalized * speed;
		}
	}
	*/
}
