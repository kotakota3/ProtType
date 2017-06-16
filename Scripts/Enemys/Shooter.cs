using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour
{
	public GameObject shot;
	public Transform shotPosition;
	public float attackTimes;
	public float startWait;
	public float rapidWait;
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
		yield return new WaitForSeconds (Random.Range (startWait, startWait * 2.0f));
		while (Application.isPlaying)
		{
			for (int i = 0; i < attackTimes; i++)
			{
				ObjectPool.instance.GetGameObject (shot, shotPosition.position, shotPosition.rotation);
				yield return new WaitForSeconds (rapidWait);
			}
			yield return new WaitForSeconds (Random.Range (endWait, endWait * 2.0f));
		}
	}
}
