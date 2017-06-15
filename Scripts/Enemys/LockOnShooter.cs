using UnityEngine;
using System.Collections;

public class LockOnShooter : Shooter
{
	Transform player;

	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	void Update ()
	{
		if ((transform.position - player.position).sqrMagnitude > 25.0f * 25.0f)
			return;
		Vector3 direction = player.position - transform.position;
		transform.rotation = Quaternion.Slerp (
			transform.rotation,
			Quaternion.LookRotation (new Vector3 (direction.x, 0, direction.z)),
			1.0f);
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ("Player"))
		{
			routine = Fire ();
			StartCoroutine (routine);
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.gameObject.CompareTag ("Player"))
		{
			StopCoroutine (routine);
		}
	}
}
