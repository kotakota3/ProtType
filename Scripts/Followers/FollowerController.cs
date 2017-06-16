using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowerController : MonoBehaviour
{
	public static int s_FollowerCount = 20;
	public GameObject[] followerObjects;
	public Transform[] followerPositions;
	public PlayerMovement playerMovement;
	public FollowerCountPanel followerCountPanel;

	int oldFollowerCount;

	void Start ()
	{
		s_FollowerCount = 20;

		oldFollowerCount = s_FollowerCount;
		PrepareFollower ();
	}

	void Update ()
	{
		if (oldFollowerCount != s_FollowerCount)
		{
			followerCountPanel.UpdateFollowerCount ();
			oldFollowerCount = s_FollowerCount;
		}
	}

	//初期配置用
	void PrepareFollower ()
	{
		if (s_FollowerCount <= 0)
			return;
		int end = s_FollowerCount >= followerPositions.Length ? followerPositions.Length : s_FollowerCount;
		for (int i = 0; i < end; i++)
		{
			if (followerPositions [i].childCount == 0)
			{
				int randomIndex = Random.Range (0, followerObjects.Length);
				GameObject go = (GameObject)Instantiate (followerObjects [randomIndex], followerPositions [i].position, followerObjects [randomIndex].transform.rotation);
				go.transform.SetParent (followerPositions [i], false);
				playerMovement.AddParty (go.GetComponent <FollowerMovement> ());
			}
		}
	}

	//生成用
	public void CreateFollower ()
	{
		if (s_FollowerCount < followerPositions.Length)
			return;
		List<Transform> freePositionList = new List<Transform> ();
		for (int i = 0; i < followerPositions.Length; i++)
		{
			if (followerPositions [i].childCount == 0)
			{
				freePositionList.Add (followerPositions [i]);
			}
		}
		int objIndex = Random.Range (0, followerObjects.Length);
		int posIndex = Random.Range (0, freePositionList.Count);
		GameObject go = (GameObject)Instantiate (followerObjects [objIndex], freePositionList [posIndex].position, followerObjects [objIndex].transform.rotation);
		go.transform.SetParent (freePositionList [posIndex], false);
		playerMovement.AddParty (go.GetComponent <FollowerMovement> ());
	}

	//	void RepositionFollowers ()
	//	{
	//		for (int i = 0; i < followerPositions.Length; i++)
	//		{
	//			if (followerPositions [i].childCount != 0)
	//				continue;
	//			for (int j = i + 1; j < followerPositions.Length; j++)
	//			{
	//				if (followerPositions [j].childCount == 0)
	//					continue;
	//				followerPositions [j].GetChild (0).SetParent (followerPositions [i]);
	//				break;
	//			}
	//		}
	//	}
}
