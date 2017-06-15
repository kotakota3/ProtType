using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
	static ObjectPool _instance;

	public static ObjectPool instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<ObjectPool> ();
				if (_instance == null)
				{
					_instance = new GameObject ("ObjectPool").AddComponent<ObjectPool> ();
				}
			}
			return _instance;
		}
	}

	Dictionary<int, List<GameObject>> pooledGameObjectDec = new Dictionary<int, List<GameObject>> ();

	public GameObject GetGameObject (GameObject prefab, Vector3 position, Quaternion rotation)
	{
		int key = prefab.GetInstanceID ();
		if (pooledGameObjectDec.ContainsKey (key) == false)
		{
			pooledGameObjectDec.Add (key, new List<GameObject> ());
		}
		List<GameObject> gameObjectList = pooledGameObjectDec [key];
		GameObject go = null;
		for (int i = 0; i < gameObjectList.Count; i++)
		{
			go = gameObjectList [i];
			if (go.activeInHierarchy == false)
			{
				go.transform.position = position;
				go.transform.rotation = rotation;
				go.SetActive (true);
				return go;
			}
		}
		go = (GameObject)Instantiate (prefab, position, rotation);
		go.transform.parent = transform;
		gameObjectList.Add (go);
		return go;
	}

	public void ReleaseGameObject (GameObject go)
	{
		go.SetActive (false);
	}
}