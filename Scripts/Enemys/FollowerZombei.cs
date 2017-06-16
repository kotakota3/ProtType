using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerZombei : MonoBehaviour
{
	PlayerMovement playerMovement;
	Rigidbody rb;
	Animator anim;
	AudioClip deadClip;

	float speed;
	int hp;
	bool isDead;

	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		anim = GetComponent <Animator> ();
		playerMovement = GameObject.FindWithTag ("Player").GetComponent<PlayerMovement> ();
		gameObject.tag = "Zombei";
		gameObject.layer = 0;
		rb.useGravity = false;
		GetComponent <Collider> ().isTrigger = true;
		deadClip = Resources.Load ("Audio/zombeiDead") as AudioClip;
		gameObject.GetComponentInChildren<Renderer> ().material.color = Color.black;
		anim.speed = 0.15f;
		speed = 2.0f;
		hp = 2;
	}

	void Update ()
	{
		if (isDead)
			return;
		anim.SetBool ("Stop", false);
	}

	void FixedUpdate ()
	{
		if (isDead)
			return;
		Move ();
		Turning ();
	}

	void Move ()
	{
		rb.position = Vector3.MoveTowards (rb.position, playerMovement.transform.position, speed * Time.deltaTime);
		rb.MovePosition (rb.position);
	}

	void Turning ()
	{
		Vector3 direction = (playerMovement.transform.position - transform.position).normalized;
		direction.y = 0.0f;
		if (direction == Vector3.zero)
			return;
		Quaternion newRotation = Quaternion.LookRotation (direction);
		rb.MoveRotation (newRotation);
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.CompareTag ("Shot"))
		{
			ObjectPool.instance.GetGameObject (other.GetComponent <Shot> ().explosion, other.transform.position, Quaternion.identity);
			ObjectPool.instance.ReleaseGameObject (other.gameObject);
			hp--;
			if (hp <= 0)
			{
//				int id = gameObject.GetInstanceID ();
//				if (gameController.IdList.Contains (id))
//					return;
//				gameController.IdList.Add (id);
				rb.constraints = RigidbodyConstraints.None;
				rb.AddForce (-transform.forward * other.GetComponent <Rigidbody> ().mass + Vector3.up * Random.Range (0.0f, 10.0f), ForceMode.Impulse);
				Dead ();
			}
		}
	}

	void Dead ()
	{
		isDead = true;
		SoundManager.instance.PlayRandomize (deadClip);
		if (!SoundManager.instance.random_Source.isPlaying)
		{
			gameObject.SetActive (false);
		}
	}
}
