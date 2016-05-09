using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class OperationEnemyHP : MonoBehaviour {
	public Camera rotateCamera;




	// Use this for initialization
	void Start () {
		rotateCamera = Camera.main;
	}

	// Update is called once per frame
	void Update () {
		transform.rotation = rotateCamera.transform.rotation;
	}
}
