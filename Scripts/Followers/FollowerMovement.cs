using UnityEngine;
using System.Collections;

public class FollowerMovement : MonoBehaviour
{
	[SerializeField]float speed;
	public AudioClip deadClip;

	PlayerMovement playerMovement;
	GameController gameController;
	FollowerController followerController;

	Rigidbody rb;
	Animator anim;
	Vector3 destination;

	float distance;
	bool isArrive = true;
	bool isDead = false;
	bool isFight = false;

	public bool isRescue{ get; set; }

	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		anim = GetComponent <Animator> ();
		playerMovement = GameObject.FindWithTag ("Player").GetComponent<PlayerMovement> ();
		gameController = GameObject.FindWithTag ("GameController").GetComponent <GameController> ();
		followerController = FindObjectOfType<FollowerController> ();
		speed = Random.Range (speed, speed * 1.5f);
	}

	void Update ()
	{
		if (isDead)
			anim.speed = 0.0f;
		else
			anim.SetBool ("Stop", Stop ());
	}

	void FixedUpdate ()
	{
		if (isDead || isFight || playerMovement.isDead)
			return;
		Move ();
		Turning ();
	}

	void Move ()
	{
		destination = isRescue ? playerMovement.rescuePos : transform.parent == null ? transform.position : transform.parent.position;
		if (!playerMovement.isStop ())
		{
			if (isArrive)
			{
				isArrive = false;
				distance = isRescue ? 0.1f : Random.Range (1.0f, 3.0f);
			}
		}
		if (!isArrive)
		{
			if (Vector3.Distance (transform.position, destination) > distance)
			{
				rb.position = isRescue ? Vector3.MoveTowards (rb.position, destination, speed * Time.deltaTime * 10.0f) : Vector3.MoveTowards (rb.position, destination, speed * Time.deltaTime);
				rb.MovePosition (rb.position);
			}
			else
			{
				destination = rb.position;
				rb.MovePosition (destination);
				isArrive = true;
				Invoke ("EndFlagRescue", 0.2f);
			}
		}
	}

	void Turning ()
	{
		if (!isArrive)
		{
			Vector3 direction = isRescue ? playerMovement.rescuePos - transform.position : transform.parent.position - transform.position;
			direction.y = 0.0f;
			if (direction == Vector3.zero)
				return;
			Quaternion newRotation = Quaternion.LookRotation (direction);
			rb.MoveRotation (newRotation);
		}
	}

	void EndFlagRescue ()
	{
		isRescue = false;
	}

	bool Stop ()
	{
		if (Vector3.Distance (transform.position, destination) < distance)
			return true;
		return false;
	}

	void Dead ()
	{
		if (playerMovement.isDead)
			return;
		isDead = true;
		FollowerController.s_FollowerCount--;
		SoundManager.instance.PlayThreeD (deadClip);
		playerMovement.RemoveParty (this);
		followerController.CreateFollower ();
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ("Enemy"))
		{
			int id = gameObject.GetInstanceID ();
			if (gameController.IdList.Contains (id))
				return;
			gameController.IdList.Add (id);
			gameObject.transform.parent = null;
			if (isRescue)
			{
				isFight = true;
				other.GetComponent <Collider> ().enabled = false;
				other.GetComponent <Rigidbody> ().velocity = Vector3.zero;
				anim.SetTrigger ("Fight");
				StartCoroutine (Scapegoat (other, 3.0f));
			}
			else
			{
				StartCoroutine (Scapegoat (other, 0.0f));
			}
		}
	}

	IEnumerator Scapegoat (Collider enemy, float fightTime)
	{
		yield return new WaitForSeconds (fightTime);
		ObjectPool.instance.GetGameObject (enemy.GetComponent <Shot> ().explosion, enemy.transform.position, Quaternion.identity);
		ObjectPool.instance.ReleaseGameObject (enemy.gameObject);
		rb.constraints = RigidbodyConstraints.None;
		rb.AddForce (-transform.forward * enemy.GetComponent <Rigidbody> ().mass + Vector3.up * Random.Range (0.0f, 10.0f), ForceMode.Impulse);
		Dead ();
	}
}