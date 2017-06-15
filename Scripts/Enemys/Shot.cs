using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shot : MonoBehaviour
{
	public enum DirectionMode
	{
		Forword,
		//		Chase
	}

	public DirectionMode directionMode;
	public float speed = 1.0f;
	public GameObject explosion;
	public AudioClip damagedClip;
	Rigidbody rb;
	GameController gameController;
	//	GameObject player;

	void Start ()
	{
		if (gameController == null)
			gameController = GameObject.FindWithTag ("GameController").GetComponent <GameController> ();
		
		rb = GetComponent<Rigidbody> ();
		switch (directionMode)
		{
			case DirectionMode.Forword:
				rb.velocity = transform.forward * speed;
				break;
//			case DirectionMode.Chase:
//				player = GameObject.FindWithTag ("Player");
//				Vector3 distance = player.transform.position - transform.position;
//				distance.Normalize ();
//				rb.velocity = distance * speed;
//				break;
		}
	}
}
