using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
	GameController gameController;
	PlayerMovement playerMovement;
	FollowerController followerController;

	void Start ()
	{
		playerMovement = GameObject.FindWithTag ("Player").GetComponent <PlayerMovement> ();
		gameController = GameObject.FindWithTag ("GameController").GetComponent <GameController> ();
		followerController = GameObject.FindObjectOfType<FollowerController> ();
	}

	void OnTriggerExit (Collider other)
	{
		if (other.CompareTag ("Player"))
		{
			gameController.GameOver ();
		}

		if (other.CompareTag ("Follower"))
		{
			FollowerMovement fm = other.GetComponent <FollowerMovement> ();
			if (!fm.isDead)
			{
				other.transform.parent = null;
				FollowerController.s_FollowerCount--;
				playerMovement.RemoveParty (fm);
				followerController.CreateFollower ();
				other.gameObject.SetActive (false);	
			}
			else
			{
				other.gameObject.SetActive (false);	
			}
		}

		if (other.CompareTag ("Shot") || other.CompareTag ("Zombei"))
		{
			other.gameObject.SetActive (false);	
		}
	}
}
