using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {
	public GameObject target;
	public GameObject player;
	private int enemyCount = 0;
	float time = 150.0f;

	public int HP;
	public int id; //難易度調整のとき、IDに応じて違う敵を出すかも知れない
	public string textHp;
	private ParticleSystem deadParticle;

	EnemyPoint enemyPoint;

	void Start () {
		iTween.MoveTo(target, player.transform.position, time);
		deadParticle = target.GetComponent<ParticleSystem>();
		GameObject _enemyPoint = GameObject.FindWithTag("GameController");
		enemyPoint = _enemyPoint.GetComponent<EnemyPoint>();

	}

	public void PutEnemy (GameObject enemy,GameObject position,int hp) {
		//enemyListMst.Clear();
		enemyCount++;
		id = enemyCount;
		HP = hp;
		GetComponentInChildren<Text>().text  = HP.ToString();

		// Despawnしたとき、敵が前にいた場所で復活するのを避けたい
		enemy.transform.position = new Vector2(position.transform.position.x,position.transform.position.y);
		//GetComponentInChildren<Text>().text  = HP.ToString();
		SimplePool.Spawn(enemy, position.transform.position ,Quaternion.identity);
	}

	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.gameObject.tag == "Fire") {
			if(HP > 1) {
				HP--;
				this.GetComponentInChildren<Text>().text  = HP.ToString();
				GetComponent<AudioSource>().Play();
			}
			else {
				GetComponent<AudioSource>().Play();
				Destroy(this.gameObject);
				enemyPoint.CalcScore();
			}
		}
	}
}
