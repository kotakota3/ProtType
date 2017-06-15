using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour
{
	public GameObject shot;
	public Transform shotPosition;
	public float attackTimes;
	public float RapidWait;
	public float startWait;
	public float endWait;
	protected IEnumerator routine;

	void Start ()
	{   
		routine = Fire ();
		StartCoroutine (routine);
	}

	void OnEnable ()
	{
		if (routine != null)
			StartCoroutine (routine);
	}

	void OnDisable ()
	{
		StopCoroutine (routine);
	}

	protected IEnumerator Fire ()
	{
		yield return new WaitForSeconds (startWait);
		while (Application.isPlaying)
		{
			for (int i = 0; i < attackTimes; i++)
			{
				ObjectPool.instance.GetGameObject (shot, shotPosition.position, shotPosition.rotation);
				yield return new WaitForSeconds (RapidWait);
			}
			yield return new WaitForSeconds (endWait);
		}
	}
}
