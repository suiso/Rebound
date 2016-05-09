using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class EnemyPoint : MonoBehaviour {
	//public GameObject enemy;
	public GameObject[] enemyPoint;
	public GameObject[] enemies;
	private float nextEnemy = 10.0F;
	private float enemyRate;
	Enemy enemy;
	public Text scoreLabel;
	public int score;

	void Start () {
		GameObject _enemy = Resources.Load("Prefabs/Enemy1") as GameObject;
		enemy = _enemy.GetComponent<Enemy>();

		for (int i = 0; i < 6; i ++) {
			//var selectEnemy = enemies[Random.Range(0,enemies.Length)];
			//var selectEnemyPoint = enemyPoint[Random.Range(0,enemyPoint.Length)];
			enemy.PutEnemy(enemies[Random.Range(0,enemies.Length)],enemyPoint[Random.Range(0,1)],Random.Range(3,10));
		}
	}

	void Update() {
		if (Time.time > nextEnemy) {
			enemyRate = Random.Range(0.3f,1.0f);
			nextEnemy = Time.time + enemyRate;
			var selectEnemy = enemies[Random.Range(0,enemies.Length)];
			var selectEnemyPoint = enemyPoint[Random.Range(0,enemyPoint.Length)];
			enemy.PutEnemy(selectEnemy,selectEnemyPoint,Random.Range(3,12));
			//GetComponent<AudioSource>().Play ();
		}
	}

	public void CalcScore() {
		score++;
		scoreLabel.text = "Score: " + score;	
	}
}
