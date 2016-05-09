///
/// Simple pooling for Unity.
///   Author: Martin "quill18" Glaude (quill18@quill18.com)
///   Latest Version: https://gist.github.com/quill18/5a7cfffae68892621267
///   License: CC0 (http://creativecommons.org/publicdomain/zero/1.0/)
///   UPDATES:
/// 	2015-04-16: Changed Pool to use a Stack generic.
///
/// Usage:
///
///   There's no need to do any special setup of any kind.
///
///   Instead of call Instantiate(), use this:
///       SimplePool.Spawn(somePrefab, somePosition, someRotation);
///
///   Instead of destroying an object, use this:
///       SimplePool.Despawn(myGameObject);
///
///   If desired, you can preload the pool with a number of instances:
///       SimplePool.Preload(somePrefab, 20);
///
/// Remember that Awake and Start will only ever be called on the first instantiation
/// and that member variables won't be reset automatically.  You should reset your
/// object yourself after calling Spawn().  (i.e. You'll have to do things like set
/// the object's HPs to max, reset animation states, etc...)
///
///
///

using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public static class SimplePool {
	const int DEFAULT_POOL_SIZE = 100;

	class Pool {
		int nextId=1;

		ArrayList inactiveList;

		// The prefab that we are pooling
		GameObject prefab;

		// Constructor
		public Pool(GameObject prefab, int initialQty) {
			this.prefab = prefab;

			inactiveList = new ArrayList();
		}

		public GameObject Spawn(Vector3 pos, Quaternion rot) {
			GameObject obj;
			if(inactiveList.Count==0) {
				obj = (GameObject)GameObject.Instantiate(prefab, pos, rot);
				obj.name = prefab.name + " ("+(nextId++)+")";

				obj.AddComponent<PoolMember>().myPool = this;
			}
			else {
				int random = Random.Range (0, inactiveList.Count);

				obj = (GameObject)inactiveList[random];
				inactiveList.RemoveAt(random);

				if(obj == null) {
					return Spawn(pos, rot);
				}
			}

			obj.transform.position = pos;
			obj.transform.rotation = rot;
			obj.SetActive(true);
			return obj;

		}

		public void Despawn(GameObject obj) {
			obj.SetActive(false);

			inactiveList.Add(obj);
		}

		public void showPool() {
			for (int i=0; i<inactiveList.Count; i++){
		    	Debug.Log(inactiveList[i].ToString());
		    }
		}

	}

	class PoolMember : MonoBehaviour {
		public Pool myPool;
	}

	static Dictionary< GameObject, Pool > pools;

	static void Init (GameObject prefab=null, int qty = DEFAULT_POOL_SIZE) {
		if(pools == null) {
			pools = new Dictionary<GameObject, Pool>();
		}
		if(prefab!=null && pools.ContainsKey(prefab) == false) {
			pools[prefab] = new Pool(prefab, qty);
		}
	}

	static public void Preload(GameObject prefab, int qty = 1) {
		Init(prefab, qty);

		GameObject[] obs = new GameObject[qty];
		for (int i = 0; i < qty; i++) {
			obs[i] = Spawn (prefab, Vector3.zero, Quaternion.identity);
		}

		for (int i = 0; i < qty; i++) {
			Despawn( obs[i] );
		}
	}

	static public GameObject Spawn(GameObject prefab, Vector3 pos, Quaternion rot) {
		Init(prefab);

		return pools[prefab].Spawn(pos, rot);
	}

	static public void Despawn(GameObject obj) {
		PoolMember pm = obj.GetComponent<PoolMember>();
		if(pm == null) {
			Debug.Log ("Object '"+obj.name+"' wasn't spawned from a pool. Destroying it instead.");
			GameObject.Destroy(obj);
		}
		else {
			pm.myPool.Despawn(obj);
		}
	}

	static public void show(GameObject obj) {
		pools[obj].showPool();
	}

}
