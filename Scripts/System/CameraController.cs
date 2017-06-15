using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public Transform player;
	public PlayerMovement playerMovement;
	public float camSpeed;

	Vector3 offset;
	bool goalCamMode;

	void Start ()
	{
		offset = transform.position - player.position;
	}

	void LateUpdate ()
	{
		transform.position = player.position + offset;
	}

	//	IEnumerator GoalCamera ()
	//	{
	//		if (!goalCamMode)
	//		{
	//			transform.position = player.position + offset;
	//			yield return null;
	//		}
	//		yield return new WaitForSeconds (1.0f);
	//		goalCamMode = true;
	//		while (goalCamMode)
	//		{
	//			transform.position = Vector3.Lerp (transform.position, new Vector3 (player.position.x, player.position.y, transform.position.z), Time.deltaTime * camSpeed);
	//			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (-28f, 0f, 0f), Time.deltaTime * camSpeed);
	//			yield return null;
	//		}
	//	}
}