using UnityEngine;
using System.Collections;

public class ReleaseObjectByTime : MonoBehaviour
{
	public float lifeTime;

	void OnEnable ()
	{
		Invoke ("Release", lifeTime);
	}

	void Release ()
	{
		gameObject.SetActive (false);
	}
}
