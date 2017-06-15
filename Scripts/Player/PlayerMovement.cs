using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] float speed = 8.0f;
	public VirtualPad virtualPad;
	public AudioClip deadClip;

	Rigidbody rb;
	Animator anim;
	GameController gameController;
	List<FollowerMovement> partyList = new List<FollowerMovement> ();

	bool _isDead = false;

	public bool isDead{ get { return _isDead; } }

	Vector3 _rescuePos = Vector3.zero;

	public Vector3 rescuePos{ get { return _rescuePos; } }

	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		anim = GetComponent <Animator> ();
		gameController = GameObject.FindWithTag ("GameController").GetComponent <GameController> ();
	}

	void Update ()
	{
		if (isDead)
			anim.speed = 0.0f;
		else
			anim.SetBool ("Stop", isStop ());
	}

	void FixedUpdate ()
	{
		if (isStop () || isDead)
			return;
		Move ();
		Turning ();
	}

	void Move ()
	{
		Vector3 movement = new Vector3 (virtualPad.Position.x, 0.0f, virtualPad.Position.y);
		movement = movement * speed * Time.deltaTime;
		rb.MovePosition (rb.position + movement);
	}

	void Turning ()
	{
		float rotationX = virtualPad.Position.x;
		float rotationZ = virtualPad.Position.y;
		if (!isStop ())
		{
			Vector3 direction = new Vector3 (rotationX, 0.0f, rotationZ);
			Quaternion newRotation = Quaternion.LookRotation (direction);
			rb.MoveRotation (newRotation);
		}
	}

	void Dead ()
	{
		_isDead = true;
		SoundManager.instance.PlaySingle (deadClip);
		gameController.GameOver ();
	}

	void OnTriggerEnter (Collider other)
	{
		//肉壁機能
		if (other.CompareTag ("EnemyCollider"))
		{
			List<FollowerMovement> canRescueList = new List<FollowerMovement> ();
			for (int i = 0; i < partyList.Count; i++)
			{
				if (!partyList [i].isRescue)
				{
					canRescueList.Add (partyList [i]);
				}
			}
			if (canRescueList.Count <= 0)
				return;
			int randomIndex = Random.Range (0, canRescueList.Count);
			canRescueList [randomIndex].isRescue = true;
			_rescuePos = (transform.position + other.transform.position) * 0.5f;
		}

		//被弾処理
		else if (other.gameObject.CompareTag ("Enemy"))
		{
			int id = gameObject.GetInstanceID ();
			if (gameController.IdList.Contains (id))
				return;
			gameController.IdList.Add (id);

			ObjectPool.instance.GetGameObject (other.GetComponent <Shot> ().explosion, other.transform.position, Quaternion.identity);
			ObjectPool.instance.ReleaseGameObject (other.gameObject);
			rb.constraints = RigidbodyConstraints.None;
			rb.AddForce (-transform.forward * other.GetComponent <Rigidbody> ().mass + Vector3.up * Random.Range (0.0f, 10.0f), ForceMode.Impulse);
			Dead ();
		}
	}

	public bool isStop ()
	{
		if (virtualPad.Position.x == 0.0f && virtualPad.Position.y == 0.0f)
			return true;
		return false;
	}

	public void AddParty (FollowerMovement follower)
	{
		partyList.Add (follower);
	}

	public void RemoveParty (FollowerMovement follower)
	{
		partyList.Remove (follower);
	}
}
